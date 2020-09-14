using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player playerPrefab;
    public CameraScript mainCamera;
    // ui
    public PlayerUI playerUI;

    //"animation"
    public float respawnWaitTime = 1.5f;

    Player player;
    Damageable playerDC;
    void Start()
    {
        InstantiatePlayer();    
    }

    void Update()
    {
        if (playerDC != null && playerDC.health <= 0)
            StartCoroutine(IP_Coroutine()); // reset player
    }

    IEnumerator IP_Coroutine()
    {
        yield return new WaitForSeconds(respawnWaitTime);
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        if (player != null) Destroy(player.gameObject);
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, null);
        mainCamera.follow = player.transform;
        playerDC = player.GetComponent<Damageable>();
        playerUI.player = playerDC;
    }

    

}
