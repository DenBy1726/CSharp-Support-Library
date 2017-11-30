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
    public class OnePointCross : ICrossingComponent
    {
        int point;
        bool useCenter;

        /// <summary>
        /// Одноточечный кросинговер
        /// 12|3 + 45|6 = 12|6 + 45|3 
        /// </summary>
        /// <param name="point">точка обрезания</param>
        public OnePointCross(int point)
        {
            this.point = point;
            useCenter = false;
        }

        /// <summary>
        /// Одноточечный кросинговер
        /// 12|3 + 45|6 = 12|6 + 45|3 
        /// </summary>
        /// <param name="useCenter">Использовать ли центр как точку обрезания</param>
        /// <param name="point">смещение относительно центра</param>
        public OnePointCross(bool useCenter = true,int point = 0)
        {
            this.point = point;
            this.useCenter = useCenter;
        }

        public Tuple<Chromosome, Chromosome> Cross(Chromosome chr1, Chromosome chr2)
        {
         
            if (chr1.Gen.Length != chr2.Gen.Length)
                throw new AI.Util.Exceptions.LogicalException(
                    "Попытка скрестить хромосомы с разным числом генов");

            int usingPoint = point;
            if (useCenter == true)
            {
                usingPoint = (chr1.Gen.Length / 2 ) + point;
            }

            if (chr1.Gen.Length <= usingPoint || usingPoint <= 0 )
                throw new AI.Util.Exceptions.LogicalException(
                    "Точка скрещивания должна лежать в интервале (0,size)");
            Chromosome f_child = new Chromosome(chr1.Gen.Length);
            Chromosome s_child = new Chromosome(chr2.Gen.Length);

            Array.Copy(chr1.Gen, 0, f_child.Gen, 0, usingPoint);
            Array.Copy(chr2.Gen, 0, s_child.Gen, 0, usingPoint);
            Array.Copy(chr2.Gen, usingPoint, f_child.Gen, usingPoint, chr2.Gen.Length - usingPoint);
            Array.Copy(chr1.Gen, usingPoint, s_child.Gen, usingPoint, chr1.Gen.Length - usingPoint);

            return new Tuple<Chromosome, Chromosome>(f_child, s_child);

        }
    }
}
