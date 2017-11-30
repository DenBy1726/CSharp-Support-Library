using System;
using System.Collections.Generic;
using System.Timers;

namespace AI.Genetic
{
    public class CancellationConditions
    {
        private double moreOrEquals = double.MaxValue;
        private double lessOrEquals = double.MinValue;
        private int iteration = int.MaxValue;
        private System.Timers.Timer timerElapsed;
        private double errorStep = -1;
        private int fitnessNoChange = int.MaxValue;

        /// <summary>
        /// Условие выполнится когда значение фитнес функции для лучшей особи больше заданного
        /// </summary>
        public double MoreOrEquals { get => moreOrEquals; set => moreOrEquals = value; }
        /// <summary>
        /// Условие выполнится когда значение фитнес функции для лучшей особи меньше заданного
        /// </summary>
        public double LessOrEquals { get => lessOrEquals; set => lessOrEquals = value; }
        /// <summary>
        /// Условие достижения итерации
        /// </summary>
        public int Iteration { get => iteration; set => iteration = value; }
        /// <summary>
        /// Условие таймера
        /// </summary>
        public Timer TimerElapsed { get => timerElapsed;
            set
            {
                timerElapsed = value;
                timerElapsed.AutoReset = false;
            }
        }
        /// <summary>
        /// Условие маленького шага прогресса фитнес функции
        /// </summary>
        public double ErrorStep { get => errorStep; set => errorStep = value; }
        /// <summary>
        /// Сколько итераций фитнес функция не менялась
        /// </summary>
        public int FitnessNoChange { get => fitnessNoChange; set => fitnessNoChange = value; }

        public bool CheckCancellation(GeneticAlgorithm alg)
        {
            
            if (alg.CurrentGeneration.FirstSolution().FitnessResult >= moreOrEquals)
                return true;

            if (alg.CurrentGeneration.FirstSolution().FitnessResult <= lessOrEquals)
                return true;

            if (alg.Iteration >= iteration)
                return true;

            if (timerElapsed != null && timerElapsed.Enabled == false)
                return true;

            if (alg.Result.Length >= 2)
            {
                List<GeneticGeneration> data = alg.Result.Data;
                double firstFitness, secondFitness;
                firstFitness = Math.Abs(data[alg.Result.Length - 2].FirstSolution().FitnessResult);
                secondFitness = Math.Abs(data[alg.Result.Length - 1].FirstSolution().FitnessResult);
                if (firstFitness == secondFitness)
                    alg.ErrorNotChanged++;
                else
                    alg.ErrorNotChanged = 0;
                if (alg.ErrorNotChanged == FitnessNoChange)
                    return true;
               
                if(Math.Abs(firstFitness - secondFitness) <= errorStep)
                    return true;
            }

            return false;
        }
    }
}