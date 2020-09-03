using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDNA
{
    public Color Color { get; set; }
    public List<Vector2> Directions { get; set; } 

}


public class Enemy : MonoBehaviour, I_Individual<EnemyDNA>
{

    // I_Individual
    public EnemyDNA DNA { get; set; }
    public double Fitness { get; set; }

    // Enemy Properties
    public float Speed;
    public int NumDirections;
    public int framesPerDirection = 5;

    int frameCounter = 0;
    int directions_index = 0;

    Rigidbody2D rb;

    public I_Individual<EnemyDNA> Cross(I_Individual<EnemyDNA> other)
    {
        return null;
    }

    public void Mutate(double lowerBound, double upperBound, double chancePerGene)
    {
    }
    
    void Start()
    {
        DNA = new EnemyDNA();
        DNA.Color = Random.ColorHSV();
        DNA.Directions = new List<Vector2>();
        for (int i = 0; i < NumDirections; i++)
        {
            DNA.Directions.Add(Random.insideUnitCircle);
        }

        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = DNA.Color;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = DNA.Directions[directions_index] * Speed * Time.deltaTime;
        frameCounter++;
        if (frameCounter == framesPerDirection)
        {
            directions_index = (directions_index + 1) % DNA.Directions.Count;
            frameCounter = 0;
        }
    }


}
