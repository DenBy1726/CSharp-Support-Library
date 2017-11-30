using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic;
using AI.Genetic.Factory.Cast;
using AI.Util.Comparators;
using AI.Genetic.Factory.Survive;
using AI.Genetic.Factory.Cross;
using AI.Genetic.Factory.Mutation;
using AI.Genetic.Factory.Selector;
using System.Diagnostics;

namespace AI.Examples.Tests
{
    [TestClass()]
    public class ReductionTaskTests
    {
        int pointsCount = 10;
        int pointDimension = 3;
        int maxDim = 2;

        [TestMethod()]
        public void ReductionTaskTest()
        {
            ReductionTask task = new ReductionTask(pointsCount, pointDimension, maxDim);

            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = true,
                    ChromosomeCount = 20,
                    CrossesNumber = 40,
                    GenCount = pointsCount * 2,
                    KillSame = true,
                    MinGen = 0,
                    MaxGen = maxDim,
                    RandomInit = true,
                    Mutation = new GeneticDataConfig.MutationConfig
                    {
                        MutationPercent = 0.05,
                        MutationRange = 10,
                        Strict = true
                    }

                },
                Cancellation = new CancellationConditions()
                {
                    Iteration = 5000
                },
                Algorithm = new GeneticAlgorithm.AlgorithmData()
                {
                    Selector = new BetterPartSellector(),
                    Task = task,
                    Crossing = new UniformCross(),
                    Mutation = new ChangeToRandomMutation(),
                    Survival = new FittestSurvival(),
                    Comparator = new ChromosomeComparatorDefault()
                }
            };

            alg.OnIterate += Alg_OnIterate;
            alg.Start();
            for(int i = 0; i < alg.CurrentGeneration.Chromosome[0].Gen.Length; i += 2)
            {
                Trace.WriteLine($"{alg.CurrentGeneration.Chromosome[0].Gen[i]}\t" +
                    $"{alg.CurrentGeneration.Chromosome[0].Gen[i+1]}");
            }

        }

        private void Alg_OnIterate(GeneticAlgorithm.IterationEventArgs obj)
        {
            Trace.WriteLine($"{obj.CancelationValue.Iteration}\t" +
                  $"{obj.BestChromosome.FitnessResult}");
        }
    }
}