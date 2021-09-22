using System;
using System.Linq;
using System.Threading.Tasks;

namespace phonebookserver.data.Models
{
    public class PhoneBookEntry
    {
        public int? Id { get; set; }

        public int PhoneBookId { get; set; }

        public string Name { get; set; }

        public string ContactNumber { get; set; }
    }
}
