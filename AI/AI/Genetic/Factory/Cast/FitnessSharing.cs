using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cast
{
    public class FitnessSharing : ICastingComponent
    {
        GeneticDataConfig cfg;
        int value;
        int afterCastCount;

        public FitnessSharing(int afterCastCount)
        {
            this.afterCastCount = afterCastCount;
        }

        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
            value = Math.Max(Math.Abs(cfg.MaxGen), Math.Abs(cfg.MinGen));


        }
        public Chromosome Preworking(Chromosome input)
        {
            int castDeliter = (int)Math.Pow(2,afterCastCount-1) - 1;
            double floatGen = input.Gen[0];
            int intGen = (int)(floatGen / value * castDeliter);
            bool sign = intGen >= 0;
            Chromosome chr = new Chromosome(afterCastCount);
            for(int i = 1; i < afterCastCount; i++)
            {
                chr[afterCastCount-i] = Math.Abs(intGen % 2);
                intGen /= 2;
            }
            chr[0] = sign ? 0 : 1;
            return chr;
        }

        public Chromosome Postworking(Chromosome input)
        {
            int rez = 0;
            int castDeliter = (int)Math.Pow(2, afterCastCount - 1) - 1;
            for (int i = 0; i < input.Gen.Length-1; i++)
            {
                if(input.Gen[afterCastCount - i - 1] == 1)
                    rez += (int)Math.Pow(2,i);
            }
            if (input[0] == 1)
                rez = -rez;
            Chromosome chr = new Chromosome(1)
            {
                [0] = (double)rez / castDeliter * value
            };
            return chr;
        }
    }
}
