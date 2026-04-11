using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float startDuration;
    [SerializeField] private float endingDuration;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private GameObject Player;

    [SerializeField] private bool isCollected;

    [SerializeField] Vector2 feedbackSize;
    [SerializeField] float goBackForce;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
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
        Vector3 FeedbackTarget = transform.position + directionAway * goBackForce;
        // This will push it away from the player
        yield return StartCoroutine(Lerp(FeedbackTarget, feedbackSize, startDuration));
        // While then this will have it move towards
        yield return StartCoroutine(Lerp(Player.transform.position, Vector2.zero, endingDuration));

        gameObject.SetActive(false);
    }

    private IEnumerator Lerp(Vector2 targetPosition, Vector2 targetSize, float duration)
    {

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            if (curve != null)
            {
                progress = curve.Evaluate(progress);
            }

            transform.position = Vector2.Lerp(transform.position, targetPosition, progress);
            transform.localScale = Vector2.Lerp(transform.localScale, targetSize, progress);

            yield return null;
        }

        transform.position = targetPosition;
        transform.localScale = targetSize;
    }
}
