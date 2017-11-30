using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cross
{
    /// <summary>
    /// Одноточечный кросинговер
    /// 12|3 + 45|6 = 12|6 + 45|3
    /// </summary>
    public class UniformCross : ICrossingComponent
    {


        /// <summary>
        /// Одноточечный кросинговер
        /// 12|3 + 45|6 = 12|6 + 45|3 
        /// </summary>
        /// <param name="point">точка обрезания</param>
        public UniformCross()
        {

        }

  
        public Tuple<Chromosome, Chromosome> Cross(Chromosome chr1, Chromosome chr2)
        {
         
            if (chr1.Gen.Length != chr2.Gen.Length)
                throw new AI.Util.Exceptions.LogicalException(
                    "Попытка скрестить хромосомы с разным числом генов");
            Chromosome f_chr = new Chromosome(chr1.Gen.Length);
            Chromosome s_chr = new Chromosome(chr1.Gen.Length);
            Array.Copy(chr1.Gen, f_chr.Gen, chr1.Gen.Length);
            Array.Copy(chr2.Gen, s_chr.Gen, chr2.Gen.Length);
            for (int i = 0; i < chr1.Gen.Length/2; i++)
            {
                double temp = f_chr.Gen[i * 2];
                f_chr.Gen[i * 2] = s_chr.Gen[i * 2];
                s_chr.Gen[i * 2] = temp;
            }

            return new Tuple<Chromosome, Chromosome>(f_chr, s_chr);

        }

        public void Swap(ref double v1,ref double v2)
        {
            double temp = v1;
            v1 = v2;
            v2 = temp;
        }
    }
}
