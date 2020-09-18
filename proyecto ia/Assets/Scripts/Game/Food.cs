using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float value = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("enemy"))
        {
            collision.GetComponent<Enemy>().hunger -= value;
            Population.instance.foodCount--;
            Destroy(gameObject);
        }
    }

}
