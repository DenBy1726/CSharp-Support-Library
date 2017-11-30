using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cast
{
    public class Vectorization : ICastingComponent
    {
        GeneticDataConfig cfg;
        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
        }
        public Chromosome Preworking(Chromosome input)
        {
            Chromosome chr = new Chromosome(input.Gen.Length * 64);
            for(int val = 0;val<input.Gen.Length;val++)
            {
                BitArray ar = new BitArray(BitConverter.GetBytes(input.Gen[val]));
                for(int i=0;i<ar.Length;i++)
                {
                    chr.Gen[val*64 + i] = ar.Get(i) == true ? 1 : 0;
                }
            }
            return chr;
        }

        public Chromosome Postworking(Chromosome input)
        {
            Chromosome chr = new Chromosome(input.Gen.Length / 64);
            for (int val = 0; val < chr.Gen.Length; val++)
            {
                bool[] ar = new bool[64];
                for (int i = 0; i < 64; i++)
                {
                    ar[i] = input.Gen[val * 64 + i] == 1? true : false;
                }
                chr[val] = BitConverter.ToDouble(PackBoolsInByteArray(ar),0);
            }
            return chr;
        }

        public byte[] PackBoolsInByteArray(bool[] bools)
        {
            int len = bools.Length;
            int bytes = len >> 3;
            if ((len & 0x07) != 0) ++bytes;
            byte[] arr2 = new byte[bytes];
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                    arr2[i >> 3] |= (byte)(1 << (i & 0x07));
            }
            return arr2;
        }
    }
}
