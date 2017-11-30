using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic;
using System.Diagnostics;
using AI.Util.Comparators;
using AI.Genetic.Factory.Selector;
using AI.Genetic.Factory.Mutation;
using AI.Genetic.Factory.Survive;
using AI.Genetic.Factory.Cross;

namespace AI.Examples.Tests
{
    [TestClass()]
    public class Task3
    {
        public GeneticGeneration[] CurrentGeneration = new GeneticGeneration[4];
        public Chromosome[] chromosomes = new Chromosome[40];
        public IComparer<AI.Genetic.Chromosome> Comparator = new ChromosomeComparatorDescendingDefault();
        public BetterPartSellector Selector = new BetterPartSellector();
        public Random Generator = new Random();
        public IMutationComponent Mutation = new ChangeToRandomMutation();
        public GeneticDataConfig Config = new GeneticDataConfig()
        {
            Mutation = new GeneticDataConfig.MutationConfig()
            {
                MutationPercent = 0.05
            }
        };
        public ISurvival Survival = new FittestSurvival();

        public ICrossingComponent Cross = new OnePointCross();


        public virtual void Competition(GeneticGeneration generation)
        {
            int length = generation.Chromosome.Length;
            double[,] matrix = new double[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == j)
                        continue;
                    matrix[i,j] = DoTask(generation[i].Gen, generation[j].Gen);
                }
            }
            for(int i = 0; i < length; i++)
            {
                generation.Chromosome[i].FitnessResult = 0;
                for (int j = 0; j < length; j++)
                {
                   if(matrix[i,j] >= matrix[j,i])
                        generation.Chromosome[i].FitnessResult++;
                }
            }
            generation.Sort(Comparator);
        }

        public double DoTask(double[] input, double[] input2)
        {
            double fitness = 0;
            for (int i = 0; i < input.Length; i++)
            {
                double a = input[i];
                double b = input2[i];

                if (a == b)
                {
                    if (a == 1)
                    {
                        fitness += 1;
                    }
                    else
                    {
                        fitness += 3;
                    }
                }
                else
                {
                    if (a > b)
                    {
                        fitness += 5;
                    }
                    else
                    {
                        fitness += 0;
                    }
                }
            }
            return fitness;
        }

        public void Divide(Chromosome[] chromosomes)
        {
            for(int i = 0; i < CurrentGeneration.Length; i++)
            {
                CurrentGeneration[i] = new GeneticGeneration(10,64);
                for(int j = 0; j < 10; j++)
                {
                    CurrentGeneration[i].Chromosome[j] = chromosomes[i * 10 + j];
                }
            }
        }

        public List<Chromosome> Survive(Chromosome[] parents, Chromosome[] childs)
        {
            List<Chromosome> fullGenerations = new List<Chromosome>(childs);
            fullGenerations.AddRange(parents);
            fullGenerations.Sort(Comparator);
            return fullGenerations.GetRange(0, 40);
        }

        [TestMethod()]
        public void Task()
        {
            Mutation.Init(Config);
            for(int i = 0; i < 40; i++)
            {
                chromosomes[i] = new Chromosome(64);
                for(int j = 0; j < 64; j++)
                {
                    chromosomes[i][j] = Generator.Next() % 2;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                List<Chromosome> childs = new List<Chromosome>();

                Divide(chromosomes);
                foreach(var it in CurrentGeneration)
                    Competition(it);
                

                //Config.CrossesNumber скрещиваний + мутаций
                for (int j = 0; j < 20; j++)
                {

                    double reprodute;
                    Tuple<Chromosome, Chromosome> afterCross;
                    Chromosome chosen1, chosen2;

                    do
                    {
                        chosen1 = Selector.Choice(chromosomes);
                        chosen2 = Selector.Choice(chromosomes);

                        afterCross = Cross.Cross(chosen1, chosen2);

                        chosen1 = afterCross.Item1;
                        chosen2 = afterCross.Item2;

                        reprodute = Generator.NextDouble();

                    } while (0.5 <= reprodute);

                    
                        Mutation.Mutate(chosen1);
                        Mutation.Mutate(chosen2);

                        Config.DoStrict(ref chosen1.Gen[i]);
                        Config.DoStrict(ref chosen2.Gen[i]);

                    childs.Add(chosen1);
                    childs.Add(chosen2);

                }

                var childs2 = childs.ToArray();

                Divide(childs2);
                foreach (var it in CurrentGeneration)
                    Competition(it);

                chromosomes = Survive(chromosomes, childs2).ToArray();

                Trace.Write(" Iteration: " + i);
                Trace.Write(" Best: " + chromosomes[0].FitnessResult);
                Trace.Write(" Worst: " + chromosomes.Last().FitnessResult);
                double avg = chromosomes.Average((x) => x.FitnessResult);
                Trace.WriteLine(" Avg: " + chromosomes.Last().FitnessResult);
                /* if (OnIterate != null)
                 {
                     IterationEventArgs args = new IterationEventArgs()
                     {
                         BestChromosome = CurrentGeneration.FirstSolution(),
                     };
                     args.Min = CurrentGeneration.Chromosome.Min((x) => x.FitnessResult);
                     args.Max = CurrentGeneration.Chromosome.Max((x) => x.FitnessResult);
                     args.Average = CurrentGeneration.Chromosome.Average((x) => x.FitnessResult);
                     args.CancelationValue = new CancellationConditions();

                     List<GeneticGeneration> data = Result.Data;
                     if (Result.Length < 2)
                         args.CancelationValue.ErrorStep = 0;
                     else
                     {
                         double firstFitness, secondFitness;
                         firstFitness = Math.Abs(data[Result.Length - 2].FirstSolution().FitnessResult);
                         secondFitness = Math.Abs(data[Result.Length - 1].FirstSolution().FitnessResult);
                         args.CancelationValue.ErrorStep = Math.Abs(firstFitness - secondFitness);
                     }
                     args.CancelationValue.Iteration = Iteration;
                     OnIterate.Invoke(args);
                 }*/
            }
        }
    }
}