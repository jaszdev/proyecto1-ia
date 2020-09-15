using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform barrel;
    public Projectile projectile;
    public float waitTime = 1f; // time between shots
    bool waiting = false;

    public void Shoot(Vector2 direction)
    {
        if (!waiting)
        {
            Projectile nProjectile = Instantiate(projectile, barrel.position, Quaternion.identity, null);
            nProjectile.SetParent(transform);
            nProjectile.Direction = direction;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }

}
