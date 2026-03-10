using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();

    Vector2 startPosition;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Move();
    }

    private void Move()
    {
        if (waypoints != null)
        {
            /*
            float distance = Vector2.Distance(
                startPosition,
                waypoints[0].position);
            waypoints[0].SetParent(null);
            float remainingDistance = distance;

            while (remainingDistance > 0)
            {
                transform.position = Vector2.Lerp(
                    startPosition, waypoints[0].position, 1 - (remainingDistance / distance));
                remainingDistance -= moveSpeed * Time.deltaTime;
            }
            */
        }
            

    }

    void Awake()
    {
        startPosition = transform.position;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Waypoint"))
            {
                waypoints.Add(child);
            }
        }

        foreach (Transform child in waypoints)
        {
            //child.SetParent(null);
        }

    }

    // Update is called once per frame
    void Update()
    {
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
