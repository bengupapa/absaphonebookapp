using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using phonebookserver.data;
using phonebookserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phonebookserver.Controllers
{
    [Route("api/{controller}")]
    public class PhoneBookController: Controller
    {
        private ILogger<PhoneBookController> _logger { get; }
        private PhoneBookDbContext _context { get; }

        public PhoneBookController(ILogger<PhoneBookController> logger, PhoneBookDbContext context) 
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet, Route("entries/{id?}")]
        public IActionResult Entries(int id)
        {
            var sampleData = System.IO.File.ReadAllText("sampledata.json");
            var data = JsonConvert.DeserializeObject<PhoneBookDirectory>(sampleData);

            return new JsonResult(data);
        }
    }
}
