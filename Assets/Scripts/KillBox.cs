using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private IDamageable damageable;

    [SerializeField] bool isPlayerTrigger;
    [SerializeField] bool isEnemyTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isPlayerTrigger)
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            KILL(damageable);
        }

        if (collision.CompareTag("Enemy") && isEnemyTrigger)
        {
            
            IDamageable damageable = collision.GetComponent<IDamageable>();
            KILL(damageable);
        }
    }

    void KILL(IDamageable damageable)
    {
        if (damageable == null) return;

        damageable.TakeDamage(damageAmount);
    }
}
