using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Mutation
{
    public class SwapMutation : IMutationComponent
    {
        GeneticDataConfig cfg;

        Random rand = new Random();

        public void Mutate(Chromosome toMutate)
        {
            for (int i = 0; i < toMutate.Gen.Length; i++)
            {
                if (rand.NextDouble() < cfg.Mutation.MutationPercent)
                {
                    int f = rand.Next(0, toMutate.Gen.Length);
                    int s = rand.Next(0, toMutate.Gen.Length);
                    Swap(ref toMutate.Gen[f], ref toMutate.Gen[s]);
                }
            }
        }

        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
        }

        public void Swap(ref double i, ref double j)
        {
            double buff = i;
            i = j;
            j = buff;
        }
    }
}
