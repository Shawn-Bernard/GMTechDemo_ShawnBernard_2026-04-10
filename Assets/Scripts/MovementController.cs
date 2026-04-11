using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour,IKnockbackable, IStoppable
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float movementOvertimeRate;
    private Vector2 targetVelocity;

    [SerializeField] public bool canMove { get; private set; }

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!canMove) return;
        targetVelocity = moveInput * moveSpeed;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, movementOvertimeRate * Time.deltaTime);
        }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetFloat("LastMoveInputX", moveInput.x);
                animator.SetFloat("LastMoveInputY", moveInput.y);
            }
            
        }

        moveInput = context.ReadValue<Vector2>();

        if (context.started)
        {
            animator.SetBool("isWalking", true);
        }
        if (animator != null)
        {
            
            animator.SetFloat("MoveInputX", moveInput.x);
            animator.SetFloat("MoveInputY", moveInput.y);
        }

    }
    public IEnumerator StopMoving(float StopMovingDuration)
    {
        animator.SetBool("isWalking", false);
        canMove = false;
        targetVelocity = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(StopMovingDuration);
        canMove = true;
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void ApplyKnockback(Transform source, float duration, float force)
    {
        StartCoroutine(Knockback(source, duration, force));
        StopMoving(duration);
    }
    public IEnumerator Knockback(Transform source, float duration, float force)
    {
        rb.linearVelocity = Vector2.zero;
        Vector3 direction = (transform.position - source.position).normalized;

        rb.AddForce(direction * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);

    }

    public void StopAndWait(float duration)
    {
        StartCoroutine(StopMoving(duration));
    }
}
