namespace AI.Genetic
{
    public interface ITask
    {
        /// <summary>
        /// Фитнесс функция
        /// </summary>
        /// <param name="input">входные данные</param>
        /// <returns>результат выполнения функции</returns>
        double DoTask(double[] input);
    }
}