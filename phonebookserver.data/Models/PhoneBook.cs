using System.Collections.Generic;

namespace phonebookserver.data.Models
{
    public class PhoneBook
    {
        public PhoneBook()
        {
            Entries = new HashSet<PhoneBookEntry>();
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<PhoneBookEntry> Entries { get; set; }
    }
}
