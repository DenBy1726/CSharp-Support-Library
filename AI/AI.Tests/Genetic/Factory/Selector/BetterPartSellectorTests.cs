using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Selector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Selector.Tests
{
    [TestClass()]
    public class BetterPartSellectorTests
    {
      
        [TestMethod()]
        public void BetterPartSellectorChoiceTest()
        {
            GeneticAlgorithm alg = Factory.DefaultGeneticAlgorithmFactory.Create(100,20);
            alg.Algorithm.Selector = new Selector.BetterPartSellector();

            Chromosome succes,fail = alg.Algorithm.Selector.Choice(alg.CurrentGeneration.Chromosome);
            Assert.AreNotEqual(fail, alg.CurrentGeneration.Chromosome[1]);

            succes = alg.Algorithm.Selector.Choice(alg.CurrentGeneration.Chromosome);
            Assert.AreEqual(succes, alg.CurrentGeneration.Chromosome[1]);

            succes = alg.Algorithm.Selector.Choice(alg.CurrentGeneration.Chromosome);
            Assert.AreEqual(succes, alg.CurrentGeneration.Chromosome[1]);

            succes = alg.Algorithm.Selector.Choice(alg.CurrentGeneration.Chromosome);
            Assert.AreEqual(succes, alg.CurrentGeneration.Chromosome[2]);

        }

    }
}