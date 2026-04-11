using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform teleporterTarget;
    [Tooltip("How far away will you exit")]
    [SerializeField] private float exitOffset = 1.5f;
    [SerializeField] private float teleportCooldown = 0.2f;

    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTeleport) return;

        if (collision.CompareTag("Player"))
        {
            Transform player = collision.transform;

            Vector2 direction = (transform.position - player.position).normalized;
            Vector2 exitPosition = (Vector2)teleporterTarget.position + (direction * exitOffset);

            player.position = exitPosition;
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
