using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic;
using AI.Genetic.Factory.Cast;
using AI.Examples;
using AI.Util.Comparators;
using AI.Genetic.Factory.Cross;
using System.Collections.Generic;
using AI.Genetic.Factory.Selector;
using AI.Genetic.Factory.Survive;
using AI.Genetic.Factory;

namespace AI.Genetic.Tests
{
    [TestClass]
    public class FittingTaskTest
    {
        [TestMethod]
        public void FittingTask()
        {
            GeneticAlgorithm alg = FittingAlgorithmFactory.Create(-5,5);
            alg.Algorithm.Task = new FitnessFunctionTask();       

            alg.Start();
        }
    }
}
