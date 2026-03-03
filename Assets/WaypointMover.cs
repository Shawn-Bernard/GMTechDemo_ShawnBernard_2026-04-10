using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Awake()
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

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
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
