using System;

namespace AI.Genetic
{
    public interface ICrossingComponent
    {
        Tuple<Chromosome,Chromosome> Cross(Chromosome chr1, Chromosome chr2);
    }
}