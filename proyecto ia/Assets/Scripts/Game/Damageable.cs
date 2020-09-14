using System.Collections;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float health;
    public float destroyWaitTime = 0.5f;
    public bool is_damageable = false;
    Protection protection;

    void Start()
    {
        protection = GetComponent<Protection>();
    }

    public void TakeDamage(float damage)
    {
        if (protection != null && protection.Protecting) return;

        health -= damage;
        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }
        else if (protection != null)
        {
            protection.Protect();
        }
    }

    IEnumerator DestroyCoroutine()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy(gameObject);
    }

}
