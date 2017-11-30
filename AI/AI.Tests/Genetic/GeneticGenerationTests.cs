using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Tests
{
    [TestClass()]
    public class GeneticGenerationTests
    {
        [TestMethod()]
        public void GeneticGenerationUsingFloatTest()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = true,
                    ChromosomeCount = 100,
                    GenCount = 20,
                    MinGen = 13,
                    MaxGen = 15
                },
                Generator = new Random()
            };
            alg.CurrentGeneration = new GeneticGeneration(alg);

            List<double> genered = new List<double>();
            for(int i=0;i < alg.Config.ChromosomeCount;i++)
            {
                for (int j = 0; j < alg.Config.GenCount; j++)
                    genered.Add(alg.CurrentGeneration[i][j]);
            }
            genered.Sort();

            Assert.IsTrue(genered[0] >= 13);
            Assert.IsTrue(genered.Last() <= 15);
            
        }

        [TestMethod()]
        public void GeneticGenerationWithoutFloatTest()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    ChromosomeCount = 100,
                    GenCount = 20,
                    MinGen = 13,
                    MaxGen = 15
                },
                Generator = new Random()
            };
            alg.CurrentGeneration = new GeneticGeneration(alg);

            List<double> genered = new List<double>();
            for (int i = 0; i < alg.Config.ChromosomeCount; i++)
            {
                for (int j = 0; j < alg.Config.GenCount; j++)
                    genered.Add(alg.CurrentGeneration[i][j]);
            }
            genered.Sort();

            Assert.IsTrue(genered[0] >= 13);
            Assert.IsTrue(genered.Last() <= 15);

        }
    }
}