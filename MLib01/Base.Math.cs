using System;

namespace MLib01.Math
{
    public static partial class Base
    {
        public static decimal GetFactorial(int n)
            => n > 0 ? n * GetFactorial(n - 1) : 1;

        public static int GetBiominalCoefficient(int k, int n)
        {
            if (k < 0 | n < 0) throw new ArgumentOutOfRangeException();
            if (k == 0 | k == n) return 1;
            return (int)(GetFactorial(n) / (GetFactorial(k) * GetFactorial(n - k)));
        }

        public static int GetBiominal(int a, int b, int n)
        {
            if (n == 0) throw new ArgumentOutOfRangeException();
            int aExp = n, bEx = 0;
            int result = 0;
            for (int i = 0; i < n + 1; i++)
            {
                result += GetBiominalCoefficient(i, n) * (a ^ aExp) * (b ^ bEx);
                aExp--; bEx++;
            }
            return result;
        }
    }
}
