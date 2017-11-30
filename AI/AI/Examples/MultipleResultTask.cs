using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class MultipleResultTask : ITask
    {
        public double DoTask(double[] input)
        {
            return func(input[0]);
        }

        public double func(double x)
        {
            return Math.Pow(Math.Sin(x),8.0);
        }
    }
}
