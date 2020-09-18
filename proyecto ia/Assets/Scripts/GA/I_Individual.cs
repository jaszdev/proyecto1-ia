using System;
using System.Collections.Generic;


public interface I_Individual<T> 
{
    T DNA { get; set; }
    double Fitness { get; set; }
    void Mutate(float lowerBound, float upperBound, float chancePerGene);
    I_Individual<T> Cross(I_Individual<T> other);
}

