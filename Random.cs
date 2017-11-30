using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calc.Generate;
using System.Numerics;

///Работа с генераторами
namespace Calc.Generate
{
    /// <summary>
    /// Интерфейс для класса, генерирующий псевдослучайный бит
    /// </summary>
    interface BitGenerator
    {
        bool GetBit();
    }
    /// <summary>
    /// LFSR Генератор, выдающий на выход бит
    /// </summary>
    class LFSR : BitGenerator
    {
        private UInt32 _shiftRegister = 1;
        public LFSR()
        {

        }

        public LFSR(UInt32 ShiftRegister)
        {
            _shiftRegister = (ShiftRegister != 0 ? ShiftRegister : 1);
        }
        /// <summary>
        /// Получает певдослучайный бит (0 или 1)
        /// </summary>
        /// <returns></returns>
        public bool GetBit()
        {
            // Любое значение кроме нуля
            _shiftRegister = ((((_shiftRegister >> 31)
                ^ (_shiftRegister >> 6)
                ^ (_shiftRegister >> 4)
                ^ (_shiftRegister >> 2)
                ^ (_shiftRegister >> 1)
                ^ _shiftRegister)
                & 0x00000001) << 31)
                | (_shiftRegister >> 1);
            return Convert.ToBoolean(_shiftRegister & 0x00000001);
        }
    }

    /// <summary>
    /// Генератор Геффе, состоящий из трех генераторов LFSR. Результатом является
    /// значение нелинейной функции от результатов генератора LFSR
    /// y = f(LFSR1,LFSR2,LFSR3)
    /// </summary>
    class GRand : BitGenerator
    {
        LFSR _lfsr;

        public GRand()
        {
            _lfsr = new LFSR();
        }
        public GRand(UInt32 initial)
        {
            _lfsr = new LFSR(initial);
        }

        /// <summary>
        /// Получает псевдослучайное значение бита (0 или 1)
        /// </summary>
        /// <returns></returns>
        public bool GetBit()
        {
            bool x1 = _lfsr.GetBit();
            bool x2 = _lfsr.GetBit();
            bool x3 = _lfsr.GetBit();
            return (x1 & x2) || (!x1 & x3);
        }

    }


    class Random : System.Security.Cryptography.RandomNumberGenerator, BitGenerator
    {
        BitGenerator _bitGenerator;
        UInt32 mod = 4294967291;
        UInt32 a = 1073741823;
        UInt32 b = 1073741823;
        UInt32 c = 12345;
        UInt32 x = 4;
        public Random()
        {
            _bitGenerator = new GRand();
        }

        public Random(UInt32 x, UInt32 bitRegister = 1)
        {
            this.x = x;
            _bitGenerator = new GRand(bitRegister);
        }
        private UInt32 NextRandomValueU()
        {
            return x = (a * x * x + b * x + c) % mod;
        }
        private Int32 NextRandomValue()
        {
            return (Int32)(x = ((a * x * x + b * x + c) % mod));
        }

        public int Get()
        {
            int retval = 0;
            do
            {
                retval = NextRandomValue();
            }
            while (_bitGenerator.GetBit() == false);
            return retval;
        }

        [Obsolete("Не безопасное использование, альтернатива GetBig(int)")]
        public BigInteger GetBig()
        {
            return GetBig(Get());
        }

        [Obsolete("Не безопасное использование, альтернатива GetBig(int)")]
        public BigInteger GetBig(int min,int max)
        {
            if (min > max)
                throw new InvalidArgumentException("Максимальное значение должно быть больше минимального");
            int n = Math.Log(max + min, 256);
            return GetBig(n) - min;
        }
      

        public double GetDouble(int order = 3)
        {
            return Math.Abs((double)Get() / Int32.MaxValue);
        }

        public override void GetBytes(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)Get(0, 255);
        }

        public override void GetNonZeroBytes(byte[] data)
        {
            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)Get(1, 255);
        }

        public bool GetBit()
        {
            return _bitGenerator.GetBit();
        }

        public BigInteger GetBig(uint maxByteLength = 100)
        {
            uint num = Get(maxByteLength);
            byte[] bigStream = new byte[num];
            GetNonZeroBytes(bigStream);
            BigInteger value = new BigInteger(bigStream);
            return value;
        }

        public BigInteger GetPrimeBig(uint maxByteLength = 100)
        {
            BigInteger value = BigInteger.Abs(GetBig(maxByteLength));
            int i = 0;
            while (!IsPrime(value + i))
            {
                i++;
            }
            return value + i;
        }

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

            Random gen = new Random(Convert.ToUInt32(DateTime.Now.Ticks % 100000000));
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

    }
}
