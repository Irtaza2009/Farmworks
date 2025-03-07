using System.Collections;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float minSleepTime = 10f;
    public float maxSleepTime = 15f;
    public float actionIntervalMin = 8f;
    public float actionIntervalMax = 12f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private bool isSitting = true;
    private bool isSleeping = false;
    private bool isDrinking = false;
    private bool isEating = false;
    private bool isStanding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(BehaviorRoutine());
    }

    void FixedUpdate()
    {
        if (!isSleeping && !isDrinking && !isEating && isStanding)
        {
            rb.velocity = moveDirection * moveSpeed;
            animator.Play("Cow_Moving");

            // Flip sprite if moving left
            if (moveDirection.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
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

            if (randomAction < 0.3f) // 30% chance to sleep
            {
                yield return StartCoroutine(SleepRoutine());
            }
            else if (randomAction < 0.5f) // 20% chance to drink
            {
                yield return StartCoroutine(DrinkRoutine());
            }
            else if (randomAction < 0.7f) // 20% chance to eat
            {
                yield return StartCoroutine(EatRoutine());
            }
            else // 30% chance to randomly move
            {
                yield return StartCoroutine(MoveRoutine());
            }

            yield return new WaitForSeconds(Random.Range(actionIntervalMin, actionIntervalMax));
        }
    }

    IEnumerator SleepRoutine()
    {
        isSleeping = true;
        isSitting = true;
        rb.velocity = Vector2.zero;

        animator.Play("Cow_Sit_Sleeping");
        Debug.Log("Cow is sleeping...");

        yield return new WaitForSeconds(Random.Range(minSleepTime, maxSleepTime));

        isSleeping = false;
        PlayIdleSitting();
    }

    IEnumerator DrinkRoutine()
    {
        yield return StandUpRoutine();

        isDrinking = true;
        rb.velocity = Vector2.zero;
        animator.Play("Cow_Drinking");
        Debug.Log("Cow is drinking...");

        yield return new WaitForSeconds(Random.Range(4f, 6f));

        isDrinking = false;
        SitDown();
    }

    IEnumerator EatRoutine()
    {
        yield return StandUpRoutine();

        isEating = true;
        rb.velocity = Vector2.zero;
        animator.Play("Cow_Eating");
        Debug.Log("Cow is eating...");

        yield return new WaitForSeconds(Random.Range(3f, 6f));

        isEating = false;
        SitDown();
    }

    IEnumerator MoveRoutine()
    {
        yield return StandUpRoutine();

        isStanding = true;
        ChangeDirection();
        yield return new WaitForSeconds(Random.Range(2f, 5f)); // Moves for a short time
        SitDown();
    }

    IEnumerator StandUpRoutine()
    {
        if (isSitting)
        {
            isSitting = false;
            animator.Play("Cow_StandUp");
            Debug.Log("Cow is standing up...");
            yield return new WaitForSeconds(1f); // Allow animation to play
        }
    }

    void ChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void SitDown()
    {
        isSitting = true;
        rb.velocity = Vector2.zero;
        animator.Play("Cow_SitDown");
        Debug.Log("Cow is sitting...");

        Invoke(nameof(PlayIdleSitting), 1f);
    }

    void PlayIdleSitting()
    {
        if (Random.value < 0.5f)
        {
            animator.Play("Cow_Sit_Blinking");
        }
        else
        {
            animator.Play("Cow_Sit_Tailwag");
        }
    }

    void PlayIdleStanding()
    {
        if (Random.value < 0.5f)
        {
            animator.Play("Cow_Idle_Blinking");
        }
        else
        {
            animator.Play("Cow_Idle_TailWag");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            moveDirection = -moveDirection; // Reverse direction if hitting a fence
        }
    }
}
