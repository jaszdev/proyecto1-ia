using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MoveRB
{
    [Range(1, 20)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public string groundLayerName;

    new Collider2D collider;

    public bool IsTouchingGround => Physics2D.IsTouchingLayers(collider, LayerMask.GetMask(groundLayerName));

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    if (rb.velocity.y < 0) rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    //    else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    //}

    public void Jump()
    {
        // check for player touching ground layer
        if (IsTouchingGround)
            rb.velocity = Vector2.up * jumpVelocity;
    }

}
