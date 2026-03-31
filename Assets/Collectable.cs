using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using Unity.Mathematics;
using NUnit.Framework;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float bounceDuration;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AnimationCurve bounceCurve;

    [SerializeField] private GameObject Player;

    public float feedbackJump;

    [SerializeField] private bool isCollected;

    public float distanceToCollect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollected == true) return;
        if (collision.CompareTag("Player"))
        {
            if (Player == null)
            {
                Player = collision.gameObject;
            }
            
            Debug.Log("Player is collecting me");
            StartCoroutine(CollectAnimation());
        }
    }

    private IEnumerator CollectAnimation()
    {
        isCollected = true;
        Vector3 directionAway = (transform.position - Player.transform.position).normalized;
        Vector3 FeedbackTarget = transform.position + directionAway * feedbackJump;
        
        float feedbackBounceTime = 0f;

        while (feedbackBounceTime < bounceDuration)
        {
            feedbackBounceTime += Time.deltaTime;
            float progress = feedbackBounceTime / bounceDuration;

            if (bounceCurve != null)
            {
                progress = bounceCurve.Evaluate(progress);
            }

            transform.position = Vector3.Lerp(transform.position, FeedbackTarget, progress);
        }

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            if (curve != null) 
            {
                progress = curve.Evaluate(progress);
            }

            transform.position = Vector2.Lerp(transform.position, Player.transform.position, progress);

            yield return null;
        }

        //gameObject.SetActive(false);
    }
}
