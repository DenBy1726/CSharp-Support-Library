using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory
{
    /// <summary>
    /// Фабрика создаст генетический алгоритм со случайной инициализацией и настройкой конфига из вне
    /// </summary>
    public class RandomGenerationAlgorithmFactory : DefaultGeneticAlgorithmFactory
    {
        /// <summary>
        /// Создание из конфига и установление алгоритма из фабрики по умолчанию
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static Genetic.GeneticAlgorithm Create(GeneticDataConfig cfg)
        {
            Genetic.GeneticAlgorithm ga = new GeneticAlgorithm()
            {
                Config = cfg,
                Cancellation = CreateCancellation(),
                Generator = new Random(DateTime.Now.Millisecond),
            };
            ga.CurrentGeneration = new GeneticGeneration(ga);
            return ga;
        }
    }
}
