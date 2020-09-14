using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage;
    public LayerMask damageLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & damageLayer) != 0)
        {
            collision.gameObject.GetComponent<Damageable>()?.TakeDamage(damage);
        }
    }

}
