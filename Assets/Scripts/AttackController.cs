using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Animator),typeof(Inventory))]
public class AttackController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Camera mainCamera;
    [SerializeField] Inventory inventory;
    [SerializeField] private GameObject meleeHitbox;
    [Range(0,1)]
    [SerializeField] private float startMeleeTime;
    [Range(0, 1)]
    [SerializeField] private float meleeDuration = 0.2f;
    [SerializeField] private float fovIncreaseAmount;
    private float fovDefault;

    public float fovDuration;

    private Vector3 attackDirection;

    public bool isAiming { get; private set; }
    [Range(0.5f,2)]
    [SerializeField] private float bulletPlacementOffset;
    [Range(0.5f, 2)]
    [SerializeField] private float meleePlacementOffset;

    private IStoppable stoppable;

    private void Awake()
    {
        stoppable = GetComponent<IStoppable>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        fovDefault = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAiming = true;
            //Debug.Log("Start Aimming");
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(fovDefault + fovIncreaseAmount));
        }

        if (context.canceled)
        {
            isAiming = false;
            //Debug.Log("Stopped Aimming");
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(fovDefault)); 
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started && isAiming)
        {
            ShootTowardsMouse();
            Debug.Log("Player is shooting");
        }

        if (context.started && !isAiming)
        {
            attackDirection = GetMouseDirection();
            if (animator != null)
            {
                animator.SetBool("isAttacking",true);
                animator.SetFloat("LookInputX",attackDirection.x);
                animator.SetFloat("LookInputY", attackDirection.y);
                animator.SetFloat("LastMoveInputX",attackDirection.x);
                animator.SetFloat("LastMoveInputY", attackDirection.y);
            }
            StartCoroutine(MeleeAttack());

            if (stoppable != null)
            {
                stoppable.StopAndWait(startMeleeTime + meleeDuration);
            }
        }
    }

    Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // offset the z by how far the camera is on the z.
        mousePosition.z = -mainCamera.transform.position.z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        return (mouseWorldPosition - transform.position).normalized;

    }
    private void ShootTowardsMouse()
    {
        if (inventory == null) return;

        attackDirection = GetMouseDirection();

        Vector3 SpawnPosition = transform.position + attackDirection * bulletPlacementOffset;

        GameObject bullet = inventory.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = SpawnPosition;
            bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, attackDirection);
            bullet.SetActive(true);
        }
    }
    IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(startMeleeTime);

        meleeHitbox.transform.rotation = Quaternion.FromToRotation(Vector3.up, attackDirection);

        meleeHitbox.transform.position = transform.position + attackDirection * meleePlacementOffset;

        meleeHitbox.SetActive(true);

        yield return new WaitForSeconds(meleeDuration);

        meleeHitbox.SetActive(false);
        if (animator != null)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private IEnumerator ChangeFOV(float targetFOV)
    {
        //Debug.Log("Change FOV started");
        float time = 0f;

        while (time < fovDuration)
        {
            time += Time.deltaTime;
            float progress = time / fovDuration;

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetFOV, progress);

            yield return null;
        }

        mainCamera.orthographicSize = targetFOV;
    }
}
