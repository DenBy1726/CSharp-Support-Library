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
    public class MultiPointCross : ICrossingComponent
    {
        List<int> point;

        /// <summary>
        /// Одноточечный кросинговер
        /// 12|3 + 45|6 = 12|6 + 45|3 
        /// </summary>
        /// <param name="point">точка обрезания</param>
        public MultiPointCross(List<int> point)
        {
            this.point = point;
           
        }


        public Tuple<Chromosome, Chromosome> Cross(Chromosome chr1, Chromosome chr2)
        {
         
            if (chr1.Gen.Length != chr2.Gen.Length)
                throw new AI.Util.Exceptions.LogicalException(
                    "Попытка скрестить хромосомы с разным числом генов");


            Chromosome f_child = new Chromosome(chr1.Gen.Length);
            Chromosome s_child = new Chromosome(chr2.Gen.Length);

          

            for(int i=0;i<point.Count-1;i++)
            {
                Array.Copy(chr1.Gen, point[i], f_child.Gen, point[i], point[i+1] - point[i]);
                Array.Copy(chr2.Gen, point[i], s_child.Gen, point[i], point[i+1] - point[i]);
            }

            return new Tuple<Chromosome, Chromosome>(f_child, s_child);

        }
    }
}
