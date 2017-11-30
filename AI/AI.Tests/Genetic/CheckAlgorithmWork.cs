using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace AI.Tests
{
    [TestClass]
    public class CheckAlgorithmWork
    {

        public void CheckAlgorithmWorkTask1Example()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = false,
                    ChromosomeCount = 100,
                    GenCount = 20,
                    CrossesNumber = 50,
                    MinGen = 0,
                    MaxGen = 1,
                    RandomInit = true,
                    ReproductionPercent = 0.5,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        Strict = true,
                        MutationRange = 1,
                        MutationPercent = 0.05
                    }

                },
                Cancellation = new CancellationConditions()
                {
                    //MoreOrEquals = 20,
                    Iteration = 20,
                    //ErrorStep = 0.0000000001
                },
                Generator = new Random(),
                Result = new ResultCache(1000000),
                Algorithm = new GeneticAlgorithm.AlgorithmData
                {
                    Comparator = new Util.Comparators.ChromosomeComparatorDescendingDefault(),
                    Crossing = new Genetic.Factory.Cross.GreaterGenCross(),
                    Selector = new Genetic.Factory.Selector.BetterPartSellector(),
                  //  Mutation = new Genetic.Factory.Mutation.ChangeToRandomMutation(),
                    Survival = new Genetic.Factory.Survive.FittestSurvival(),
                    Task = new Examples.TaskExample()
                }
            };

            alg.Start();

            using (var file = new System.IO.StreamWriter(alg.Config.GenCount + ".txt"))
            {
                file.WriteLine(alg.Config.ToString());
                file.WriteLine("Gen     Iteration   Max     Average");
                for (int i = 0; i < alg.Result.Data.Count; i++)
                {
                    double max = alg.Result.Data[i].Chromosome.Max((x) => x.FitnessResult);
                    double av = alg.Result.Data[i].Chromosome.Average((x) => x.FitnessResult);
                    file.WriteLine("{0}\t{1}\t{2}\t{3}", alg.Config.GenCount, i, max, av);
                }
            }


            //  Trace.WriteLine();



        }

 
        public void CheckAlgorithmWorkTask2Example()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = false,
                    ChromosomeCount = 100,
                    GenCount = 10,
                    CrossesNumber = 50,
                    MinGen = 0,
                    MaxGen = 1,
                    RandomInit = true,
                    ReproductionPercent = 0.5,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        Strict = true,
                        MutationRange = 1,
                        MutationPercent = 0.05
                    }

                },
                Cancellation = new CancellationConditions()
                {
                    Iteration = 20,
                    //ErrorStep = 0.0000000001
                },
                Generator = new Random(),
                Result = new ResultCache(1000000),
                Algorithm = new GeneticAlgorithm.AlgorithmData
                {
                    Comparator = new Util.Comparators.ChromosomeComparatorDescendingDefault(),
                    Crossing = new Genetic.Factory.Cross.GreaterGenCross(),
                    Selector = new Genetic.Factory.Selector.BetterPartSellector(),
                  //  Mutation = new Genetic.Factory.Mutation.ChangeToRandomMutation(),
                    Survival = new Genetic.Factory.Survive.FittestSurvival(),
                    Task = new Examples.TaskExample()
                }
            };

            alg.Start();

            using (var file = new System.IO.StreamWriter(alg.Config.GenCount + ".txt"))
            {
                file.WriteLine(alg.Config.ToString());
                file.WriteLine("Gen     Iteration   Max     Average");
                for (int i = 0; i < alg.Result.Data.Count; i++)
                {
                    double max = alg.Result.Data[i].Chromosome.Max((x) => x.FitnessResult);
                    double av = alg.Result.Data[i].Chromosome.Average((x) => x.FitnessResult);
                    file.WriteLine("{0}\t{1}\t{2}\t{3}", alg.Config.GenCount, i, max, av);
                }
            }



        }


        public void CheckAlgorithmWorkTask3Example()
        {
            GeneticAlgorithm alg = new GeneticAlgorithm()
            {
                Config = new GeneticDataConfig()
                {
                    AllowFloat = false,
                    ChromosomeCount = 100,
                    GenCount = 100,
                    CrossesNumber = 50,
                    MinGen = 0,
                    MaxGen = 1,
                    RandomInit = true,
                    ReproductionPercent = 0.5,
                    KillSame = true,
                    Mutation = new GeneticDataConfig.MutationConfig()
                    {
                        Strict = true,
                        MutationRange = 1,
                        MutationPercent = 0.05
                    }

                },
                Cancellation = new CancellationConditions()
                {
                    Iteration = 20,
                    //ErrorStep = 0.0000000001
                },
                Generator = new Random(),
                Result = new ResultCache(1000000),
                Algorithm = new GeneticAlgorithm.AlgorithmData
                {
                    Comparator = new Util.Comparators.ChromosomeComparatorDescendingDefault(),
                    Crossing = new Genetic.Factory.Cross.GreaterGenCross(),
                    Selector = new Genetic.Factory.Selector.BetterPartSellector(),
                 //   Mutation = new Genetic.Factory.Mutation.ChangeToRandomMutation(),
                    Survival = new Genetic.Factory.Survive.FittestSurvival(),
                    Task = new Examples.TaskExample()
                }
            };

            alg.Start();

            using (var file = new System.IO.StreamWriter(alg.Config.GenCount + ".txt"))
            {
                file.WriteLine(alg.Config.ToString());
                file.WriteLine("Gen     Iteration   Max     Average");
                for (int i = 0; i < alg.Result.Data.Count; i++)
                {
                    double max = alg.Result.Data[i].Chromosome.Max((x) => x.FitnessResult);
                    double av = alg.Result.Data[i].Chromosome.Average((x) => x.FitnessResult);
                    file.WriteLine("{0}\t{1}\t{2}\t{3}", alg.Config.GenCount, i, max, av);
                }
            }



        }
    }
}
