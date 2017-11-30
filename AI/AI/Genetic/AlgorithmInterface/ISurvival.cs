using System.Collections.Generic;

namespace AI.Genetic
{
    public interface ISurvival
    {
        List<Chromosome> Survive(GeneticGeneration parents, List<Chromosome> childs);

        void Init(GeneticDataConfig cfg, IComparer<Chromosome> comparator);
    }
}