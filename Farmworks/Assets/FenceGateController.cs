using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceGateController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !isOpen)
        {
            animator.Play("Fence_Door_Open_Vertical");
            isOpen = true;
        }
    }
}
