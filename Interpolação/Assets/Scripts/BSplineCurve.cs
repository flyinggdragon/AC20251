using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSplineCurve : Curve
{
    public override void GenerateCurve(List<Transform> controlPoints)
    {
        if (controlPoints.Count < 4) return;

        int n = controlPoints.Count;

        for (int i = 0; i < n; i++)
        {
            // Usa índice circular (módulo n)
            Vector3 p0 = controlPoints[i % n].position;
            Vector3 p1 = controlPoints[(i + 1) % n].position;
            Vector3 p2 = controlPoints[(i + 2) % n].position;
            Vector3 p3 = controlPoints[(i + 3) % n].position;

            for (int j = 0; j <= pointsNumber; j++)
            {
                float t = j / (float)pointsNumber;
                Vector3 point = CalculateBSplinePoint(t, p0, p1, p2, p3);
                InstantiateIntermediatePoints(point);
            }
        }
    }

    private Vector3 CalculateBSplinePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 point =
            ((-t * t * t + 3 * t * t - 3 * t + 1) * p0 +
             (3 * t * t * t - 6 * t * t + 4) * p1 +
             (-3 * t * t * t + 3 * t * t + 3 * t + 1) * p2 +
             (t * t * t) * p3) / 6.0f;

        return point;
    }
}