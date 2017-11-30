using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Selector
{
    /// <summary>
    /// отбираются лучшие записи в порядке:
    /// 1 - 2
    /// 2 - 3
    /// 3 - 4 
    /// ...
    /// </summary>
    public class RandomSelector : AI.Genetic.ICrossingSelectorComponent
    {
        Random r = new Random();

        /// <summary>
        /// отбираются лучшие записи в порядке:
        /// 1 - 2
        /// 2 - 3
        /// 3 - 4 
        /// ...
        /// </summary>
        public RandomSelector()
        {
        }

        public Chromosome Choice(Chromosome[] gen)
        {
            return gen[r.Next(0,gen.Length)];
        }

        public void Init(GeneticDataConfig cfg)
        {
        }
    }
}
