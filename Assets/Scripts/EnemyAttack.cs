using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float attackRange;
    [Range(0, 1)]
    [SerializeField] private float startMeleeTime;
    [Range(0, 1)]
    [SerializeField] private float meleeDuration = 0.2f;

    [SerializeField] private GameObject meleeHitbox;
    [SerializeField] private Animator animator;

    [SerializeField] private float meleePlacementOffset = 1f;

    private Vector3 attackDirection;
    private bool isAttacking;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        HandleAttack();
    }

    /// <summary>
    /// Checks distance and if already attacking, if not then they will start attacking
    /// </summary>
    private void HandleAttack()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= attackRange)
        {
            StartCoroutine(MeleeAttack());
        }
    }
    /// <summary>
    /// Puts the hit box towards the attack direction and position, has a start before melee and hit box duration
    /// </summary>
    /// <returns></returns>
    IEnumerator MeleeAttack()
    {
        SetAnimation(true);
        yield return new WaitForSeconds(startMeleeTime);

        meleeHitbox.transform.rotation = Quaternion.FromToRotation(Vector3.up, attackDirection);

        meleeHitbox.transform.position = transform.position + attackDirection * meleePlacementOffset;

        meleeHitbox.SetActive(true);

        yield return new WaitForSeconds(meleeDuration);

        meleeHitbox.SetActive(false);
        SetAnimation(false);
    }

    /// <summary>
    /// Setting animations floats and takes in a bool to set is attacking
    /// </summary>
    /// <param name="isAttacking"></param>
    void SetAnimation(bool isAttacking)
    {
        if (animator == null) return;

        animator.SetBool("isAttacking", isAttacking);
        animator.SetFloat("LookInputX", attackDirection.x);
        animator.SetFloat("LookInputY", attackDirection.y);
        animator.SetFloat("LastMoveInputX", attackDirection.x);
        animator.SetFloat("LastMoveInputY", attackDirection.y);
    }
}
