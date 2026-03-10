using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class AutoDoor : MonoBehaviour
{
    public float rotationAmount;
    public float timeToOpen;

    public Transform doorHinge;

    public AnimationCurve animationCurve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorHinge = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is in the door zone");
            var boom = Vector2.Lerp(doorHinge.position, new Vector3(0, rotationAmount), timeToOpen);
            Debug.Log(boom);
            doorHinge.Rotate(boom);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player left the door zone");
            doorHinge.rotation = transform.rotation;
        }
        
    }

    /*
    IEnumerator PlayDoorAnim()
    {
        yield return new WaitForSeconds(timeToOpen); 
    }*/


}
