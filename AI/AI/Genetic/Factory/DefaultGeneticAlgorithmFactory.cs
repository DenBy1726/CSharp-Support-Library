using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory
{

    public class DefaultGeneticAlgorithmFactory
    {
        /// <summary>
        /// Конструирует генетический алгоритм по умолчанию. Для запуска необходимо определить поле Task
        /// </summary>
        /// <param name="geneCount">количество генов</param>
        /// <param name="chromosomeCount">количество хромосом</param>
        /// <returns>Готовый алгоритм</returns>
        public static GeneticAlgorithm Create(int geneCount,int chromosomeCount)
        {
            GeneticAlgorithm ga = new GeneticAlgorithm()
            {
                Config = CreateConfig(geneCount, chromosomeCount),
                Cancellation = CreateCancellation(),
                Generator = new Random(DateTime.Now.Millisecond),
                Algorithm = CreateAlgorithm()
            };
            ga.CurrentGeneration = new GeneticGeneration(ga);
            return ga;
        }

        /// <summary>
        /// Конструирует конфиг с настройкой основных этапов алгоритма
        /// </summary>
        /// <returns></returns>
        public static GeneticAlgorithm.AlgorithmData CreateAlgorithm()
        {
            GeneticAlgorithm.AlgorithmData alg = new GeneticAlgorithm.AlgorithmData()
            {
                Selector = new Selector.BetterPartSellector(),
                Crossing = new Cross.OnePointCross(),
                Comparator = new Util.Comparators.ChromosomeComparatorDefault(),
                Mutation = new Mutation.ChangeToRandomMutation(),
                Survival = new Survive.FittestSurvival()
            };
            return alg;
        }

        /// <summary>
        /// Конструирует конфиг с реализациями частей алгоритма
        /// </summary>
        /// <param name="geneCount">количество генов</param>
        /// <param name="chromosomeCount">количество хромосом</param>
        /// <returns></returns>
        public static GeneticDataConfig CreateConfig(int geneCount, int chromosomeCount)
        {
            GeneticDataConfig config = new GeneticDataConfig()
            {
                GenCount = geneCount,
                ChromosomeCount = chromosomeCount,
                CrossesNumber = geneCount/2
            };
            return config;
        }


        /// <summary>
        /// Конструирует условие завершения алгоритма по умолчанию 
        /// </summary>
        /// <returns></returns>
        public static Genetic.CancellationConditions CreateCancellation()
        {
            CancellationConditions cond = new CancellationConditions()
            {
                Iteration = 1000
                
            };
           // cond.TimerElapsed.Elapsed += TimerElapsed_Elapsed;
            return cond;
        }

        private static void TimerElapsed_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            (sender as System.Timers.Timer).Stop();
        }

    }
}
