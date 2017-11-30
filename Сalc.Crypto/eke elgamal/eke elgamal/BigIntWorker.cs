using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Calc
{
    class BigIntWorker
    {
        public static bool IsPrime(BigInteger value, int maxWitnessCount = 64)
        {
            // take care of the simple cases of small primes and the
            // common composites having those primes as factors
            if (value <= BigInteger.One) return false;
            if ((value % new BigInteger(2)) == BigInteger.Zero) return value == new BigInteger(2);
            if ((value % new BigInteger(3)) == BigInteger.Zero) return value == new BigInteger(3);
            if ((value % new BigInteger(5)) == BigInteger.Zero) return value == new BigInteger(5);
            if (((value % new BigInteger(7)) == BigInteger.Zero) || ((value % new BigInteger(11)) == BigInteger.Zero)
                || ((value % new BigInteger(13)) == BigInteger.Zero) || ((value % new BigInteger(17)) == BigInteger.Zero)
                || ((value % new BigInteger(19)) == BigInteger.Zero) || ((value % new BigInteger(23)) == BigInteger.Zero)
                || ((value % new BigInteger(29)) == BigInteger.Zero) || ((value % new BigInteger(31)) == BigInteger.Zero)
                || ((value % new BigInteger(37)) == BigInteger.Zero) || ((value % new BigInteger(41)) == BigInteger.Zero)
                || ((value % new BigInteger(43)) == BigInteger.Zero))
            {
                return (value <= new BigInteger(43));
            }
            return InternalIsPrimeMR(value, maxWitnessCount, new ulong[0]);
        }

        private static bool InternalIsPrimeMR(BigInteger value, int witnessCount, params ulong[] witnesses)
        {
            // compute n − 1 as (2^s)·d (where d is odd)
            BigInteger valLessOne = value - BigInteger.One;
            BigInteger d = valLessOne / 2; // we know that value is odd and valLessOne is even, so unroll 1st iter of loop
            uint s = 1;
            while ((d % 2) == BigInteger.Zero)
            {
                d /= 2;
                s++;
            }

            Calc.Generate.Random gen = new Calc.Generate.Random(Convert.ToUInt32(DateTime.Now.Ticks % 100000000));
            // test value against each witness
            for (int i = 0; i < witnessCount; i++)
            {
                BigInteger a;
                if (i < witnesses.Length)
                {
                    a = witnesses[i];
                    if (a >= valLessOne)
                    {
                        a %= value - 3;
                        a += 3;
                    }
                }
                else
                {
                    a = gen.GetBig();
                }
                BigInteger x = BigInteger.ModPow(a, d, value);

                if (x == BigInteger.One) continue;
                for (uint r = 1; (r < s) && (x != valLessOne); r++)
                {
                    x = BigInteger.ModPow(x, 2, value);
                    if (x == BigInteger.One) return false;
                }
                if (x != valLessOne) return false;
            }
            // witnesses confirm value is prime
            return true;
        }

        public static BigInteger InverseMod(BigInteger a, BigInteger n)
        {
            BigInteger b = n, x = BigInteger.Zero, d = BigInteger.One;
            while (a.CompareTo(BigInteger.Zero) == 1)//a>0
            {
                BigInteger q = b / a;
                BigInteger y = a;
                a = b % a;
                b = y;
                y = d;
                d = x - (q * d);
                x = y;
            }
            x = x % n;
            if (x.CompareTo(BigInteger.Zero) == -1)//x<0
            {
                x = (x + n) % (n);
            }
            return x;
        }
    }
}
