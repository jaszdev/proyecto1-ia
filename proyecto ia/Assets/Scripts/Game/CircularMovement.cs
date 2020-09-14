using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{

    public float rotateSpeed = 5f;
    public float radius = 1;
    public Vector2 center;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        angle = rotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = center + offset;
    }
}
