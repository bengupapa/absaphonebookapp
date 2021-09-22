using phonebookserver.data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace phonebookserver.api.Core
{
    public class PhoneBookEntryComparer : IEqualityComparer<PhoneBookEntry>
    {
        public bool Equals(PhoneBookEntry x, PhoneBookEntry y) =>
            x?.Name == y?.Name
                && x?.ContactNumber == x?.ContactNumber
                && x?.PhoneBookId == y?.PhoneBookId;

        public int GetHashCode([DisallowNull] PhoneBookEntry obj) => obj.GetHashCode();

        public static PhoneBookEntryComparer Instance = new PhoneBookEntryComparer();
    }
}
