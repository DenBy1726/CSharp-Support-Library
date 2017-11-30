using System;

namespace AI.Genetic
{
    public class GeneticDataConfig
    { 
        /// <summary>
        /// Минимальное значение гена
        /// </summary>
        public int MinGen { get => minGen; set => minGen = value; }
        /// <summary>
        /// Максимальное значение гена
        /// </summary>
        public int MaxGen { get => maxGen; set => maxGen = value; }
        /// <summary>
        /// Количество генов
        /// </summary>
        public int GenCount { get => genCount; set => genCount = value; }
        /// <summary>
        /// Количество хромосом
        /// </summary>
        public int ChromosomeCount { get => chromosomeCount; set => chromosomeCount = value; }
    
        /// <summary>
        /// Разрешено ли использовать в качестве генов числа с плавающей точкой
        /// </summary>
        public bool AllowFloat { get => allowFloat; set => allowFloat = value; }
        /// <summary>
        /// Инициализировать ли модель случайными числами
        /// </summary>
        public bool RandomInit { get => randomInit; set => randomInit = value; }
        /// <summary>
        /// Количество скрещиваний
        /// </summary>
        public int CrossesNumber { get => crossesNumber; set => crossesNumber = value; }
        /// <summary>
        /// Вероятность скрещивания
        /// </summary>
        public double ReproductionPercent { get => reproductionPercent; set => reproductionPercent = value; }
        public MutationConfig Mutation { get => mutation; set => mutation = value; }
        /// <summary>
        /// Удалять при естественном отборе одинаковые хромосомы
        /// </summary>
        public bool KillSame { get => killSame; set => killSame = value; }

        private int minGen = int.MinValue;
        private int maxGen = int.MaxValue;
        private int genCount = 0;
        private int chromosomeCount = 0;
        private bool allowFloat = true;
        private bool randomInit = true;
        private int crossesNumber = -1;
        private double reproductionPercent = 1;
        private MutationConfig mutation = new MutationConfig();
        private bool killSame = true;

        public class MutationConfig
        {
            /// <summary>
            /// Вероятность мутации гена
            /// </summary>
            public double MutationPercent { get => mutationPercent; set => mutationPercent = value; }
            /// <summary>
            /// Диапазон разброса мутации
            /// </summary>
            public double MutationRange { get => mutationRange; set => mutationRange = value; }
            /// <summary>
            /// Может ли мутация игнорировать ограничения min,max?
            /// </summary>
            public bool Strict { get => strict; set => strict = value; }

            private double mutationPercent = 0.05;
            private double mutationRange = 0.5;
            private bool strict = false;

        }
        /// <summary>
        /// Проверка корректность значений
        /// </summary>
        public void Validate()
        {
            if (double.IsNaN(minGen))
            {
                throw new System.ArgumentException("minGene не должно равняться NaN");
            }
            if (double.IsNaN(maxGen))
            {
                throw new System.ArgumentException("maxGene не должно равняться NaN");
            }
            
            if(minGen > MaxGen)
            {
                throw new AI.Util.Exceptions.LogicalException("minGene должно быть меньше maxGene");
            }

            if(genCount < 1)
            {
                throw new System.ArgumentException("genCount должно быть больше чем 0");
            }

            if(chromosomeCount < 2)
            {
                throw new System.ArgumentException("chromosomeCount должно быть больше чем 1");
            }

            if(crossesNumber < 1)
            {
                throw new System.ArgumentException("crossesNumber должно быть больше чем 1");
            }

            if(Mutation.MutationPercent > 1 || Mutation.MutationRange < 0)
            {
                throw new System.ArgumentException("mutationPercent должно лежать в интервале [0,1]");
            }

            

        }

        /// <summary>
        /// В зависимости от конфигурации преобразует вещественное число
        /// </summary>
        /// <param name="value">число</param>
        /// <param name="floatPart">плавающая часть</param>
        public void Floating(ref double value,double floatPart = 0)
        {
            if (AllowFloat == true)
                value += floatPart;
            else
                value = Math.Round(value + floatPart);

            //если прибавление(вычитание) числа на диапазоне [0,1] выбило диапазон,
            //восстанавливаем
            /*if (value < MinGen)
                value = MinGen;
            if (value > MaxGen)
                value = MaxGen;*/
        }

        /// <summary>
        /// загоняет значение в границу допустимых значений если надо
        /// </summary>
        /// <param name="newValue"></param>
        public void DoStrict(ref double newValue)
        {
            //если проверка границ строгая
            if (Mutation.Strict == true)
            {

                if (newValue < MinGen)
                    newValue = MinGen;
                else
                    newValue = MaxGen;

            }
        }

    public override string ToString()
        {
            return Util.ToString.ReflexString(this);
        }

    }
}