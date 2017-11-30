using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLP_Multilayer
{
    public class NeuroComparator : IComparer<KeyValuePair<int,AI.Neuro.MLP.NeuroImage<double>>>
    {

        public int Compare(KeyValuePair<int, AI.Neuro.MLP.NeuroImage<double>> x, 
            KeyValuePair<int, AI.Neuro.MLP.NeuroImage<double>> y)
        {
            if (x.Key > y.Key)
                return 1;
            else if (x.Key < y.Key)
                return -1;
            else
                return 0;       
        }
    }
}
