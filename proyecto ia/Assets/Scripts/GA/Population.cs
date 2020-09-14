using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    public Vector2 area;
    public int individualCount;
    public GameObject individual;

    void Start()
    {
        for (int i = 0; i < individualCount; i++)
        {
            Instantiate(individual, RandomPosition(), Quaternion.identity, transform);
        }
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
