using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private HealthSystemData data;

    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;

    public int CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = Mathf.Clamp(value, 0, data.maxHealth);
    }

    public UnityEvent OnDeath;

    public UnityEvent OnReset;

    private void Start()
    {
        ResetLife();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public void TakeDamage(int damageTaken)
    {
        CurrentHealth -= damageTaken;

        Debug.Log($"Took {damageTaken} damage current Health {currentHealth}");
        StartCoroutine(TakeDamageEffect());
        if (currentHealth == 0)
        {
            Death();
        }
    }

    IEnumerator TakeDamageEffect()
    {
        spriteRenderer.material = data.flashMaterial;
        spriteRenderer.color = data.flashColorStartAndEnd;

        yield return new WaitForSeconds(data.flashTime);
        spriteRenderer.color = data.flashColorMiddle;

        yield return new WaitForSeconds(data.flashTime);
        spriteRenderer.color = data.flashColorStartAndEnd;

        yield return new WaitForSeconds(data.flashTime);
        //Back to normal
        spriteRenderer.material = defaultMaterial;
        spriteRenderer.color = Color.white;
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
    }

    private void Death()
    {
        OnDeath!.Invoke();
        ResetLife();
    }

    private void ResetLife()
    {
        currentHealth = data.maxHealth;
        OnReset!.Invoke();
    }
    public void WaitForDeath()
    {
        StartCoroutine(DeathRoutine());
    }
    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(data.waitForDeathTime);

        gameObject.SetActive(false);
    }

}