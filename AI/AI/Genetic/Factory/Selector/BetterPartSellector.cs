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
    public class BetterPartSellector : AI.Genetic.ICrossingSelectorComponent
    {
        int current = 0;
        bool wasUsed = false;

        /// <summary>
        /// отбираются лучшие записи в порядке:
        /// 1 - 2
        /// 2 - 3
        /// 3 - 4 
        /// ...
        /// </summary>
        public BetterPartSellector()
        {
        }

        public Chromosome Choice(Chromosome[] gen)
        {
            if (wasUsed == false)
            {
                wasUsed = true;
            }
            else
            {
                wasUsed = false;
                if (current < gen.Length - 1)
                    current++;
                else
                    current = 0;
            }
            return gen[current];
        }

        public void Init(GeneticDataConfig cfg)
        {
        }
    }
}
