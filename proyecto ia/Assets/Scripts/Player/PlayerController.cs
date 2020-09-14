using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum INPUT { KEYBOARD }

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }


    private void Update()
    {
        //if (Input.GetButtonDown("Jump")) player.Jump();
        player.Direction = GetDirection(INPUT.KEYBOARD);
    }

    private Vector2 GetDirection(INPUT option)
    {
        float dx = 0, dy = 0;
        switch (option)
        {
            case INPUT.KEYBOARD:
                dx = Input.GetAxis("Horizontal");
                dy = Input.GetAxis("Vertical");
                break;
            default:
                break;
        }
        return new Vector2(dx, dy);
    }
    


}
