using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic
{
    /// <summary>
    /// Предобработка и постобработка перед началом работы с хромосомой
    /// </summary>
    public interface  ICastingComponent
    {
        void Init(GeneticDataConfig cfg);

        Chromosome Preworking(Chromosome input);

        Chromosome Postworking(Chromosome input);
    }
}
