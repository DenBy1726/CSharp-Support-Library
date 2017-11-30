using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Survive
{
    public class FittestSurvivalWithoutParents : ISurvival
    {
        GeneticDataConfig cfg;
        IComparer<Chromosome> comparator;

        /// <summary>
        /// Выживают самые сильнейшие особи (дети)
        /// </summary>
        public FittestSurvivalWithoutParents()
        {

        }

        public void Init(GeneticDataConfig cfg, IComparer<Chromosome> comparator)
        {
            this.cfg = cfg;
            this.comparator = comparator;
        }

        public List<Chromosome> Survive(GeneticGeneration parents, List<Chromosome> childs)
        {
            List<Chromosome> fullGenerations = new List<Chromosome>(childs);
            fullGenerations.Sort(comparator);
            if (cfg.KillSame == true)
                fullGenerations = fullGenerations.Distinct(childs[0] as IEqualityComparer<Chromosome>).ToList();
            while (fullGenerations.Count < cfg.ChromosomeCount)
                fullGenerations = fullGenerations.Concat(fullGenerations).ToList();
            return fullGenerations.GetRange(0, cfg.ChromosomeCount);
        }
    }
}

