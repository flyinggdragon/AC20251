using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurveModes
{
    Linear,
    BSpline
}

public class Ball : MonoBehaviour
{
    // Modo de curva
    public CurveModes curveMode;
    private Curve curve;

    // Componentes
    public Rigidbody rb;

    // Pontos
    private Transform currentPoint;
    private int currentPointIndex = 0;
    public List<Transform> controlPoints;
    public List<Transform> intermediatePoints;

    // Outros
    public Transform intermediatesParent;
    public float speed = 5f;
    public float pointThreshold = 0.1f;
    void Start()
    {
        if (curveMode is CurveModes.Linear)
            curve = new LinearInterpolation();
        else if (curveMode is CurveModes.BSpline)
            curve = new BSplineCurve();

        curve.GenerateCurve(controlPoints);

        intermediatePoints = new List<Transform>();
        intermediatePoints = GetIntermediatePoints();

        if (intermediatePoints.Count > 0)
            transform.position = intermediatePoints[0].position;
    }

    void Update()
    {
        if (intermediatePoints.Count < 2) return;

        Transform target = intermediatePoints[currentPointIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < pointThreshold)
        {
            currentPointIndex++;
            if (currentPointIndex >= intermediatePoints.Count)
                currentPointIndex = 0;
        }
    }

    private List<Transform> GetIntermediatePoints()
    {
        List<Transform> l = new List<Transform>();

        foreach (Transform i in intermediatesParent) l.Add(i);

        return l;
    }

    public void Move(Vector3 direction)
    {
        rb.linearVelocity = new(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
    }
}
