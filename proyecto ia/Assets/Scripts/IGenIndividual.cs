using System;
using System.Collections.Generic;


public interface IGenIndividual<T> where T : ICloneable
{
    //public List<T> DNA { get; set; }
    //public int DNALength => DNA.Count;
    //public double Fitness { get; set; }
    //public void Mutate(double lowerBound, double upperBound, double chancePerGene);
    //public IGenIndividual<T> Cross(IGenIndividual<T> other);
}

