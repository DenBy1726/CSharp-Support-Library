using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int x, y;
            int c = Calc.Algorithm.Eiler(25);
            List<int> d = Calc.Algorithm.Factorization(256);
            UInt64 e = Calc.Algorithm.Fib(3);
        }
    }
}

namespace Calc
{
    static class Algorithm
    {
        public static int GCD(int a, int b) 
        {
            while (b != 0)
            {
                //a = a/b;
                //b = a%b;
                b = a % (a = b);
            }
           
            return a;
        }

        public static int GCD(int a,int b,out int x,out int y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1,out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        public static int Pow(int a, int n)
        {
            int res = 1;
            while (n !=0)
            {
                if ((n % 2) != 0)
                    res *= a;
                a *= a;
                n /= 2;
            }
            return res ;
        }

        public static int Eiler(int a)
        {
            int res = a;
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                
                if (a % i == 0)
                {
                    while (a % i == 0)
                    {
                        a /= i;
                        res -= res/i;
                    }
                }
            }
            return res;           
        }

        public static bool IsPrime(UInt64 a)
        {
            double end = Math.Sqrt(a);
            for (UInt64 i = 0; i <= end; i++)
            {
                if (a % i == 0)
                    return false;
            }
            return true;
        }

        public static List<int> Factorization(UInt64 a)
        {
            if (a == 0 || a == 1)
                return new List<int>() { Convert.ToInt32(a) };
            List<int> rez = new List<int>();
            double end = Math.Sqrt(a);
            for (UInt64 i = 2; i  <= end; i++)
            {
                if (a % i == 0)
                {
                    while (a % i == 0)
                    {
                        a /= i;
                        rez.Add(Convert.ToInt32(i));
                    }
                }
            }
            if (a != 1)
                rez.Add(Convert.ToInt32(a));
            return rez;    
        }

        public static List<int> PrimeTo(int a)
        {
            if (a == 0)
                return null;
            if (a == 1)
                return new List<int> { 1 };
            if( a ==2)
                return new List<int> { 1 ,2};
            BitArray flag = new BitArray(a-2, true);
            flag.Set(0, true);
            List<int> rez = new List<int>();
            rez.Add(1);
            rez.Add(2);
            for (int i = 3; i*i <= a; i += 2)
            {
                if (flag[i-3])
                {
                    rez.Add(i);
                    if(Convert.ToInt64(i*i) <= a)
                    {
                        for(int j = i*i; j <=a;j+=i)
                            flag[j-3] = false;
                    }
                }
            }
            return rez;

        }

        public static UInt64 Fib(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            UInt64 a = 0, b = 1;
            for (int i = 1; i < n; i++)
            {
                b += a;
                a = b - a;
            }
            return b;
        }
        
    }
}
