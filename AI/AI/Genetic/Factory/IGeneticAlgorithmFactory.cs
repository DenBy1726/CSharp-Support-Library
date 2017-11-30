using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic.Factory
{
    /// <summary>
    /// Интерфейс фабрики для создания генетических алгоритмов
    /// </summary>
    public interface IGeneticAlgorithmFactory
    {
        /// <summary>
        /// Создание алгоритма
        /// </summary>
        /// <returns>Экземпляр генетического алгоритма</returns>
        Genetic.GeneticAlgorithm Create();
        /// <summary>
        /// Создание конфига алгоритма
        /// </summary>
        /// <returns>Экземпляр конфига алгоритма</returns>
        Genetic.GeneticDataConfig CreateConfig();
        /// <summary>
        /// Создание условия завершения алгоритма
        /// </summary>
        /// <returns>Экземпляр конфига условий выхода</returns>
        Genetic.CancellationConditions CreateCancellation(); 
        /// <summary>
        /// Создание кеша результатов алгоритма
        /// </summary>
        /// <returns>Экземпляр кеша результатов алгортима</returns>
        Genetic.ResultCache CreateResultCache();

        /// <summary>
        ///Конструирует конфиг с реализациями частей алгоритма
        /// </summary>
        GeneticAlgorithm.AlgorithmData CreateAlgorithm();
    }
}
