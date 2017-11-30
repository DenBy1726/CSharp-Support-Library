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
    public class ResultCacheTests
    {
        ResultCache cache = new ResultCache(2);

        [TestMethod()]
        public void ResultCachePushTest()
        {
            GeneticGeneration gen = new GeneticGeneration(2, 2);
            gen[0][0] = 1;
            GeneticGeneration gen2 = new GeneticGeneration(2, 2);

            cache.Push(gen);

            List<GeneticGeneration> both = cache.Data;
            Assert.AreEqual(gen, both[0]);

            Assert.AreNotEqual(gen2, both[0]);

        }

    }
}