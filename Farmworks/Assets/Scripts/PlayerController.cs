using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    private string currentDirection = "Down";

    [SerializeField] HighlightController highlightController;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0)
        {
            moveY = 0;
        }

        movement.x = moveX;
        movement.y = moveY;

        UpdateAnimation();

        Check();
        
    }

    private void Check()
    {
        Vector2 position = rb.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);


        foreach (Collider2D c in colliders)
        {
            if (c.CompareTag("Interactable"))
            {
                Debug.Log("Interactable object found");
                highlightController.Highlight(c.gameObject);
                return;
            }
        }
        highlightController.Hide();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            if (movement.y > 0)
            {
                currentDirection = "Up";
            }
            else if (movement.y < 0)
            {
                currentDirection = "Down";
            }
            else if (movement.x > 0)
            {
                currentDirection = "Right";
            }
            else if (movement.x < 0)
            {
                currentDirection = "Left";
            }
            
            animator.Play($"Move_{currentDirection}");
        } else
        {
            animator.Play($"Idle_{currentDirection}");
        }
    }
}
