using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDNA
{
    public Color Color { get; set; }
    public float[] Weights { get; set; }

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
    public float senseRadius = 5f;
    public Vector2[] senses; // 0: player 1: projectile 2: enemies, 3: food
    public LayerMask playerLayer;
    public LayerMask projetileLayer;
    public LayerMask enemiesLayer;
    public LayerMask foodLayer;
    
    int numWeights = 4;

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
        senses = new Vector2[numWeights];
        DNA.Weights = new float[numWeights];

        for (int i = 0; i < numWeights; i++)
        {
            senses[i] = new Vector2();
            DNA.Weights[i] = Random.Range(-1f, 1f);
        }

        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = DNA.Color;
    }
    
    private void FixedUpdate()
    {
        // Reset senses 
        for (int i = 0; i < numWeights; i++)  senses[i] = new Vector2();
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, senseRadius);
        foreach (Collider2D collider in colliders)
        {
            int sense_index = -1;
            if (((1 << collider.gameObject.layer) & playerLayer) != 0) sense_index = 0;
            else if (((1 << collider.gameObject.layer) & projetileLayer) != 0) sense_index = 1;
            else if (((1 << collider.gameObject.layer) & enemiesLayer) != 0) sense_index = 2;
            else if (((1 << collider.gameObject.layer) & foodLayer) != 0) sense_index = 3;

            if (sense_index != -1)
                senses[sense_index] = (collider.transform.position - transform.position).normalized;
        }

        rb.velocity = GetDirection() * Speed * Time.deltaTime;
    }

    Vector2 GetDirection()
    {
        Vector2 direction = new Vector2();

        for (int i = 0; i < numWeights; i++)
        {
            direction += senses[i] * DNA.Weights[i]; 
        }
        direction.Normalize();
        return direction;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }


}
