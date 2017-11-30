using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Tests
{
    [TestClass()]
    public class CancellationConditionsTests
    {
        [TestMethod()]
        public void CheckCancellationTest()
        {
            GeneticAlgorithm alg = Factory.DefaultGeneticAlgorithmFactory.Create(2,2);
            CancellationConditions cond = alg.Cancellation;


            GeneticGeneration temp1 = new GeneticGeneration(2, 2);
            alg.Result.Push(temp1);
            alg.Result.Data[0][0].FitnessResult = 1;

            GeneticGeneration temp2 = new GeneticGeneration(2, 2);
            alg.Result.Push(temp2);
            alg.Result.Data[1][0].FitnessResult = 0.5;

            Assert.IsFalse(cond.CheckCancellation(alg));

            
            alg.Result.Data[1][0].FitnessResult = alg.Result.Data[0][0].FitnessResult - cond.ErrorStep;
            Assert.IsTrue(cond.CheckCancellation(alg));

            cond.ErrorStep = -1;
            alg.CurrentGeneration = temp1;
            cond.MoreOrEquals = 1;
            Assert.IsTrue(cond.CheckCancellation(alg));

            cond.MoreOrEquals = double.MaxValue;
            cond.LessOrEquals = 1;
            Assert.IsTrue(cond.CheckCancellation(alg));

            cond.LessOrEquals = double.MinValue;
            Assert.IsFalse(cond.CheckCancellation(alg));
      
            Assert.IsFalse(cond.CheckCancellation(alg));

        }
    }
}