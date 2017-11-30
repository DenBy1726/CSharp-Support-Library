using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic
{
    public interface ICrossingSelectorComponent
    {
        void Init(GeneticDataConfig cfg);

        /// <summary>
        /// Выбор элемента из коллекции
        /// </summary>
        /// <param name="gen">коллекция</param>
        /// <returns>коллекция после выбора из нее элементов</returns>
        Chromosome Choice(Chromosome[] gen);
    }
}
