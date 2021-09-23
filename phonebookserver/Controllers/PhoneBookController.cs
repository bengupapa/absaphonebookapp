using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using phonebookserver.api.Core;
using phonebookserver.api.Models;
using phonebookserver.data;
using phonebookserver.data.Models;
using phonebookserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phonebookserver.Controllers
{
    [Route("api/contacts")]
    public class PhoneBookController : Controller
    {
        private ILogger<PhoneBookController> _logger { get; }
        private PhoneBookDbContext _context { get; }
        private JsonSerializerSettings _jsonSerializerSettings { get; }

        public PhoneBookController(ILogger<PhoneBookController> logger, PhoneBookDbContext context)
        {
            _logger = logger;
            _context = context;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        /// <summary>
        /// Gets phone books and their entries
        /// </summary>
        /// <returns>
        /// Phone books and their entries
        /// </returns>
        [HttpGet, Route("phonebooks")]
        public async Task<IActionResult> GetPhoneBooksAndTheirEntries()
        {
            try
            {
                _logger.LogInformation("START: fetching data:");

                var response = await _context.PhoneBooks.Include(e => e.Entries).ToListAsync();

                _logger.LogInformation(JsonConvert.SerializeObject(response, _jsonSerializerSettings));
                _logger.LogInformation("DONE: fetching data:");

                return Ok(response);
            }
            catch (Exception e)
            {
                var message = "ERROR: Failed to pull Phone Books";

                _logger.LogError(message, e);

                return BadRequest(message);
            }
        }

        /// <summary>
        /// Gets phone book and it's entries
        /// </summary>
        /// <param name="term">Search term for retrieving a specific phone book.</param>
        /// <returns>
        /// Phone book of the specified id and it's entries
        /// </returns>
        [HttpGet, Route("phonebooks/search/{term}")]
        public async Task<IActionResult> GetPhoneBookWithEntries(string term)
        {
            try
            {
                _logger.LogInformation("START: fetching data:");

                term = term.ToLower();

                var ids = await _context.PhoneBookEntries
                    .Include(e => e.PhoneBook)
                    .Where(e => e.Name.ToLower().Contains(term) || e.ContactNumber.Contains(term))
                    .Select(x => x.PhoneBookId)
                    .ToListAsync();

                var books = await _context.PhoneBooks
                    .Include(e => e.Entries)
                    .Where(e => e.Name.ToLower().Contains(term) || ids.Contains(e.Id.Value))
                    .ToListAsync();

                var response = books.Distinct(PhoneBookComparer.Instance).ToList();

                _logger.LogInformation(JsonConvert.SerializeObject(response, _jsonSerializerSettings));
                _logger.LogInformation("DONE: fetching Phone book and entries:");

                return Ok(response);
            }
            catch (Exception e)
            {
                var message = "ERROR: Failed to pull Phone Books";

                _logger.LogError(message, e);

                return BadRequest(message);
            }
        }

        /// <summary>
        /// Gets phone book and it's entries
        /// </summary>
        /// <param name="id">Id for retrieving a specific phone book.</param>
        /// <returns>
        /// Phone book of the specified id and it's entries
        /// </returns>
        [HttpGet, Route("phonebooks/{id}")]
        public async Task<IActionResult> GetPhoneBookWithEntries(int id)
        {
            try
            {
                _logger.LogInformation("START: fetching data:");

                var response = await _context.PhoneBooks
                    .Include(e => e.Entries)
                    .Where(e => e.Id == id)
                    .ToListAsync();

                _logger.LogInformation(JsonConvert.SerializeObject(response, _jsonSerializerSettings));
                _logger.LogInformation("DONE: fetching Phone book and entries:");

                return Ok(response);
            }
            catch (Exception e)
            {
                var message = "ERROR: Failed to pull Phone Books";

                _logger.LogError(message, e);

                return BadRequest(message);
            }
        }

        /// <summary>
        /// Creates new or updates exisiting phone book entry
        /// </summary>
        /// <param name="entry">Phone book entry payload</param>
        /// <returns></returns>
        [HttpPost, Route("createentry")]
        public async Task<IActionResult> PostEntry([FromBody] Entry entry)
        {
            try
            {
                _logger.LogInformation("START: processing entry:");

                var existingEntry = _context.PhoneBookEntries
                    .FirstOrDefault(x => x.Name.ToLower() == entry.Name.ToLower() && x.PhoneBookId == entry.PhoneBookId);

                if (existingEntry != null)
                {
                    existingEntry.ContactNumber = entry.ContactNumber;
                    _logger.LogInformation("UPDATED: existing entry successfully");
                }
                else
                {
                    PhoneBookEntry phoneBookEntry = new()
                    {
                        PhoneBookId = entry.PhoneBookId,
                        Name = entry.Name,
                        ContactNumber = entry.ContactNumber
                    };

                    _context.PhoneBookEntries.Add(phoneBookEntry);
                    _logger.LogInformation(JsonConvert.SerializeObject(entry));
                    _logger.LogInformation("CREATED: entry successfully");
                }

                await _context.SaveChangesAsync();

                return Ok("Entry processed successfully");
            }
            catch (Exception e)
            {
                var message = "ERROR: Failed to process entry";

                _logger.LogError($"{message} - {e.Message}");

                return BadRequest(message);
            }
        }

        /// <summary>
        /// Creates new phone book if it does not exist.
        /// </summary>
        /// <param name="book">Phone book payload</param>
        /// <returns></returns>
        [HttpPost, Route("createphonebook")]
        public async Task<IActionResult> CreatePhoneBook([FromBody] Book book)
        {
            try
            {
                _logger.LogInformation("START: processing phone book:");

                var existingBook = _context.PhoneBooks.FirstOrDefault(x => x.Name.ToLower() == book.Name.ToLower());

                if (existingBook != null)
                    return Ok("EXIST: Phone book already exists");

                PhoneBook phoneBook = new()
                {
                    Name = book.Name
                };

                _context.PhoneBooks.Add(phoneBook);
                _logger.LogInformation(JsonConvert.SerializeObject(book));
                _logger.LogInformation("CREATED: book successfully");

                await _context.SaveChangesAsync();

                return Ok("Phone book processed successfully");
            }
            catch (Exception e)
            {
                var message = "ERROR: Failed to process phone book";

                _logger.LogError($"{message} - {e.Message}");

                return BadRequest(message);
            }
        }
    }
}
