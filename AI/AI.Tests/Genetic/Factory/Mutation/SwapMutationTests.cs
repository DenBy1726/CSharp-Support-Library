using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Mutation.Tests
{
    [TestClass()]
    public class SwapMutationTests
    {
        [TestMethod()]
        public void SwapMutationTest()
        {
            Chromosome input = new Chromosome(5);
            Chromosome output = new Chromosome(5);
            input.Gen[0] = 1;
            output.Gen[0] = 1;

            SwapMutation mutate = new SwapMutation();

            mutate.Init(new GeneticDataConfig()
            {
                Mutation = new GeneticDataConfig.MutationConfig()
                {
                    MutationPercent = 1
                },
                GenCount = 5
            });

            mutate.Mutate(input);

            CollectionAssert.AreNotEqual(input.Gen, output.Gen);
        }

    }
}