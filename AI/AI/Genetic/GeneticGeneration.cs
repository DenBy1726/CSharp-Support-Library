using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AI.Genetic
{
    public class GeneticGeneration : IComparable<GeneticGeneration> ,IEnumerator, IEnumerable
    {
        private Chromosome[] chromosome;

        private int position = -1;

        public GeneticGeneration(int count,int genCount)
        {
            Chromosome = new Genetic.Chromosome[count];
            for(int i=0;i < count;i++)
            {
                Chromosome[i] = new Genetic.Chromosome(genCount);
            }
        }

        public Chromosome this[int index]
        {
            get
            {
                return Chromosome[index];
            }

            set
            {
                Chromosome[index] = value;
            }
        }

        public GeneticGeneration(Random generator,GeneticDataConfig cfg)
        {
            int count = cfg.ChromosomeCount;
            int genCount = cfg.GenCount;
            Chromosome = new Genetic.Chromosome[count];
            for (int i = 0; i < count; i++)
            {
                Chromosome[i] = new Genetic.Chromosome(genCount);
                if (cfg.RandomInit == false)
                    continue;

                for(int j=0;j < genCount;j++)
                {
                    double random = generator.Next((int)cfg.MinGen,(int)cfg.MaxGen);
                    cfg.Floating(ref random, generator.NextDouble());
                    Chromosome[i].Gen[j] = random;
                }
            }
        }

        public GeneticGeneration(GeneticAlgorithm alg) : this(alg.Generator, alg.Config)
        {

        }


        public int CompareTo(GeneticGeneration other)
        {
            if (Chromosome.SequenceEqual(other.Chromosome) == true)
                return 0;
            else
                return -1;
        }

        public Chromosome FirstSolution()
        {
            return Chromosome[0];
        }

       
        public void Sort(IComparer<AI.Genetic.Chromosome> comparer)
        {
            Array.Sort(Chromosome,comparer);
        }

        public override string ToString()
        {
            return "better = { " + FirstSolution() +" } ;length = " + Chromosome.Length + ";";
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            if (position == Chromosome.Length - 1)
            {
                Reset();
                return false;
            }

            position++;
            return true;
        }

        //IEnumerable
        public void Reset()
        { position = -1; }

        //IEnumerable
        public object Current
        {
            get { return Chromosome[position]; }
        }

        public Chromosome[] Chromosome { get => chromosome; set => chromosome = value; }
    }
}