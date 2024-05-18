using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 50;

    private void Start()
    {
        lineRenderer.positionCount = lineSegmentCount;
    }

    public void RenderTrajectory(Vector3 startPoint, Vector2 initialVelocity, bool isParabolic)
    {
        Vector3[] linePositions = new Vector3[lineSegmentCount];

        if (isParabolic)
        {
            for (int i = 0; i < lineSegmentCount; i++)
            {
                float t = i * 0.1f;
                Vector3 gravity = Physics2D.gravity * t * t * 0.5f;
                linePositions[i] = startPoint + (Vector3)(initialVelocity * t) + gravity;
            }
        }
        else
        {
            for (int i = 0; i < lineSegmentCount; i++)
            {
                float t = i * 0.1f;
                linePositions[i] = startPoint + (Vector3)(initialVelocity * t);
            }
        }

        lineRenderer.SetPositions(linePositions);
    }

    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}
