using System;
using System.Collections;
using System.Collections.Generic;
using GGJ.Platform;
using RythmFramework;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
    public Vector3 raycastPosition;
    public float rayCastDistance;
    public float rayCastRadius;
    public LayerMask layerMask;
    public string platformTag;
    public float ungripDeltaThreshold;
    public float maxVelocity;

    private float ungroundedDeltaTime = 0;
    private Collider previousCollider;
    private Vector3 previousPosition;

    private void FixedUpdate()
    {
        // Does the ray intersect any objects excluding the player layer
        if (Physics.SphereCast(transform.position + raycastPosition, rayCastRadius, Vector3.down, out var hit, rayCastDistance, layerMask))
        {
            ungroundedDeltaTime = 0;
            GroundedEvents.current.Grounded(true);
            
            if (hit.collider.CompareTag(platformTag))
            {
                previousCollider = hit.collider;
                Vector3 newPosition = hit.collider.transform.position;

                if (previousCollider == hit.collider)
                {
                    Vector3 velocity = (newPosition - previousPosition)/Time.deltaTime;
                    velocity = velocity.magnitude < maxVelocity ? velocity : velocity.normalized * maxVelocity;
                    GroundedEvents.current.PlatformUpdate(true, velocity);
                }

                previousPosition = newPosition;
            }
            else
            {
                previousCollider = null;
                GroundedEvents.current.PlatformUpdate(false, Vector3.zero);
            }
            
        }
        else
        {
            previousCollider = null;
            ungroundedDeltaTime += Time.deltaTime;
            if (ungroundedDeltaTime > ungripDeltaThreshold) {
                GroundedEvents.current.Grounded(false);
                GroundedEvents.current.PlatformUpdate(false, Vector3.zero);
            }
        }
    }
    
    private void OnDrawGizmos() {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 position = transform.position + raycastPosition;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position + Vector3.down*0.05f, rayCastRadius);
            Gizmos.DrawLine(position, position + Vector3.down*rayCastDistance);
            Gizmos.DrawWireSphere(position + Vector3.down*(rayCastDistance - 0.05f), rayCastRadius);
        }
    }
}
