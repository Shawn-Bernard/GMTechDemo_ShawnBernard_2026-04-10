using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointMover : MonoBehaviour
{
    private List<Transform> waypoints;

    Vector2 startPosition;
    Vector3 targetPosition;


    int waypointIndex;
    public AnimationCurve curve;

    public float duration = 1f;

    public bool isMoving;

    public float waitTime;

    void Awake()
    {
        waypoints = new List<Transform>();

        GetListOfWaypoints();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        HandleWaypoint();
    }

    private void HandleWaypoint()
    {
        if (!isMoving && waypoints.Count > 0)
        {
            targetPosition = GetNextPosition();

            StartCoroutine(MoveToWaypoint(transform.position, targetPosition));
        }
    }

    private IEnumerator MoveToWaypoint(Vector3 start, Vector3 target)
    {
        isMoving = true;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            progress = curve.Evaluate(progress);

            transform.position = Vector3.Lerp(start, target, progress);

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        isMoving = false;
    }

    Vector3 GetNextPosition()
    {
        waypointIndex = (waypointIndex + 1) % waypoints.Count;
        return waypoints[waypointIndex].position;
    }

    void GetListOfWaypoints()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Waypoint"))
            {
                waypoints.Add(child);
            }
        }

        foreach (Transform child in waypoints)
        {
            child.SetParent(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }



    private void OnDrawGizmos()
    {
        if (waypoints != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Waypoint"))
                {
                    Gizmos.DrawCube(child.position, Vector3.one);
                }
            }

        }
            
    }
}
