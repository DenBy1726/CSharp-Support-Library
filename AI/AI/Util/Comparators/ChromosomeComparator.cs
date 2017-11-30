using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic;

namespace AI.Util.Comparators
{


    public class ChromosomeComparatorDefault : IComparer<AI.Genetic.Chromosome>
    {
        /// <summary>
        /// По возрастанию критерия оценки
        /// </summary>
        public ChromosomeComparatorDefault()
        {

        }
        public int Compare(Chromosome x, Chromosome y)
        {
            return x.FitnessResult.CompareTo(y.FitnessResult);
        }
    }

    /// <summary>
    /// По убыванию критерия оценки
    /// </summary>
    public class ChromosomeComparatorDescendingDefault : IComparer<AI.Genetic.Chromosome>
    {
        public int Compare(Chromosome x, Chromosome y)
        {
            return -x.FitnessResult.CompareTo(y.FitnessResult);
        }
    }
}
