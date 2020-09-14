using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform follow;
    public float lerp;
    public Vector3 offset;

    // from (0, 0)
    public Vector3 bounds;

    void Update()
    {
        if (follow == null) return;

        Vector3 position = Vector3.Lerp(transform.position, follow.position + offset, lerp);
        position.x = Mathf.Clamp(position.x, -bounds.x / 2f, bounds.x / 2f);
        position.y = Mathf.Clamp(position.y, -bounds.y / 2f, bounds.y / 2f);
        transform.position = position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, bounds);
    }
}
