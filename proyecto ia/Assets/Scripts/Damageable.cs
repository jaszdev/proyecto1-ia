using UnityEngine;

public class Damageable : MonoBehaviour
{

    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
