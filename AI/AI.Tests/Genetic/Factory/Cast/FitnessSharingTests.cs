using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Cast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cast.Tests
{
    [TestClass()]
    public class FitnessSharingTests
    {
        [TestMethod()]
        public void FitnessSharingTest()
        {
            double[] inputs = new double[]
            {
                -0.71
            };
            double results;
            
            double[][] chromosomes = new double[][]
            {
                new double[]
                {
                    1,0,1,0,1,1
                }
            };
            double[] targets = new double[6];

            FitnessSharing share = new FitnessSharing(6);

            share.Init(new GeneticDataConfig()
            {
                MaxGen = 2,
                MinGen = -2
            });

            for (int i = 0; i < inputs.Length; i++)
            {
                Chromosome chr = new Chromosome(1)
                {
                    Gen = new double[]
                    {
                        inputs[i]
                    }
                };
                targets = share.Preworking(chr).Gen;

                CollectionAssert.AreEqual(targets, chromosomes[i]);

                Chromosome chr2 = new Chromosome(6)
                {
                    Gen = targets
                };

                results = share.Postworking(chr2).Gen[0];

                Assert.AreEqual(Math.Round(results,2), Math.Round(inputs[i],2));
            }
        }

    }


}
