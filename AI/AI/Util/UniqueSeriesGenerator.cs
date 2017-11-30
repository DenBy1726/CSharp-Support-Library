using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Util
{
    public class UniqueSeriesGenerator
    {
        private HashSet<double> values;
        System.Random rnd = new System.Random();
        public UniqueSeriesGenerator(HashSet<double> values)
        {
            this.values = values;
        }

        public double[][] Generate(int count)
        {
            double[][] rez = new double[count][];
            for(int i = 0; i < count; i++)
            {
                rez[i] = Next();
            }
            return rez;
        }

        private double[] Next()
        {
            double[] series = values.ToArray();
            for(int i = 0; i < series.Length; i++)
            {
                int f = rnd.Next(0, series.Length);
                int s = rnd.Next(0, series.Length);
                Swap(ref series[f], ref series[s]);
            }
            return series;
        }

        public void Swap(ref double i,ref double j)
        {
            double buff = i;
            i = j;
            j = buff;
        }

        public HashSet<double> Values { get => values; set => values = value; }
    }
}
