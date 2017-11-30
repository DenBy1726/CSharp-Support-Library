namespace AI.Genetic
{
    public interface IMutationComponent
    {
        void Mutate(Chromosome toMutate);

        void Init(GeneticDataConfig cfg);
    }
}