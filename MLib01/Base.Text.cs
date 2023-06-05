using System;
using System.Collections.Generic;
using System.Linq;

namespace MLib01.Text
{
    public static partial class Base
    {
        public static IEnumerable<string> StrQSort(string[] array, bool ignoreCase)
        {
            if (array.Length < 2) return array;
            var povitIndex = array.Length / 2;
            var lessArray = Array.FindAll(array, str => String.Compare(str, array[povitIndex], ignoreCase) < 0);
            var povitArray = Array.FindAll(array, str => String.Compare(str, array[povitIndex], ignoreCase) == 0);
            var greaterArray = Array.FindAll(array, str => String.Compare(str, array[povitIndex], ignoreCase) > 0);
            return StrQSort(lessArray, ignoreCase).Concat(povitArray).Concat(StrQSort(greaterArray, ignoreCase));
        }
    }
}
