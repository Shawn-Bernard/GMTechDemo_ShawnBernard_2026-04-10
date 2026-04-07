using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;

    [SerializeField] Transform target;

    Vector2 moveDirection;

    public float movementOvertimeRate;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        FindPlayer();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, moveDirection, movementOvertimeRate * Time.deltaTime);

        }
    }
}
