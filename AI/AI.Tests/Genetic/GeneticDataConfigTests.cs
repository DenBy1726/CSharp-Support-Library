using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Util.Exceptions;
namespace AI.Genetic.Tests
{
    [TestClass()]
    public class GeneticDataConfigTests
    {
        [TestMethod()]
        public void DataConfigValidateTest()
        {
            GeneticDataConfig cfg = new GeneticDataConfig();

            /*   cfg.MinGen = double.NaN;
               Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);

                cfg.MinGen = 0;
               cfg.MaxGen = double.NaN;
               Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);*/

            cfg.MinGen = 0;
            cfg.MaxGen = -1;
            Assert.ThrowsException<LogicalException>((Action)cfg.Validate);

            cfg.MaxGen = 1;

            Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);

            cfg.GenCount = 1;
            cfg.ChromosomeCount = 1;
            Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);

            cfg.ChromosomeCount = 2;
            //  cfg.MutationPercent = 2;
            //   Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);

            //     cfg.MutationPercent = 0.1;

            Assert.ThrowsException<ArgumentException>((Action)cfg.Validate);
            cfg.CrossesNumber = cfg.ChromosomeCount / 2;

            cfg.Validate();
        }

        [TestMethod()]
        public void GeneticDataConfigFloatingTest()
        {
            GeneticDataConfig cfg = new GeneticDataConfig();
            cfg.AllowFloat = true;
            cfg.MinGen = 0;
            cfg.MaxGen = 1;

            double testValue = 0;
            double testFloat = -0.3;

            cfg.Floating(ref testValue, testFloat);

            Assert.AreEqual(testValue, 0);

            cfg.Floating(ref testValue, -testFloat);

            Assert.AreEqual(testValue, 0.3);

            cfg.AllowFloat = false;

            cfg.Floating(ref testValue, testFloat);

            Assert.AreEqual(testValue, 0);

            cfg.Floating(ref testValue, -2*testFloat);

            Assert.AreEqual(testValue, 1);






        }
    }
}