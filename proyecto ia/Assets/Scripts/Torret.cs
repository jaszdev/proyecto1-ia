using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torret : MonoBehaviour
{
    public Transform barrel;
    public Projectile projectile;
    public float projectileSpeed;
    public float waitTime = 1f; // time between shots
    bool waiting = false;
    public float searchRadius = 5f;
    private Vector2 shootDirection;
    public float projectileLifetime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
        shootDirection = Vector2.zero;
    }
    
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);
        shootDirection = Vector2.zero;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "enemy")
            {
                shootDirection = (collider.transform.position - transform.position).normalized;
                break;
            }
        }
    }


    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Projectile p = Instantiate(projectile, barrel.position, Quaternion.identity, null);
            p.SetParent(barrel);
            p.Speed = projectileSpeed;
            yield return new WaitForSeconds(waitTime);
            while (shootDirection == Vector2.zero)
                yield return null; // no targets 

            if (p != null)
            {
                p.Direction = shootDirection;
                Destroy(p.gameObject, projectileLifetime);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }

}
