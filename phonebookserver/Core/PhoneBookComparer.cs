using phonebookserver.data.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace phonebookserver.api.Core
{
    public class PhoneBookComparer : IEqualityComparer<PhoneBook>
    {
        public bool Equals(PhoneBook x, PhoneBook y) => x?.Name == y?.Name;

        public int GetHashCode([DisallowNull] PhoneBook obj) => obj.GetHashCode();

        public static PhoneBookComparer Instance = new PhoneBookComparer();
    }
}
