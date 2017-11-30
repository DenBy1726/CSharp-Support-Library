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
    public class RandomUsingPrioritySellectorTests
    {
        Dictionary<int, int> freq = new Dictionary<int, int>();
        [TestMethod()]
        public void RandomUsingPrioritySellectorChoiceTest()
        {
            GeneticAlgorithm alg = Factory.DefaultGeneticAlgorithmFactory.Create(100, 20);
            var selector = new Selector.RandomUsingPrioritySellector();
            selector.Rand = new Random(8);
            alg.Algorithm.Selector = selector;
            

            for(int i=0;i < 1000;i++)
            {
                var temp = alg.Algorithm.Selector.Choice(alg.CurrentGeneration.Chromosome);

                AddValue(
                    Array.FindIndex(alg.CurrentGeneration.Chromosome, (x) => x.Equals(temp))
                );
            }

            var list = freq.OrderBy((x) => x.Key).ToList();

            for(int i=0;i < list.Count-1;i++)
            {
                Assert.IsTrue(list[i].Value >= list[i + 1].Value);
            }



        }

        public void AddValue(int value)
        {
            if(freq.ContainsKey(value) == true)
            {
                freq[value]++;
            }
            else
            {
                freq[value] = 1;
            }
        }

    }
}