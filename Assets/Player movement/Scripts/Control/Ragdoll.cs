using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;
    private const float momentumMultiplier = 5f;

    private void Start()
    {
        allColliders = GetComponentsInChildren<Collider>();
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in allColliders) {
            if (collider.gameObject.CompareTag("Ragdoll")) {
                collider.enabled = isRagdoll;
            }
        }

        foreach(Rigidbody rigidbody in allRigidbodies) {
            if (rigidbody.gameObject.CompareTag("Ragdoll")) {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }

    public void EnableRagdollWithPhysics(Vector3 momentum)
    {
        foreach(Collider collider in allColliders) {
            if (collider.gameObject.CompareTag("Ragdoll")) {
                collider.enabled = true;
            }
        }

        foreach(Rigidbody rigidbody in allRigidbodies) {
            if (rigidbody.gameObject.CompareTag("Ragdoll")) {
                rigidbody.isKinematic = false;
                rigidbody.useGravity = true;

                rigidbody.velocity = momentum + Random.insideUnitSphere * momentumMultiplier;
            }
        }

        controller.enabled = false;
        animator.enabled = false;
    }
}
