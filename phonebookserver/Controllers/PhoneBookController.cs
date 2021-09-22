using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        public PhoneBookController(ILogger<PhoneBookController> logger) 
        {
            _logger = logger;
        }

        [HttpGet, Route("entries/{id?}")]
        public IActionResult Entries(int id)
        {
            var sampleData = System.IO.File.ReadAllText("sampledata.json");
            var data = JsonConvert.DeserializeObject<PhoneBook>(sampleData);
            return new JsonResult(data);
        }
    }
}
