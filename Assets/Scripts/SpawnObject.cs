using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
