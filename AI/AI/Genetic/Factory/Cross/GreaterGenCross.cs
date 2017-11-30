using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cross
{
    /// <summary>
    /// Выбирает больший ген из двух 
    /// </summary>
    public class GreaterGenCross : ICrossingComponent
    {
        int point;
        bool useCenter;



        /// <summary>
        /// Выбирает больший ген из двух 
        /// </summary>
        public GreaterGenCross()
        {
           
        }

        public Tuple<Chromosome, Chromosome> Cross(Chromosome chr1, Chromosome chr2)
        {
         
            if (chr1.Gen.Length != chr2.Gen.Length)
                throw new AI.Util.Exceptions.LogicalException(
                    "Попытка скрестить хромосомы с разным числом генов");

           
            Chromosome f_child = new Chromosome(chr1.Gen.Length);
            Chromosome s_child = new Chromosome(chr2.Gen.Length);

            for (int i = 0; i < chr1.Gen.Length; i++)
            {
                int better = (int)Math.Max(chr1.Gen[i], chr2.Gen[i]);
                int worst = (int)Math.Min(chr1.Gen[i], chr2.Gen[i]);
                f_child.Gen[i] = better;
                s_child.Gen[i] = better;
                //s_child.Gen[i] = worst;
            }

            return new Tuple<Chromosome, Chromosome>(f_child, s_child);

        }
    }
}
