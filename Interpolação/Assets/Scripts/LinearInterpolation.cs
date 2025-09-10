using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : Curve
{
    public override void GenerateCurve(List<Transform> controlPoints)
    {
        for (int i = 0; i < controlPoints.Count; i++)
        {
            Vector3 p0 = controlPoints[i].position;
            Vector3 p1 = controlPoints[(i + 1) % controlPoints.Count].position;

            for (int j = 0; j < pointsNumber; j++) {
                float t = (float)j / pointsNumber;
				Vector3 point = Vector3.Lerp(p0, p1, t);
				InstantiateIntermediatePoints(point);
            }
        }
    }
}