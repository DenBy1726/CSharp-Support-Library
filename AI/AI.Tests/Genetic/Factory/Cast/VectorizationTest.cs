using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Cast;
using AI.Genetic;

namespace AI.Genetic.Factory.Cast.Test
{
    /// <summary>
    /// Сводное описание для VectorizationTest
    /// </summary>
    [TestClass]
    public class VectorizationTest
    {
     

        [TestMethod]
        public void VectorizationTest1()
        {
            Vectorization vect = new Vectorization();
            Chromosome chr = new Chromosome(2);
            chr.Gen[0] = 100;
            chr.Gen[1] = 150;

            Chromosome chr2 = vect.Preworking(chr);
            chr2 = vect.Postworking(chr2);

            CollectionAssert.AreEqual(chr.Gen, chr2.Gen);
        }
    }
}
