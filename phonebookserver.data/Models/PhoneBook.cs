using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public ICollection<PhoneBookEntry> Entries { get; set; }
    }
}
