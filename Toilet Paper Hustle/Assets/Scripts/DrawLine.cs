using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform origin;
    public Transform destination;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(1, destination.position);
    }
}
