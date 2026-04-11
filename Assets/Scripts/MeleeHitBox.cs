using UnityEngine;

public class MeleeHitBox : MonoBehaviour
{
    [SerializeField] private string triggerTag;

    [SerializeField] private int damageAmount;

    [SerializeField] private float knockbackForce;

    [SerializeField] private float knockbackDuration;

    private IDamageable damageable;
    private IKnockbackable knockbackable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void isWithinAttackRange()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerTag == null) return;
        if (collision.CompareTag(triggerTag))
        {
            knockbackable = collision.GetComponent<IKnockbackable>();
            damageable = collision.GetComponent<IDamageable>();
            DealDamage();
            PushBack();
        }
    }

    void DealDamage()
    {
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }

    void PushBack()
    {
        if (knockbackable != null)
        {
            knockbackable.ApplyKnockback(transform, knockbackDuration, knockbackForce);
        }
    }
}
