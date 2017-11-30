using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Cross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Cross.Tests
{
    [TestClass()]
    public class OnePointCrossTests
    {
        int point = 1;
        [TestMethod()]
        public void OnePointCrossTest()
        {
            Chromosome chr1 = new Chromosome(3)
            {
                Gen = new double[] { 1.0, 2.0, 3.0 }
            };

            Chromosome chr2 = new Chromosome(3)
            {
                Gen = new double[] { 4.0, 5.0, 6.0 }
            };

            var alg = new Cross.OnePointCross(point);

            var rez = alg.Cross(chr1, chr2);

            Assert.IsTrue(rez.Item1.Gen.SequenceEqual(new double[] { 1.0, 5.0, 6.0 }));
            Assert.IsTrue(rez.Item2.Gen.SequenceEqual(new double[] { 4.0, 2.0, 3.0 }));

            alg = new Cross.OnePointCross();

            var rez2 = alg.Cross(rez.Item1, rez.Item2);

            Assert.IsTrue(rez2.Item1 == chr1);
            Assert.IsTrue(rez2.Item2 == chr2);

        }
    }
}