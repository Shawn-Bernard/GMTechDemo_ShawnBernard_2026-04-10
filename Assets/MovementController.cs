using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float movementOvertimeRate;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        Vector2 targetVelocity = moveInput * moveSpeed;

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
                animator.SetFloat("LastInputX", moveInput.x);
                animator.SetFloat("LastInputY", moveInput.y);
            }
            
        }

        moveInput = context.ReadValue<Vector2>();

        if (context.started)
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("InputX", moveInput.x);
                animator.SetFloat("InputY", moveInput.y);
            }
        }
        
    }
}
