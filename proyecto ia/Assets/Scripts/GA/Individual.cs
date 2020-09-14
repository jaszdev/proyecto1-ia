using System.Collections.Generic;

public abstract class Individual<T> 
{
    public T DNA { get; set; }
    public double Fitness { get; set; }
    
    public abstract Individual<T> Cross(Individual<T> other);

    public abstract void Mutate(float lowerBound, float upperBound, float chancePerGene);

}
