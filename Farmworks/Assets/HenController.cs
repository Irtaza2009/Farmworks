using System.Collections;
using UnityEngine;

public class HenController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float peckTime = 3f;
    public float nestTime = 5f;
    public GameObject eggPrefab;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private bool isPecking = false;
    private bool isNesting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(BehaviorRoutine());
    }

    void FixedUpdate()
    {
        if (!isPecking && !isNesting)
        {
            rb.velocity = moveDirection * moveSpeed;
            animator.Play("Hen_Move"); // Play walking animation
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator BehaviorRoutine()
    {
        while (true)
        {
            float randomAction = Random.value;

            if (randomAction < 0.3f) // 30% chance to peck
            {
                yield return StartCoroutine(PeckRoutine());
            }
            else if (randomAction < 0.5f) // 20% chance to nest
            {
                yield return StartCoroutine(NestRoutine());
            }
            else // Move randomly
            {
                ChangeDirection();
            }

            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Wait before the next action
        }
    }

    IEnumerator PeckRoutine()
    {
        isPecking = true;
        animator.Play("Hen_Peck"); // Play peck animation
        Debug.Log("Hen is pecking...");
        yield return new WaitForSeconds(peckTime);
        isPecking = false;
    }

    IEnumerator NestRoutine()
    {
        isNesting = true;
        rb.velocity = Vector2.zero;

        Debug.Log("Hen is jumping into nest...");
        animator.Play("Hen_JumpIntoNest");
        yield return new WaitForSeconds(1f); // Delay for jump animation

        Debug.Log("Hen is nesting...");
        animator.Play("Hen_Nesting");
        yield return new WaitForSeconds(nestTime);

        // Lay an egg
        Instantiate(eggPrefab, transform.position, Quaternion.identity);
        Debug.Log("Egg laid!");

        isNesting = false;
        ChangeDirection(); // Resume movement
    }

    void ChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            moveDirection = -moveDirection; // Reverse direction when hitting a fence
        }
    }
}
