using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class MoveRB : MonoBehaviour
{

    private Vector2 direction;
    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set => direction = value.normalized;
    }

    public float Speed;
    public float gravityScale;

    public float GravityScale
    {
        get => gravityScale;
        set
        {
            rb.gravityScale = value;
            gravityScale = value;
        }
    }
   
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (GravityScale == 0) // No gravity
        {
            rb.velocity = Direction * Speed * Time.deltaTime;
        }
        else
        {
            float vx = Direction.x == 0 ? 0 : (Direction.x / Mathf.Abs(Direction.x)) * Speed * Time.deltaTime;
            float vy = rb.velocity.y;
            //rb.velocity.Set(vx, vy);
            rb.velocity = new Vector2(vx, vy);
        }
        
    }


}
