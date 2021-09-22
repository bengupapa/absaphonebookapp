using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phonebookserver.api.Models
{
    public class Entry
    {
        public int PhoneBookId { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }
    }
}
