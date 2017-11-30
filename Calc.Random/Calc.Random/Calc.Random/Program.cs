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


    class Program
    {


        static void Main(string[] args)
        {
            // Program a = new Program();

            byte[] val = new byte[100000];
            uint x = Convert.ToUInt32(DateTime.Now.Ticks % 100000000);
            Random gen = new Random(x);
            gen.GetBytes(val);
            BigInteger N, E, D, S;
            Calc.Crypto.ECPRSA.GenerateKey(out N, out E, out D);
            S = Calc.Crypto.ECPRSA.ComputeS(val, D, N);

            bool value = Calc.Crypto.ECPRSA.CheckECP(val, S, N, E);

        }
    }
}
