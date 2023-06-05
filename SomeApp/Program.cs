using System;
using MLib01;
using System.Linq;

namespace SomeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myInts = new[] { 5, 10, 2, 6, -1, 17, -43, 100, 1, 8, 0, 2, 7, 5, 6 };
            var mySortedInts = Base.QSort(myInts);
            mySortedInts.ToList().ForEach(i => Console.Write(i + ", "));
            Console.ReadLine();
        }
    }
}
