using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace AI.Genetic.Factory.Selector
{
    /// <summary>
    /// Достает запись учитывая приоритеты элементов
    /// </summary>
    public class RandomUsingPrioritySellector : ICrossingSelectorComponent
    {
        Random rand = new Random();
        GeneticDataConfig cfg;
        double lambda;

        public Random Rand { get => rand; set => rand = value; }

        public RandomUsingPrioritySellector(double lambda = 1)
        {
            this.lambda = lambda;
        }

        protected int GetNext(int minVal,int maxVal)
        {
            double result;
            do
            {
                double u = Rand.NextDouble();
                double t = -Log(u) / lambda;
                double increment = (maxVal - minVal) / 6.0;
                result = minVal + (t * increment);
                
            } while (result >= maxVal);
            return (int)result;
        }

        public Chromosome Choice(Chromosome[] gen)
        {
            
            List<Chromosome> rez = new List<Chromosome>();
            int index = GetNext(0, gen.Length - 1);
            return gen[index];
        }

        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
        }

    }
}
