using System;
using System.Collections.Generic;
using System.Linq;

namespace MLib01
{
    public static partial class Base
    {
        public static IEnumerable<T> QSort<T>(T[] array) where T : IComparable<T>
        {
            if (array.Length < 2) return array;
            var povitIndex = array.Length / 2;
            var lessArray = Array.FindAll(array, item => item.CompareTo(array[povitIndex]) < 0);
            var povitArray = Array.FindAll(array, item => item.CompareTo(array[povitIndex]) == 0);
            var greaterArray = Array.FindAll(array, item => item.CompareTo(array[povitIndex]) > 0);
            return QSort(lessArray).Concat(povitArray).Concat(QSort(greaterArray));
        }

        private static int _tempInx1 = -1, _tempInx2 = -1;

        public static int BinarySearch<T>(T[] array,T value) where T : IComparable<T>
        {
            if (_tempInx1 < 0) _tempInx1 = 0;
            if (_tempInx2 < 0) _tempInx2 = array.Length;
            var curPosition = (_tempInx1 + _tempInx2) / 2;
            if (array[curPosition].CompareTo(value) == 0) return curPosition;
            if (array[curPosition].CompareTo(value) < 0) _tempInx1 = curPosition;
            if (array[curPosition].CompareTo(value) > 0) _tempInx2 = curPosition;
            return BinarySearch(array, value);
        }
    }
}
