using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
public class ShootController : MonoBehaviour
{
    public Camera mainCamera;

    public float fovIncreaseAmount;
    private float fovDefault;

    public float duration;

    public Vector2 lookInput;

    public bool IsAiming { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        fovDefault = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAiming = true;
            Debug.Log("Start Aimming");
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(fovDefault + fovIncreaseAmount));
        }

        if (context.canceled)
        {
            IsAiming = false;
            Debug.Log("Stopped Aimming");
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(fovDefault)); 
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started && IsAiming)
        {
            Debug.Log("Player is shooting");
        }

        if (context.started && !IsAiming)
        {
            Debug.Log("Player has melee attacked");
        }
    }

    public void LookPosition(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private IEnumerator ChangeFOV(float targetFOV)
    {
        //Debug.Log("Change FOV started");
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, progress);

            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
    }
}
