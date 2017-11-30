using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AI.Genetic
{
    public class Chromosome : IComparable<Chromosome>, IEnumerator, IEnumerable, IEqualityComparer<Chromosome>
    {
        double fitnessResult;
        double[] gen;
        int position;

        public double FitnessResult { get => fitnessResult; set => fitnessResult = value; }
        public double[] Gen { get => gen; set => gen = value; }

        public Chromosome(int count)
        {
            gen = new double[count];
        }

        public double this[int index]
        {
            get
            {
                return gen[index];
            }

            set
            {
                gen[index] = value;
            }
        }

        public int CompareTo(Chromosome other)
        {
            if (fitnessResult == other.fitnessResult && gen.SequenceEqual(other.gen))
                return 0;
            else
                return -1;
        }

        public override string ToString()
        {
            return "fitness = " + fitnessResult +" ;length = "+  gen.Length + " ;gens = { " + string.Join(",", Gen) + " };";
        }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            if (position == gen.Length - 1)
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
            get { return gen[position]; }
        }

        public void SolveTask(ITask task)
        {
            fitnessResult = task.DoTask(gen);
        }

        public bool Equals(Chromosome x, Chromosome y)
        {
            if (x == null || y == null)
                return false;
            return x == y;
        }

        public static bool operator ==(Chromosome x, Chromosome y)
        {
            if (ReferenceEquals(x,y))
                return true;
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            if (x.fitnessResult == y.fitnessResult && x.Gen.SequenceEqual(y.Gen))
                return true;
            else
                return false;
        }

        public static bool operator !=(Chromosome x, Chromosome y)
        {
            return !(x == y);
        }

        public int GetHashCode(Chromosome obj)
        {
            return obj.FitnessResult.GetHashCode();
        }
    }
}