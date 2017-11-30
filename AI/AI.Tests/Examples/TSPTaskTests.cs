using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using AI.Util;
using AI.Genetic;
using AI.Genetic.Factory;
using AI.Genetic.Factory.Cross;
using AI.Genetic.Factory.Cast;
using AI.Genetic.Factory.Mutation;

namespace AI.Examples.Tests
{
    [TestClass()]
    public class TSPTaskTests
    {
        const int TOWNS = 8;
        const int CHROMOSOMES = 10;
        [TestMethod()]
        public void TSPTaskTest()
        {
            
            TSPTask task = new TSPTask();
            Debug.WriteLine("Точки:");
            HashSet<double> seriesSet = new HashSet<double>();
            int temp = 1;
            foreach(var p in task.Points)
            {
                Debug.WriteLine("{0}\t{1}", p.X, p.Y);
                seriesSet.Add(temp++);
            }
            Trace.WriteLine("");

            seriesSet.Remove(1);
            var series = new UniqueSeriesGenerator(seriesSet).Generate(CHROMOSOMES);

            GeneticAlgorithm alg = DefaultGeneticAlgorithmFactory.Create(TOWNS - 1,CHROMOSOMES);

            alg.Config.AllowFloat = false;
            alg.Config.RandomInit = false;
            alg.Config.MinGen = 2;
            alg.Config.MaxGen = TOWNS;
            alg.Cancellation.FitnessNoChange = 5;
            alg.Config.Mutation.MutationPercent = 0.05;

            alg.Config.CrossesNumber = 100;

            alg.Algorithm.Crossing = new OnePointCross(true);
            alg.Algorithm.Casting = new Unification(seriesSet);
            alg.Algorithm.Mutation = new SwapMutation();
            alg.Algorithm.Task = task;

            for(int i = 0; i < series.Length; i++)
            {
                alg.CurrentGeneration.Chromosome[i] = new Chromosome(TOWNS - 1)
                {
                    Gen = series[i]
                };
            }

            alg.OnIterate += Alg_OnIterate;
            alg.Start();

            Trace.WriteLine("");

            for (int i = 0; i < task.Dimension.GetLength(0); i++)
            {
                for (int j = 0; j < task.Dimension.GetLength(1); j++)
                {
                    Trace.Write(task.Dimension[i, j] + "\t");
                }
                Trace.WriteLine("");
            }

            Trace.WriteLine("");

            for (int i = 0; i < alg.Result.Data[0].Chromosome[0].Gen.Length; i++)
            {
                int num = (int)alg.Result.Data[0].Chromosome[0].Gen[i];
                Trace.WriteLine($"{task.Points[num-1].X}\t{task.Points[num-1].Y}");
            }
            Trace.WriteLine("");
            for (int i = 0; i < alg.CurrentGeneration.Chromosome[0].Gen.Length; i++)
            {
                int num = (int)alg.CurrentGeneration.Chromosome[0].Gen[i];
                Trace.WriteLine($"{task.Points[num - 1].X}\t{task.Points[num - 1].Y}");
            }
            Trace.WriteLine("");
        }

        private void Alg_OnIterate(GeneticAlgorithm.IterationEventArgs obj)
        {
            Trace.WriteLine($"{obj.CancelationValue.Iteration}\t" +
               $"{obj.BestChromosome.FitnessResult}");
        }
    }
}