using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoDoor : MonoBehaviour
{
    [SerializeField] private Sprite opened;

    [SerializeField] private Sprite closed;

    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = opened;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = closed;
        }
    }
}