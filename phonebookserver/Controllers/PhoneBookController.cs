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
    [Route("api/{controller}")]
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

        [HttpGet, Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                _logger.LogInformation("START: fetching data:");

                var data = id.HasValue
                    ? _context.PhoneBooks.Include(e => e.Entries).Where(e => e.Id == id)
                    : _context.PhoneBooks.Include(e => e.Entries);

                var response = await data.ToListAsync();

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

        [HttpPost, Route("create")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entry entry)
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
    }
}
