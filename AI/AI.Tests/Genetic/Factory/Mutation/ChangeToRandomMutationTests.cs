using Microsoft.VisualStudio.TestTools.UnitTesting;
using AI.Genetic.Factory.Mutation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory.Mutation.Tests
{
    [TestClass()]
    public class ChangeToRandomMutationTests
    { 
        [TestMethod()]
        public void ChangeToRandomMutateTest()
        {
            GeneticDataConfig cfg = DefaultGeneticAlgorithmFactory.CreateConfig(1, 1);
            Chromosome chr = new Chromosome(1);
            chr.Gen[0] = 0.5;

            //Запрещаем вещественные числа и ставим диапазон [0,1]
            cfg.MinGen = 0;
            cfg.MaxGen = 1;
            cfg.AllowFloat = false;

            //мутация 100%, диапазон мутации 200000%, запрещаем игнорировать диапазон,
            //если за диапазоном то ложим на границу диапазона
            cfg.Mutation.MutationPercent = 1;
            cfg.Mutation.MutationRange = 2000;
            cfg.Mutation.Strict = true;

            ChangeToRandomMutation mutate = new ChangeToRandomMutation();
            mutate.Init(cfg);
            mutate.Mutate(chr);

            Assert.AreNotEqual(chr.Gen[0], 0.5);
            Assert.IsTrue(chr.Gen[0] == 0 || chr.Gen[0] == 1);

        }

    }
}