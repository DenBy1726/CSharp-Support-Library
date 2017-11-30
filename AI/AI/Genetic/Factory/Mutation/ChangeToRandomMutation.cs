using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Mutation
{
    public class ChangeToRandomMutation : IMutationComponent
    {
        GeneticDataConfig cfg;

        Random rand = new Random();

        public void Mutate(Chromosome toMutate)
        {
            for (int i = 0; i < toMutate.Gen.Length; i++)
            {
                if (rand.NextDouble() < cfg.Mutation.MutationPercent)
                {
                    double newValue;

                    newValue = rand.Next((int)(cfg.MinGen * cfg.Mutation.MutationRange),
                        (int)(cfg.MaxGen * cfg.Mutation.MutationRange));
                    //если новое число за диапазоном допустимых значений
                    if (newValue < cfg.MinGen || newValue > cfg.MaxGen)
                    {
                        cfg.DoStrict(ref newValue);


                        cfg.Floating(ref newValue);
                    }
                    toMutate.Gen[i] = newValue;
                }
            }
        }

        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
        }
    }
}
