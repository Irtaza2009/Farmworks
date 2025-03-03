using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;

    Character character;

    private string currentDirection = "Down";

    [SerializeField] HighlightController highlightController;
    [SerializeField] float sizeOfInteractableArea = 1.2f;

    [Header("Stamina Settings")]
    [SerializeField] private float staminaDrainRate = 0.1f; // Stamina lost per movement tick
    [SerializeField] private int staminaRegenRate = 1; // Stamina regained per second
    [SerializeField] private float staminaRegenDelay = 1f; // Delay before regen starts

    private float staminaRegenTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.isExhausted) // Only allow movement if not exhausted
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            if (moveX != 0)
            {
                moveY = 0; // Prioritize horizontal movement
            }

            movement.x = moveX;
            movement.y = moveY;

            if (movement != Vector2.zero)
            {
                character.GetTired(staminaDrainRate); // Reduce stamina when moving
                staminaRegenTimer = staminaRegenDelay; // Reset regen timer
            }
            else
            {
                staminaRegenTimer -= Time.deltaTime;
                if (staminaRegenTimer <= 0)
                {
                    character.Rest(staminaRegenRate); // Regenerate stamina
                    if (character.stamina.currVal > 0)
                    {
                        character.isExhausted = false; // Allow movement again when stamina is regained
                    }
                }
            }

            UpdateAnimation();
        }
        else
        {
            movement = Vector2.zero; // Stop movement if exhausted
            animator.Play($"Idle_{currentDirection}");
        }

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
