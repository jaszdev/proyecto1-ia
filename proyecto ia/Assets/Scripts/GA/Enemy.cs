using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDNA
{
    public Color Color { get; set; }
    public List<float[]> coefs;

    public EnemyDNA(int numSenses, int degrees)
    {
        coefs = new List<float[]>();

        for (int i = 0; i < numSenses; i++)
            coefs.Add(new float[degrees + 1]);
    }

}


public class Enemy : MonoBehaviour, I_Individual<EnemyDNA>
{
    public static float MutatationRate = 0.05f;


    // I_Individual
    public EnemyDNA DNA { get; set; }
    public double Fitness { get; set; }

    // Enemy Properties
    public float Speed;
    public float senseRadius = 5f;
    public Vector2[] senses; // 0: player 1: projectile 2: enemies, 3: food
    public LayerMask playerLayer;
    public LayerMask projetileLayer;
    public LayerMask enemiesLayer;
    public LayerMask foodLayer;

    int numSenses = 4;
    int degrees = 10;

    float[] state;

    Rigidbody2D rb;
    Damageable damageable;

    public float hunger = 0;
    public float hungerIncrease;
    public float drive = 0;
    public float driveIncrease;
    public int gen = 0;
    public float reproductionCoolDown = 10f;
    public bool coolingDown = true;

    public Enemy enemyPrefab;

    public float age = 0f;

    List<Color> colors;

    void Start()
    {
        senses = new Vector2[numSenses];

        state = new float[numSenses];
        // 0: health
        // 1: constante = -1
        // 2: drive
        // 3: hunger
        //for (int i = 0; i < numSenses; i++) state[i] = 1;
        state[1] = -1;

        rb = GetComponent<Rigidbody2D>();
        
        damageable = GetComponent<Damageable>();

        StartCoroutine(CooldownCoroutine());

        if (gen == 0)
        {

            DNA = new EnemyDNA(numSenses, degrees);
            DNA.Color = Random.ColorHSV();

            for (int i = 0; i < numSenses; i++)
            {
                for (int j = 0; j < degrees + 1; j++)
                {
                    DNA.coefs[i][j] = Random.Range(-10f, 10f);
                }
            }
        }
        GetComponent<SpriteRenderer>().color = DNA.Color;

        Mutate(-10f, 10f, MutatationRate);

        colors = new List<Color>();
        // 0: player 1: projectile 2: enemies, 3: food
        colors.Insert(0, Color.red);
        colors.Insert(1, Color.yellow);
        colors.Insert(1, Color.green);
        colors.Insert(1, Color.blue);
    }

    public I_Individual<EnemyDNA> Cross(I_Individual<EnemyDNA> other)
    {
        Enemy child = new Enemy();

        child.DNA = new EnemyDNA(numSenses, degrees);
        child.DNA.Color = Random.value < 0.5f ? DNA.Color : other.DNA.Color;

        for (int i = 0; i < numSenses; i++)
        {
            for (int j = 0; j < degrees + 1; j++)
            {
                DNA.coefs[i][j] = Random.value < 0.5f ? DNA.coefs[i][j] : other.DNA.coefs[i][j];
            }
        }

        return child;
    }

    public void Mutate(float lowerBound, float upperBound, float chancePerGene)
    {
        for (int i = 0; i < numSenses; i++)
        {
            for (int j = 0; j < degrees + 1; j++)
            {
                if (Random.value < chancePerGene)
                    DNA.coefs[i][j] = MutateStrategy1(lowerBound, upperBound);
            }
        }
    }

    // 1: Cambiar gen totalmente
    float MutateStrategy1(float lowerBound, float upperBound)
    {
        return Random.Range(lowerBound, upperBound);
    }

    // 2: Cambia gen por un delta determinado
    float MutateStrategy2(float coeff, float delta)
    {
        return coeff + Random.Range(-delta, delta);
    }

    void Update()
    {
        age += Time.deltaTime;
        // 0: player 1: projectile 2: enemies, 3: food

        state[0] = damageable.health;
        hunger += hungerIncrease;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        if (hunger > 30) damageable.TakeDamage(0.05f * (hunger / 100f)); // castigo por pasar hambre
        state[3] = hunger;
        drive += driveIncrease;
        drive = Mathf.Clamp(drive, 0f, 100f);
        state[2] = drive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (coolingDown) return;
        if (collision.gameObject.tag.Equals("enemy") && drive > 40)
        {
            drive = 0;
            StartCoroutine(CooldownCoroutine());

            // Probabilidad de Reproducirse 
            if (Random.value < 0.5f) return;

            // Reproduccion
            I_Individual<EnemyDNA> i_child = Cross(collision.gameObject.GetComponent<Enemy>());
            Vector3 offset = Random.insideUnitCircle * 2f;
            Enemy child = Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity, null);
            // Set child
            child.gen = gen + 1;
            child.DNA = i_child.DNA;

            print("A child is born!");
            Population.instance.individualCount++;
            Population.instance.births++;
            if (child.gen > Population.instance.maxGen) Population.instance.maxGen = child.gen;
            Population.instance.enemies.Add(child);
        }
    }

    IEnumerator CooldownCoroutine()
    {
        coolingDown = true;
        yield return new WaitForSeconds(reproductionCoolDown);
        coolingDown = false;
    }

    private void FixedUpdate()
    {
        // Reset senses 
        for (int i = 0; i < numSenses; i++) senses[i] = new Vector2();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, senseRadius);
        foreach (Collider2D collider in colliders)
        {
            int sense_index = -1;
            if (((1 << collider.gameObject.layer) & playerLayer) != 0) sense_index = 0;
            else if (((1 << collider.gameObject.layer) & projetileLayer) != 0) sense_index = 1;
            else if (((1 << collider.gameObject.layer) & enemiesLayer) != 0) sense_index = 2;
            else if (((1 << collider.gameObject.layer) & foodLayer) != 0) sense_index = 3;

            if (sense_index != -1)
                senses[sense_index] = (collider.transform.position - transform.position);
        }

        rb.velocity = GetDirection() * Speed * Time.deltaTime;
    }


    // Metodo donde se combina el adn del individuo
    // y sus sentidos para decidir una direccion  de movimiento.
    Vector2 GetDirection()
    {
        Vector2 direction = new Vector2();

        for (int i = 0; i < numSenses; i++)
        {
            float f_i = GAMath.EvaluatePolynomial(DNA.coefs[i], state[i]);
            direction += senses[i] * GAMath.SigmoidFunction(f_i);
        }
        direction.Normalize();
        return direction;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, senseRadius);

        for (int i = 0; i < numSenses; i++)
        {
            Gizmos.color = colors[i];
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + senses[i]);
        }
    }


}
