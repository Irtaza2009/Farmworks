using System.Collections;
using UnityEngine;

public class CowController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float minSleepTime = 10f;
    public float maxSleepTime = 15f;
    public float actionIntervalMin = 8f;
    public float actionIntervalMax = 12f;
    public GameObject milkBottlePrefab; // Assign in Inspector

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
            transform.localScale = new Vector3(moveDirection.x < 0 ? -1 : 1, 1, 1);
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

            if (randomAction < 0.3f) yield return StartCoroutine(SleepRoutine());
            else if (randomAction < 0.5f) yield return StartCoroutine(DrinkRoutine());
            else if (randomAction < 0.7f) yield return StartCoroutine(EatRoutine());
            else yield return StartCoroutine(MoveRoutine());

            yield return new WaitForSeconds(Random.Range(actionIntervalMin, actionIntervalMax));
        }
    }

    IEnumerator SleepRoutine()
    {
        isSleeping = true;
        isSitting = true;
        rb.velocity = Vector2.zero;
        animator.Play("Cow_Sit_Sleeping");
        yield return new WaitForSeconds(Random.Range(minSleepTime, maxSleepTime));
        isSleeping = false;
        PlayIdleSitting();
    }

    IEnumerator DrinkRoutine()
    {
        yield return StandUpRoutine();
        isDrinking = true;
        animator.Play("Cow_Drinking");
        yield return new WaitForSeconds(Random.Range(4f, 6f));
        isDrinking = false;
        SitDown();
    }

    IEnumerator EatRoutine()
    {
        yield return StandUpRoutine();
        isEating = true;
        animator.Play("Cow_Eating");
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        isEating = false;
        SitDown();
    }

    IEnumerator MoveRoutine()
    {
        yield return StandUpRoutine();
        isStanding = true;
        ChangeDirection();
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        SitDown();
    }

    IEnumerator StandUpRoutine()
    {
        if (isSitting)
        {
            isSitting = false;
            animator.Play("Cow_StandUp");
            yield return new WaitForSeconds(1f); // Allow animation to play
            InstantiateMilkBottle(); // Spawn milk bottle when standing up
        }
    }

    void InstantiateMilkBottle()
    {
        if (milkBottlePrefab != null)
        {
            Instantiate(milkBottlePrefab, transform.position, Quaternion.identity);
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
        Invoke(nameof(PlayIdleSitting), 1f);
    }

    void PlayIdleSitting()
    {
        animator.Play(Random.value < 0.5f ? "Cow_Sit_Blinking" : "Cow_Sit_Tailwag");
    }

    void PlayIdleStanding()
    {
        animator.Play(Random.value < 0.5f ? "Cow_Idle_Blinking" : "Cow_Idle_TailWag");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            moveDirection = -moveDirection;
        }
    }
}
