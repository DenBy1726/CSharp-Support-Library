using AI.Genetic.Factory.Cross;
using AI.Genetic.Factory.Selector;
using AI.Genetic.Factory.Survive;
using AI.Util.Comparators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Genetic
{
    public class GeneticAlgorithm
    {
        public GeneticDataConfig Config;
        public CancellationConditions Cancellation;
        public Random Generator;
        public ResultCache Result;
        public GeneticGeneration CurrentGeneration;

        public int ErrorNotChanged = 0;

        public event Action<IterationEventArgs> OnIterate;

        public class IterationEventArgs : EventArgs
        {
            public Chromosome BestChromosome;
            public Chromosome[] Chromosomes;
            public double Min, Max, Average;
            public CancellationConditions CancelationValue;

        }
        
        /// <summary>
        /// Компоненты алгоритма
        /// </summary>
        public class AlgorithmData
        {
            /// <summary>
            /// Компоненты алгоритма
            /// </summary>
            public AlgorithmData()
            {

            }

            public IMutationComponent Mutation;
            public ICrossingComponent Crossing;
            public ICrossingSelectorComponent Selector;
            public ITask Task;
            public IComparer<Chromosome> Comparator;
            public ISurvival Survival;
            public ICastingComponent Casting;

            public virtual void Validate()
            {
                if (Selector == null)
                    Selector = new BetterPartSellector();
                if (Task == null)
                    throw new System.NullReferenceException("Task не может быть null");
                if (Comparator == null)
                    Comparator = new ChromosomeComparatorDescendingDefault();
                if (Crossing == null)
                    Crossing = new OnePointCross();
                if (Survival == null)
                    Survival = new FittestSurvival();
            }
        }

        public AlgorithmData Algorithm = new AlgorithmData();

       

        private int iteration = 0;


        public GeneticAlgorithm()
        {
     
        }

        public int Iteration { get => iteration;}

        /// <summary>
        /// выполнить заданное количество итераций
        /// </summary>
        /// <param name="count"></param>
        public virtual void Iterate(uint count = 1)
        {

            for (int i = 0; i < count; i++)
            {
                List<Chromosome> childs = new List<Chromosome>();

                Competition();
                Result.Push(CurrentGeneration);
                InvokeIteration(CurrentGeneration.Chromosome.ToList());

                //Config.CrossesNumber скрещиваний + мутаций
                for (int j = 0; j < Config.CrossesNumber; j++)
                {

                    double reprodute;
                    Tuple<Chromosome, Chromosome> afterCross;
                    Chromosome chosen1, chosen2;

                    do
                    {
                        chosen1 = Algorithm.Selector.Choice(CurrentGeneration.Chromosome);
                        chosen2 = Algorithm.Selector.Choice(CurrentGeneration.Chromosome);

                        if (Algorithm.Casting != null)
                        {
                            chosen1 = Algorithm.Casting.Preworking(chosen1);
                            chosen2 = Algorithm.Casting.Preworking(chosen2);
                        }

                        afterCross = Algorithm.Crossing.Cross(chosen1, chosen2);

                        chosen1 = afterCross.Item1;
                        chosen2 = afterCross.Item2;

                        reprodute = Generator.NextDouble();

                    } while (Config.ReproductionPercent <= reprodute);

                    if (Algorithm.Mutation != null)
                    {
                        Algorithm.Mutation.Mutate(chosen1);
                        Algorithm.Mutation.Mutate(chosen2);
                    }

                    if (Algorithm.Casting != null)
                    {
                        chosen1 = Algorithm.Casting.Postworking(chosen1);
                        chosen2 = Algorithm.Casting.Postworking(chosen2);
                    }


                    for (int k = 0; k < chosen1.Gen.Length; k++)
                    {
                        Config.DoStrict(ref chosen1.Gen[i]);
                        Config.DoStrict(ref chosen2.Gen[i]);
                    }


                    chosen1.SolveTask(Algorithm.Task);
                    chosen2.SolveTask(Algorithm.Task);

                    childs.Add(chosen1);
                    childs.Add(chosen2);

                }

                List<Chromosome> newGeneration =
                    Algorithm.Survival.Survive(CurrentGeneration, childs);

                if (newGeneration.Count > Config.ChromosomeCount)
                    newGeneration.RemoveRange(Config.ChromosomeCount,
                        newGeneration.Count - Config.ChromosomeCount);
                else if (newGeneration.Count < Config.ChromosomeCount)
                    throw new AI.Util.Exceptions.LogicalException("Размер популяции после естественного отбора" +
                        "меньше чем заданная в конфиге. Вероятно ошибка в алгоритме Survival");

                CurrentGeneration = new GeneticGeneration(this)
                {
                    Chromosome = newGeneration.ToArray()
                };
                iteration++;

            }
        }

        private void InvokeIteration(List<Chromosome> newGeneration)
        {
            if (OnIterate != null)
            {
                IterationEventArgs args = new IterationEventArgs()
                {
                    BestChromosome = CurrentGeneration.FirstSolution(),
                };
                args.Min = CurrentGeneration.Chromosome.Min((x) => x.FitnessResult);
                args.Max = CurrentGeneration.Chromosome.Max((x) => x.FitnessResult);
                args.Average = CurrentGeneration.Chromosome.Average((x) => x.FitnessResult);
                args.CancelationValue = new CancellationConditions();

                List<GeneticGeneration> data = Result.Data;
                if (Result.Length < 2)
                    args.CancelationValue.ErrorStep = 0;
                else
                {
                    double firstFitness, secondFitness;
                    firstFitness = Math.Abs(data[Result.Length - 2].FirstSolution().FitnessResult);
                    secondFitness = Math.Abs(data[Result.Length - 1].FirstSolution().FitnessResult);
                    args.CancelationValue.ErrorStep = Math.Abs(firstFitness - secondFitness);
                }
                args.CancelationValue.Iteration = Iteration;
                args.CancelationValue.FitnessNoChange = ErrorNotChanged;
                args.Chromosomes = newGeneration.ToArray();
                OnIterate.Invoke(args);
            }
        }

        /// <summary>
        /// Решает задачу для каждой хромосомы и сортирует их в порядке качетсва результата
        /// </summary>
        public virtual void Competition()
        {
            foreach(Chromosome indv in CurrentGeneration)
            {
                indv.SolveTask(Algorithm.Task);
            }
            CurrentGeneration.Sort(Algorithm.Comparator);
        }

        /// <summary>
        /// запуск алгоритма до тех пор пока не выполнится условие выхода
        /// </summary>
        public virtual void Start()
        {
            Init();
            
            do
            {
                Iterate();
            } while (Cancellation.CheckCancellation(this) == false);
            Result.Push(CurrentGeneration);
            InvokeIteration(CurrentGeneration.Chromosome.ToList());
        }

        public void Init()
        {
            Validator();
            if (Cancellation.TimerElapsed != null)
                Cancellation.TimerElapsed.Start();
            if (CurrentGeneration == null)
                CurrentGeneration = new GeneticGeneration(this);
            Algorithm.Selector.Init(Config);
            if(Algorithm.Mutation != null)
                Algorithm.Mutation.Init(Config);
            Algorithm.Survival.Init(Config,Algorithm.Comparator);
            if (Algorithm.Casting != null)
                Algorithm.Casting.Init(Config);
            if (Config.CrossesNumber == 0)
                Config.CrossesNumber = Config.ChromosomeCount;
        }

        /// <summary>
        /// Валидация компонентов
        /// </summary>
        protected virtual void Validator()
        {
            try
            {
                Validate();
            }
            catch(Exception e)
            {
                throw new Util.Exceptions.ValidationException("Ошибка при проверке GeneticAlgorithm", e);
            }
            try
            {
                Config.Validate();
            }
            catch(Exception e)
            {
                throw new Util.Exceptions.ValidationException("Ошибка при проверке Config", e);
            }

            
        }

        /// <summary>
        /// Валидация сборки генетического алгоритма
        /// </summary>
        public virtual void Validate()
        {
            if (Config == null)
                throw new System.NullReferenceException("Config не может быть null/n" +
                    "Совет: Вы можете использовать IO.Genetic.Factory.DefaultGeneticAlgorithmFactory " +
                    "для автогенерации конфига по умолчанию");
            if (Cancellation == null)
                Cancellation = new CancellationConditions()
                {
                    ErrorStep = 0.00001,
                    Iteration = 1000,
                };
            if (Generator == null)
                Generator = new Random();
            if (Result == null)
                Result = new ResultCache(2000);

            Algorithm.Validate();

        }

     


    }
}
