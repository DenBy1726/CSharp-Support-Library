using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class TaskExample : ITask
    {
        //лучшая особь со всеми еденицами
        public double DoTask(double[] input)
        {
            double fitness = 0;
            for(int i=0;i < input.Length; i++)
            {
                if (input[i] == 1)
                    fitness++;
            }
            return fitness;
        }
    }
}
