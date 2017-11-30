using System.Collections.Generic;

namespace AI.Genetic
{
    public class ResultCache
    {
        public List<GeneticGeneration> Data;

        private int capacity;

        private int length;

        public ResultCache(int count)
        {
            Data = new List<GeneticGeneration>();
            length = 0;
        }

        public int Capacity { get => capacity; set => capacity = value; }
        public int Length { get => length; set => length = value; }

        public void Push(GeneticGeneration data)
        {
            if(Data.Count == Capacity)
            {
                Data.Clear();
                length = 0;
            }
            Data.Add(data);
            length++;
        }

        
    }
}