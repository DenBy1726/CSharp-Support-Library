using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic;
using System.Diagnostics;

namespace AI.Examples.Tests
{
    [TestClass()]
    public class NeuralGenTrainTaskTests
    {
        [TestMethod()]
        public void DoNeuralGenTaskTest()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = false,
                    ChromosomeCount = 100,
                    GenCount = 42,
                    CrossesNumber = 100,
                    MinGen = -1,
                    MaxGen = 1,
                    RandomInit = true,
                    ReproductionPercent = 1,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        Strict = true,
                        MutationRange = 5,
                        MutationPercent = 0.2
                    }

                },
                Cancellation = new CancellationConditions()
                {
                    MoreOrEquals = 63,
                    Iteration = 5000,
                  //  TimerElapsed = new System.Timers.Timer(2000)
                    //ErrorStep = 0.0000000001
                },
                Generator = new Random(),
                Result = new ResultCache(1000000),
                Algorithm = new GeneticAlgorithm.AlgorithmData
                {
                    Comparator = new Util.Comparators.ChromosomeComparatorDescendingDefault(),
                    Crossing = new Genetic.Factory.Cross.MultiPointCross(new List<int> {0,6,12,18,30,36,42 }),
                    Selector = new Genetic.Factory.Selector.RandomUsingPrioritySellector(),
                    Mutation = new Genetic.Factory.Mutation.ChangeToRandomMutation(),
                    Survival = new Genetic.Factory.Survive.FittestSurvivalWithoutParents(),
                    Task = new Examples.NeuralGenTrainTask()
                }
            };

            alg.OnIterate += Alg_OnIterate1;

            alg.Start();

            //  using (var file = new System.IO.StreamWriter(alg.Config.GenCount + ".txt"))
            // {
            //    file.WriteLine(alg.Config.ToString());
            //    file.WriteLine("Gen     Iteration   Max     Average");

            // }
        }

        private void Alg_OnIterate1(GeneticAlgorithm.IterationEventArgs obj)
        {
            Trace.WriteLine($"{obj.BestChromosome.Gen.Length}\t{obj.CancelationValue.Iteration}\t" +
                $"{obj.Max}\t{obj.Average}");
        }

    }
}