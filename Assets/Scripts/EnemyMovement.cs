using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour,IKnockbackable,IStoppable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceRange;
    [SerializeField] private float coneSize;
    [SerializeField] private float rangeBeforeStopping;

    [SerializeField] LayerMask raycastMask;
    private Rigidbody2D rb;

    private Vector3 targetPosition;
    [SerializeField] private GameObject player;

    private Vector2 moveDirection;

    private Vector3 directionToTarget;
    private Vector3 directionToPlayer;

    private Animator animator;

    [SerializeField] private float waitTime;
    private float waitTimer;

    [SerializeField] private bool canMove;

    private void Awake()
    {
        canMove = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FindPlayer();
        GetRandomPosition();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    bool isPlayerWithinRange()
    {
        directionToPlayer = (player.transform.position - transform.position);

        // If the player is out of range return false
        if (directionToPlayer.magnitude > distanceRange) return false;

        Vector2 forwardDirection = directionToTarget.normalized;

        float playerAngle = Vector2.Angle(forwardDirection, directionToPlayer);
        
        // If the player angle is out of view return false
        if (playerAngle > coneSize)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Shoots a raycast from the enemy to player, if the player is hit then the target becomes the player
    /// </summary>
    void LineOfSightCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceRange,raycastMask);
        if (!hit) return;

        if (hit.collider.CompareTag("Player"))
        {
            //Debug.Log("Player is now the target");
            targetPosition = player.transform.position;
        }
    }

    void GetRandomPosition()
    {
        Vector3 randomPosition = (Random.insideUnitSphere * distanceRange) + transform.position;
        targetPosition = randomPosition;
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerWithinRange())
        {
            LineOfSightCheck();
            MoveTowardTarget();
        }
        else
        {
            // If were close enough
            if (Vector2.Distance(transform.position, targetPosition) < rangeBeforeStopping)
            {
                // Wait and add time
                waitTimer += Time.deltaTime;
                //Stop motion
                rb.linearVelocity = Vector2.zero;
                // Done waiting
                if (waitTimer >= waitTime)
                {
                    waitTimer = 0;
                    GetRandomPosition();
                    
                }
                //Waiting
                else
                {
                    if (animator != null)
                    {
                        animator.SetBool("isWalking", false);

                        animator.SetFloat("LastMoveInputX", directionToTarget.x);
                        animator.SetFloat("LastMoveInputY", directionToTarget.y);
                    }
                }

            }
            else
            {
                MoveTowardTarget();
                if (animator != null)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetFloat("MoveInputX", directionToTarget.x);
                    animator.SetFloat("MoveInputY", directionToTarget.y);
                }
            }
        }


    }

    void MoveTowardTarget()
    {
        if (!canMove) return;

        directionToTarget = (targetPosition - transform.position).normalized;
        rb.linearVelocity = directionToTarget * moveSpeed;
    }

    public void ApplyKnockback(Transform objectOfImpact, float duration, float knockbackForce)
    {
        StartCoroutine(Knockback(objectOfImpact, duration, knockbackForce));

    }

    /// <summary>
    /// Applies knockback takes in the transform for direction, duration for how long 
    /// </summary>
    /// <param name="objectOfImpact"></param>
    /// <param name="duration"></param>
    /// <param name="knockbackForce"></param>
    /// <returns></returns>
    public IEnumerator Knockback(Transform objectOfImpact,float duration,float knockbackForce)
    {
        canMove = false;
        Debug.Log("Started Knockback");

        rb.linearVelocity = Vector2.zero;
        Vector3 direction = (transform.position - objectOfImpact.position).normalized;

        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        canMove = true;

    }

    public void StopAndWait(float duration)
    {
        StartCoroutine(StopMoving(duration));
    }
    public IEnumerator StopMoving(float StopMovingDuration)
    {
        animator.SetBool("isWalking", false);
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(StopMovingDuration);
        canMove = true;
    }
}
