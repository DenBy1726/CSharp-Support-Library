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
    public class LuckyDogTests
    {
        ITask task = new LuckyDog();
        [TestMethod()]
        public void LuckyDogTest()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Algorithm = new GeneticAlgorithm.AlgorithmData()
                {
                    Task = task,
                    Comparator = new ChromosomeComparatorDefault(),
                    Survival = new FittestSurvival(),
                    Crossing = new UniformCross(),
                    Mutation = new ChangeToRandomMutation(),
                    Selector = new RandomSelector()
                },
                Config = new GeneticDataConfig()
                {
                    ChromosomeCount = 20,
                    GenCount = 1000,
                    MaxGen = 10,
                    MinGen = 0,
                    AllowFloat = false,
                    RandomInit = true,
                    CrossesNumber = 100,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        MutationPercent = 0.05,
                        MutationRange = 5
                    }
                },
                Cancellation = new CancellationConditions()
                {
                    LessOrEquals = 0

                }
            };

            alg.OnIterate += Alg_OnIterate;
            alg.Start();
        }

        private void Alg_OnIterate(GeneticAlgorithm.IterationEventArgs obj)
        {

            if (obj.CancelationValue.Iteration == 5)
            {
                Trace.WriteLine("");
                int curX = 500, curY = 500;
                var input = obj.BestChromosome.Gen;
                for (int i = 0; i < input.Length / 4; i++)
                {
                    curY -= (int)input[i * 4 + 0];
                    curX += (int)input[i * 4 + 1];
                    curY += (int)input[i * 4 + 2];
                    curX -= (int)input[i * 4 + 3];
                    Trace.WriteLine($"{curX}\t{curY}");
                }
            }
             /*if (obj.CancelationValue.Iteration == 0)
             {
             Trace.WriteLine("");
                 int curX = 500, curY = 500;
                 var input = obj.BestChromosome.Gen;
                 for (int i = 0; i < input.Length / 4; i++)
                 {
                     curY -= (int)input[i * 4 + 0];
                     curX += (int)input[i * 4 + 1];
                     curY += (int)input[i * 4 + 2];
                     curX -= (int)input[i * 4 + 3];
                     Trace.WriteLine($"{curX}\t{curY}");
                 }
             }*/
            /*  Trace.WriteLine($"{obj.CancelationValue.Iteration}\t" +
                  $"{obj.BestChromosome.FitnessResult}");*/
            /*Trace.WriteLine($"{obj.CancelationValue.Iteration}\t" + 
                obj.Chromosomes.Where(x => x.FitnessResult == 0).Count());*/
        }

    }
}