using System.Collections.Generic;
using UnityEngine;

public class ImageIndividual : Individual<Vector3>
{
    public ImageIndividual()
    {
        DNA = new List<Vector3>();
    }

    public ImageIndividual(int pixelCount)
    {
        DNA = new List<Vector3>();
        for (int i = 0; i < pixelCount; i++)
        {
            Color randColor = Random.ColorHSV();
            Vector3 colorVector = new Vector3(randColor.r, randColor.g, randColor.b);
            DNA.Add(colorVector);
        }
    }

    public override Individual<Vector3> Cross(Individual<Vector3> other)
    {
        ImageIndividual newIndividual = new ImageIndividual();

        int genes = DNA_Length;
        for (int i = DNA_Length; i < genes; i++)
        {
            Vector3 gene = Random.value < 0.5 ? DNA[i] : other.DNA[i];
            newIndividual.DNA.Add(gene);
        }

        return newIndividual;
    }

    public override void Mutate(float lowerBound, float upperBound, float chancePerGene)
    {
        int genes = DNA_Length;
        for (int i = 0; i < genes; i++)
        {
            if (Random.value > chancePerGene) continue;

            Vector3 gene = DNA[i];
            gene.x = Mathf.Clamp(gene.x + Random.Range(lowerBound, upperBound), 0, 255);
            gene.y = Mathf.Clamp(gene.y + Random.Range(lowerBound, upperBound), 0, 255);
            gene.z = Mathf.Clamp(gene.z + Random.Range(lowerBound, upperBound), 0, 255);
        }
    }

}
