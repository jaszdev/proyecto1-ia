using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Damageable player;
    public float playerHealth;
    public int numHearts;

    public List<Transform> hearts;

    private void Start()
    {
        hearts = new List<Transform>(GetComponentsInChildren<Transform>());
        if (hearts.Count > 0) hearts.RemoveAt(0); // remove own transform
    }

    private void Update()
    {
        if (player == null || hearts == null || hearts.Count < numHearts) return;

        for (int i = 0; i < numHearts; i++)
            hearts[i].gameObject.SetActive(i + 1 <= player.health);
        playerHealth = player.health;
    }


}
