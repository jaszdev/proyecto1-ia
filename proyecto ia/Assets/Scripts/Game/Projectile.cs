using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MoveRB
{
    public float damage;
    public LayerMask damageLayer;
    

    Transform parent;
    public void SetParent(Transform parent) => this.parent = parent;

    protected override void Start()
    {
        base.Start();
        //Destroy(gameObject, timeToDestroy);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float waitDestroy = 0.05f;
        if (((1 << collision.gameObject.layer) & damageLayer) != 0)
        {
            collision.GetComponent<Damageable>()?.TakeDamage(damage);
            Destroy(gameObject, waitDestroy);
        }
        else if (collision.transform != parent) Destroy(gameObject, waitDestroy);

    }
}
