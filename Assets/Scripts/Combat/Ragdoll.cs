using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;

    private void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider col in allColliders)
        {
            if (col.gameObject.CompareTag("Ragdoll"))
            {
                col.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rig in allRigidbodies)
        {
            if (rig.gameObject.CompareTag("Ragdoll"))
            {
                rig.isKinematic = !isRagdoll;
                rig.useGravity = isRagdoll;
            }
        }

        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}