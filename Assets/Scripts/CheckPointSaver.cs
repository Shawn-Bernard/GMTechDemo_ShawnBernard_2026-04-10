using UnityEngine;

public class CheckPointSaver : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Sprite isCheckpoint;
    [SerializeField] private Sprite notCheckpoint;

    private Vector2 spawnPoint;

    private GameObject currentCheckpoint;
    private GameObject lastCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.gameObject;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.SetParent(player.transform);
            transform.position = Vector3.zero;
        }

        spawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            // If its the same checkpoint then do nothing
            if (collision.gameObject == currentCheckpoint) return;

            lastCheckpoint = currentCheckpoint;

            currentCheckpoint = collision.gameObject;
            spawnPoint = currentCheckpoint.transform.position;

            SwapCheckpoint();
        }
    }

    void SwapCheckpoint()
    {
        if (lastCheckpoint != null)
        {
            SpriteRenderer lastSpriteRender = lastCheckpoint.GetComponent<SpriteRenderer>();
            if (lastSpriteRender != null)
            {
                lastSpriteRender.sprite = notCheckpoint;
            }
        }

        if (currentCheckpoint != null)
        {
            SpriteRenderer currentSpriteRender = currentCheckpoint.GetComponent<SpriteRenderer>();
            if (currentSpriteRender != null)
            {
                currentSpriteRender.sprite = isCheckpoint;
            }
        }
    }
    public void MoveToCheckPoint()
    {
        player.transform.position = spawnPoint;
    }
}
