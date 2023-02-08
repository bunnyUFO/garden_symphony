using System;
using System.Collections;
using System.Collections.Generic;
using GGJ.Platform;
using RythmFramework;
using UnityEngine;

public class PlatformGripper : MonoBehaviour
{
    [Header("platform raycast position settings")]
    public Vector3 raycastPosition;
    public float rayCastDistance;
    public float rayCastRadius;
    
    [Header("edge raycast position settings")]
    public float forwardOffset;
    public float edgeRayCastDistance;
    public float edgeRayCastRadius;
    
    [Header("Collision Settings")]
    public LayerMask layerMask;
    public string platformTag;
    
    [Header("Fine Tuning Settings")]
    public float ungripDeltaThreshold;
    public float maxVelocity;

    private float ungroundedDeltaTime = 0;
    private Collider previousCollider;
    private Vector3 previousPosition;

    private void LateUpdate()
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
                Vector3 displacement = transform.position - newPosition;

                if (previousCollider == hit.collider)
                {
                    Vector3 velocity = (newPosition - previousPosition)/Time.deltaTime;
                    velocity = velocity.magnitude < maxVelocity ? velocity : velocity.normalized * maxVelocity;
                    GroundedEvents.current.PlatformUpdate(true, velocity, displacement);
                }
                else
                {
                    GroundedEvents.current.PlatformUpdate(true, Vector3.zero, displacement);
                }

                previousPosition = newPosition;
            }
            else
            {
                previousCollider = null;
                GroundedEvents.current.PlatformUpdate(false, Vector3.zero, Vector3.zero);
            }
            
        }
        else
        {
            previousCollider = null;
            ungroundedDeltaTime += Time.deltaTime;
            if (ungroundedDeltaTime > ungripDeltaThreshold) {
                GroundedEvents.current.Grounded(false);
                GroundedEvents.current.PlatformUpdate(false, Vector3.zero, Vector3.zero);
            }
        }
        
        if (Physics.SphereCast(transform.position + raycastPosition + transform.forward * forwardOffset, edgeRayCastRadius, Vector3.down, out var edgeHit, edgeRayCastDistance, layerMask))
        {
            GroundedEvents.current.LedgeUpdate(false);
        }
        else
        {
            GroundedEvents.current.LedgeUpdate(true);
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

            // position += transform.forward * forwardOffset;
            // Gizmos.color = Color.yellow;
            // Gizmos.DrawWireSphere(position + Vector3.down*0.05f, edgeRayCastRadius);
            // Gizmos.DrawLine(position, position + Vector3.down*edgeRayCastDistance);
            // Gizmos.DrawWireSphere(position + Vector3.down*(edgeRayCastDistance - 0.05f), edgeRayCastRadius);
        }
    }
}
