using UnityEngine;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    public Vector3 rotationAmount;
    public float duration = 1f;

    public Transform doorHinge;
    public AnimationCurve curve;

    private Quaternion closedRotation;
    private Quaternion openRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorHinge ??= transform.GetChild(0);
        AssignRotations();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is in the door zone");
            StartCoroutine(RotateDoor(closedRotation, openRotation));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left the door zone");
            StartCoroutine(RotateDoor(openRotation, closedRotation));
        }
    }

    void AssignRotations()
    {
        closedRotation = doorHinge.rotation;
        openRotation = Quaternion.Euler(rotationAmount);
    }

    private IEnumerator RotateDoor(Quaternion start, Quaternion target)
    {

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float progress = Mathf.Clamp01(time / duration);

            if (curve != null)
            {
                progress = curve.Evaluate(progress);
            }
            doorHinge.rotation = Quaternion.Lerp(start, target, progress);

            yield return null;
        }


    }
}