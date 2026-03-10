using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleporterTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = teleporterTarget.position;
        }
    }
}
