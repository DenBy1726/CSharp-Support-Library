using AI.Examples;
using AI.Genetic.Factory.Cast;
using AI.Genetic.Factory.Cross;
using AI.Genetic.Factory.Selector;
using AI.Genetic.Factory.Survive;
using AI.Util.Comparators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory
{
    /// <summary>
    /// Фабрика создаст генетический алгоритм для минимизации функции от одной переменной
    /// </summary>
    public class FittingAlgorithmFactory : DefaultGeneticAlgorithmFactory
    {


        public static GeneticAlgorithm Create(int min,int max)
        {
            GeneticAlgorithm alg = new GeneticAlgorithm();
            alg.Algorithm = CreateAlgorithm();
            alg.Cancellation = CreateCancellation();
            alg.Config = CreateConfig(min,max);
            alg.Result = CreateResultCache();
            alg.Generator = new Random();
            return alg;
        }

        public static GeneticAlgorithm.AlgorithmData CreateAlgorithm()
        {
            var Algorithm = new GeneticAlgorithm.AlgorithmData()
            {
                Casting = new Vectorization(),
                Comparator = new ChromosomeComparatorDefault(),
                Crossing = new MultiPointCross(new List<int>() { 0, 8, 16, 64 }),
                Selector = new BetterPartSellector(),
                Survival = new FittestSurvival()
            };
            return Algorithm;
        }

        public static CancellationConditions CreateCancellation()
        {
            var cancrlation = new CancellationConditions()
            {
                Iteration = 1000,
                ErrorStep = 0.000000001
            };
            return cancrlation;
        }

        public static GeneticDataConfig CreateConfig(int min, int max)
        {
            var Config = new GeneticDataConfig()
            {
                AllowFloat = true,
                ChromosomeCount = 100,
                CrossesNumber = 50,
                GenCount = 1,
                KillSame = true,
                MaxGen = max,
                MinGen = min,
                Mutation = new GeneticDataConfig.MutationConfig()
                {
                    MutationPercent = 0.1,
                    MutationRange = 0.5,
                    Strict = true,
                },
                RandomInit = true,
                ReproductionPercent = 0.5
            };
            return Config;

        }

        public static ResultCache CreateResultCache()
        {
            return new ResultCache(100);
        }
    }
}
