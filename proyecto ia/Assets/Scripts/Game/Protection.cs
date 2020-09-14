using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Damageable))]
public class Protection : MonoBehaviour
{
    public float protectionTime = 1.5f;
    public int transitions = 4;
    float transitionTime;

    public Color transitionColor1;
    public Color transitionColor2;

    SpriteRenderer sr;
    Damageable damageable;

    public bool protecting = false;
    public bool Protecting => protecting;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        damageable = GetComponent<Damageable>();
        transitionTime = protectionTime / transitions;
    }

    public void Protect()
    {
        if (!protecting)
            StartCoroutine(ProtectionCoroutine());
    }

    public IEnumerator ProtectionCoroutine()
    {
        Color originalColor = sr.color;
        protecting = true;
        for (int i = 0; i < transitions / 2; i++)
        {
            sr.color = transitionColor1;
            yield return new WaitForSeconds(transitionTime);
            sr.color = transitionColor2;
            yield return new WaitForSeconds(transitionTime);
        }
        sr.color = originalColor;
        protecting = false;
    }

}
