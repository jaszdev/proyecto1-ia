using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public static Population instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public Vector2 area;
    public int individualCount;
    public GameObject individual;

    public int targetFoodCount;
    public int foodCount;
    public GameObject food;

    // Enemy List
    public List<Enemy> enemies;

    // stats
    public int deaths = 0;
    public int births = 0;
    public int maxGen = 0;

    public int numTorrets;
    public Torret torret;

    void Start()
    {
        enemies = new List<Enemy>();
        // Instanciar poblacion inicial
        for (int i = 0; i < individualCount; i++)
        {
            Enemy enemy = Instantiate(individual, RandomPosition(), Quaternion.identity, transform).GetComponent<Enemy>();
            enemies.Add(enemy);
        }
        // Instanciar alimento inicial
        for (int i = 0; i < foodCount; i++)
        {
            Instantiate(food, RandomPosition(), Quaternion.identity, transform);
        }

        // Instanciar torretas
        for (int i = 0; i < numTorrets; i++)
        {
            Instantiate(torret, RandomPosition(), Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < (targetFoodCount - foodCount); i++)
        {
            Instantiate(food, RandomPosition(), Quaternion.identity, transform);
            foodCount++;
        }
    }

    public int AvgHunger()
    {
        int avgHunger = 0;

        foreach (Enemy e in enemies) avgHunger += Mathf.RoundToInt(e.hunger);
        avgHunger /= individualCount;

        return avgHunger;
    }

    public int AvgDrive()
    {
        int avgDrive = 0;

        foreach (Enemy e in enemies) avgDrive += Mathf.RoundToInt(e.drive);
        avgDrive /= individualCount;

        return avgDrive;
    }

    public int AvgHealth()
    {
        int avgHealth = 0;

        foreach (Enemy e in enemies) avgHealth += Mathf.RoundToInt(e.GetComponent<Damageable>().health);
        avgHealth /= individualCount;

        return avgHealth;
    }

    public int AvgAge()
    {
        int avgAge = 0;

        foreach (Enemy e in enemies) avgAge += Mathf.RoundToInt(e.age);
        avgAge /= individualCount;

        return avgAge;
    }

    Vector2 RandomPosition()
    {
        Vector2 rPosition = transform.position;
        rPosition += new Vector2
        {
            x = Random.Range(-area.x / 2f, area.x / 2f),
            y = Random.Range(-area.y / 2f, area.y / 2f)
        };
        return rPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireCube(transform.position, area);
    }

}
