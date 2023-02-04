using System;
using System.Collections;
using System.Collections.Generic;
using GGJ.Platform;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
    public Vector3 raycastPosition;
    public float rayCastDistance;
    public LayerMask layerMask;

    private void LateUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position + raycastPosition, Vector3.down, out hit, rayCastDistance, layerMask))
        {
            transform.SetParent(hit.collider.transform);
        }
        else
        {
            transform.SetParent(null);
        }
    }
    
    private void OnDrawGizmos() {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 position = transform.position + raycastPosition;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position + Vector3.down*0.05f, 0.05f);
            Gizmos.DrawLine(position, position + Vector3.down*rayCastDistance);
            Gizmos.DrawSphere(position + Vector3.down*(rayCastDistance - 0.05f), 0.05f);
        }
    }
}
