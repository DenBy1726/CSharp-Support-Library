using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cast
{
    public class Unification : ICastingComponent
    {
        GeneticDataConfig cfg;
        HashSet<double> set;
        public Unification(HashSet<double> set)
        {
            this.set = set;
        }

        public void Init(GeneticDataConfig cfg)
        {
            this.cfg = cfg;
        }

        public Chromosome Postworking(Chromosome chr)
        {

            var setCopy = new HashSet<double>(set);
            List<int> Collisions = new List<int>();
            for (int i = 0; i < chr.Gen.Length; i++)
            {
                if (setCopy.Contains(chr.Gen[i]))
                {
                    setCopy.Remove(chr.Gen[i]);
                }
                else
                {
                    Collisions.Add(i);
                }
            }

            if (Collisions.Count == 0)
                return chr;

            double[] notUsed = setCopy.ToArray();

            if (notUsed.Length != Collisions.Count)
            {
                throw new Exception("Невозможно составить уникальное отображение при кросинговере." +
                    "Вероятно множество допустимых значение шире или уже чем размер хромосомы");
            }

            for (int i = 0; i < notUsed.Length; i++)
            {
                chr.Gen[Collisions[i]] = notUsed[i];
            }

            return chr;

        }

        public Chromosome Preworking(Chromosome input)
        {
            return input;
        }
    }
}
