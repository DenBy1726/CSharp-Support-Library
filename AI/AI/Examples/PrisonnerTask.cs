using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    class PrisonnerTask
    {

        //лучшая особь со всеми еденицами
        public double DoTask(double[] input,double[] input2)
        {
            double fitness = 0;
            for (int i = 0; i < input.Length; i++)
            {
                double a = input[i];
                double b = input2[i];

                if(a == b)
                {
                    if(a == 1)
                    {
                        fitness += 1;
                    }
                    else
                    {
                        fitness += 3;
                    }
                }
                else
                {
                    if(a > b)
                    {

                    }
                }
            }
            return fitness;
        }

    }
}
