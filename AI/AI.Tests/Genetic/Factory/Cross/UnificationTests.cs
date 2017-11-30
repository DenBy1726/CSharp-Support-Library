using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Cross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Genetic.Factory.Cast;

namespace AI.Genetic.Factory.Cross.Tests
{
    [TestClass()]
    public class UnificationTests
    {
        [TestMethod()]
        public void UniqueCrossTest()
        {
            double[] input1 = new double[]
            {
                2,3,4,5
            };

            double[] input2 = new double[]
           {
                4,3,5,2
           };

            double[] output1 = new double[]
            {
                2,3,5,4
            };

            double[] output2 = new double[]
           {
                4,3,2,5
           };

            Unification cast = new Unification(new HashSet<double>()
            {
                2,3,4,5
            });

            var rez = cast.Postworking(new Chromosome(4)
            {
                Gen = input1
            });

            var rez2 = cast.Postworking(new Chromosome(4)
            {
                Gen = input2
            });

            CollectionAssert.AreEqual(rez.Gen, output1);
            CollectionAssert.AreEqual(rez2.Gen, output2);
        }

      
    }
}