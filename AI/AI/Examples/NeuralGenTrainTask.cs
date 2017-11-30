using AI.Genetic;
using AI.Neuro.MLP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Examples
{
    public class NeuralGenTrainTask : ITask
    {

        double[][] input = new double[][]
        {
                new double[]{1,1,1,1,1,1 },
                new double[]{-1,1,1,1,1,1 },
                new double[]{1,-1,1,1,1,1 },
                new double[]{-1,-1,1,1,1,1 },
                new double[]{1,1,-1,1,1,1 },
                new double[]{-1,1,-1,1,1,1 },
                new double[]{1,-1,-1,1,1,1 },
                new double[]{-1,-1,-1,1,1,1 },
                new double[]{1,1,1,-1,1,1 },
                new double[]{-1,1,1,-1,1,1 },
                new double[]{1,-1,1,-1,1,1 },
                new double[]{-1,-1,1,-1,1,1 },
                new double[]{1,1,-1,-1,1,1 },
                new double[]{-1,1,-1,-1,1,1 },
                new double[]{1,-1,-1,-1,1,1 },
                new double[]{-1,-1,-1,-1,1,1 },
                new double[]{1,1,1,1,-1,1 },
                new double[]{-1,1,1,1,-1,1 },
                new double[]{1,-1,1,1,-1,1 },
                new double[]{-1,-1,1,1,-1,1 },
                new double[]{1,1,-1,1,-1,1 },
                new double[]{-1,1,-1,1,-1,1 },
                new double[]{1,-1,-1,1,-1,1 },
                new double[]{-1,-1,-1,1,-1,1 },
                new double[]{1,1,1,-1,-1,1 },
                new double[]{-1,1,1,-1,-1,1 },
                new double[]{1,-1,1,-1,-1,1 },
                new double[]{-1,-1,1,-1,-1,1 },
                new double[]{1,1,-1,-1,-1, 1},
                new double[]{-1,1,-1,-1,-1, 1},
                new double[]{1,-1,-1,-1,-1, 1},
                new double[]{-1,-1,-1,-1,-1,1 },
                new double[]{1,1,1,1,1,-1 },
                new double[]{-1,1,1,1,1,-1 },
                new double[]{1,-1,1,1,1,-1 },
                new double[]{-1,-1,1,1,1,-1 },
                new double[]{1,1,-1,1,1,-1 },
                new double[]{-1,1,-1,1,1,-1 },
                new double[]{1,-1,-1,1,1,-1 },
                new double[]{-1,-1,-1,1,1,-1 },
                new double[]{1,1,1,-1,1,-1 },
                new double[]{-1,1,1,-1,1,-1 },
                new double[]{1,-1,1,-1,1,-1 },
                new double[]{-1,-1,1,-1,1,-1 },
                new double[]{1,1,-1,-1,1,-1 },
                new double[]{-1,1,-1,-1,1,-1 },
                new double[]{1,-1,-1,-1,1,-1 },
                new double[]{-1,-1,-1,-1,1,-1 },
                new double[]{1,1,1,1,-1,-1 },
                new double[]{-1,1,1,1,-1,-1 },
                new double[]{1,-1,1,1,-1,-1 },
                new double[]{-1,-1,1,1,-1,-1 },
                new double[]{1,1,-1,1,-1,-1 },
                new double[]{-1,1,-1,1,-1,-1 },
                new double[]{1,-1,-1,1,-1,-1 },
                new double[]{-1,-1,-1,1,-1,-1 },
                new double[]{1,1,1,-1,-1,-1 },
                new double[]{-1,1,1,-1,-1,-1 },
                new double[]{1,-1,1,-1,-1,-1 },
                new double[]{-1,-1,1,-1,-1,-1 },
                new double[]{1,1,-1,-1,-1,-1 },
                new double[]{-1,1,-1,-1,-1,-1 },
                 new double[]{1,-1,-1,-1,-1,-1 },
                new double[]{-1,-1,-1,-1,-1,-1 },

        };

        int bar = 0;

        MultiLayerNeuralNetwork network;
        public NeuralGenTrainTask()
        {
            ISingleLayer<double>[] layers = new ISingleLayer<double>[2];
            layers[0] = new SingleLayer(6, 6, new Neuro.MLP.ActivateFunction.BipolarTreshhold(), new Random());
            layers[1] = new SingleLayer(6, 1, new Neuro.MLP.ActivateFunction.BipolarTreshhold(), new Random());
            MultiLayer mLayer = new MultiLayer(layers);
            DifferintiableLearningConfig config = new DifferintiableLearningConfig(new Neuro.MLP.ErrorFunction.HalfEuclid());
            config.Step = 0.1;
            config.OneImageMinError = 0.01;
            config.MinError = 0.5;
            config.MinChangeError = 0.0000001;
            config.UseRandomShuffle = true;
            config.MaxEpoch = 10000;
            SimpleBackPropogation learn = new SimpleBackPropogation(config);
            network = new MultiLayerNeuralNetwork(mLayer, learn);

        }

        public double DoTask(double[] input)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    network.Layer[0].Neuron[i].Weights[j] = input[i * 6 + j];
                }
                network.Layer[0].Neuron[i].Offset = 0;
            }
            for (int i = 0; i < 6; i++)
            {
                network.Layer[1].Neuron[0].Weights[i] = input[36 + i];
               
            }
            network.Layer[1].Neuron[0].Offset = 0;
            int fit = 0;
            for (int i = 0; i < this.input.Length; i++)
            {
                var rez = network.Compute(this.input[i]);
                double sTarget = this.input[i].Sum(x => (x == -1 ? 0 : x)) + 1;
                double target = (sTarget % 2 )*2 - 1;
                if((rez[0] > 0 && target > 0)|| (rez[0] < 0 && target < 0))
                {
                    fit++;
                }
              
            }
            return fit;
        }
    }
}
