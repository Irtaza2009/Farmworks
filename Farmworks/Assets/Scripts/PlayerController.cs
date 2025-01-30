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
