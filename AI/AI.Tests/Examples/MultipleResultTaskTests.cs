using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic;
using AI.Genetic.Factory.Cast;
using System.Diagnostics;
using AI.Util.Comparators;
using AI.Genetic.Factory.Survive;
using AI.Genetic.Factory.Cross;
using AI.Genetic.Factory.Mutation;
using AI.Genetic.Factory.Selector;

namespace AI.Examples.Tests
{
    [TestClass()]
    public class MultipleResultTaskTests
    {
        [TestMethod()]
        public void MultiplyFitness()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Algorithm = new GeneticAlgorithm.AlgorithmData()
                {
                    Task = new MultipleResultTask(),
                    Casting = new FitnessSharing(20),
                    Comparator = new ChromosomeComparatorDefault(),
                    Survival = new FittestSurvival(),
                    Crossing = new MultiPointCross(new List<int>()
                    {
                        1,3,5,7,9,11,13,15,17,19
                    }),
                    Mutation = new ChangeToRandomMutation(),
                    Selector = new RandomSelector()
                },
                Config = new GeneticDataConfig()
                {
                    ChromosomeCount = 10,
                    GenCount = 1,
                    MaxGen = 10,
                    MinGen = -10,
                    AllowFloat = true,
                    RandomInit = true,
                    CrossesNumber = 5,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        MutationPercent = 0.05,
                    }
                },
                Cancellation = new CancellationConditions()
                {
                    Iteration = 10
                }
            };

            alg.OnIterate += Alg_OnIterate;
            alg.Start();

            
        }

        private void Alg_OnIterate(GeneticAlgorithm.IterationEventArgs obj)
        {
            Trace.WriteLine($"{obj.CancelationValue.Iteration}\t" +
                $"{obj.Average}");
        }
    }
}