using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Projectile : MonoBehaviour
{
    public int speed;

    [SerializeField] private int damageAmount;

    [SerializeField] private float lifeTime;

    [SerializeField] private float knockbackForce;

    [SerializeField] private float knockbackDuration;

    private IDamageable damageable;
    private IKnockbackable knockbackable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartLifeTimer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    public void StartLifeTimer()
    {
        StartCoroutine(WaitUntillDeath());
    }
    IEnumerator WaitUntillDeath()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            knockbackable = collision.GetComponent<IKnockbackable>();
            damageable = collision.GetComponent<IDamageable>();
            DealDamage();
            PushBack();
            gameObject.SetActive(false);
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
