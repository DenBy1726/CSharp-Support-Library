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
    public class GeneticAlgorithmTests
    {
     
     
        [TestMethod()]
        public void GeneticAlgorithmValidateTest()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm();

            Assert.ThrowsException<NullReferenceException>((Action)alg.Validate);

            alg.Config = new GeneticDataConfig();
            Assert.ThrowsException<NullReferenceException>((Action)alg.Validate);

            alg.Algorithm.Task = new AI.Examples.TaskExample();

            alg.Validate();

            alg = Factory.DefaultGeneticAlgorithmFactory.Create(2, 2);
            alg.Algorithm.Task = new Examples.TaskExample();

            alg.Validate();
        }

        [TestMethod()]
        public void GeneticAlgorithmCompetitionTest()
        {
            GeneticDataConfig cfg = new GeneticDataConfig()
            {
                ChromosomeCount = 100,
                GenCount = 20,
                MaxGen = 1,
                MinGen = 0,
                AllowFloat = false
            };
            GeneticAlgorithm alg = Factory.RandomGenerationAlgorithmFactory.Create(cfg);
            alg.Algorithm.Task = new Examples.TaskExample();
            alg.Algorithm.Comparator = new Util.Comparators.ChromosomeComparatorDescendingDefault();

            alg.Competition();

            Assert.IsTrue(alg.CurrentGeneration[0].FitnessResult > alg.CurrentGeneration[99].FitnessResult);



        }
    }
}