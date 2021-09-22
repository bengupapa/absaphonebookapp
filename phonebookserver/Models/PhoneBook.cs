using System.Collections.Generic;

namespace phonebookserver.Models
{
    public class PhoneBook
    {
        public string Name { get; set; }

        public List<Entry> Entries { get; set; }
    }
}
