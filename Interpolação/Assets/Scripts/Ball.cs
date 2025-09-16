using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private Curve curve;

    // Componentes
    public Rigidbody rb;

    // Pontos
    private int currentPointIndex = 0;
    public List<Transform> controlPoints;
    public List<Transform> intermediatePoints;

    // Outros
    public Transform intermediatesParent;
    public float speed = 5f;
    public float pointThreshold = 0.1f;
    void Start()
    {
        curve = GetComponent<Curve>();
        curve.GenerateCurve(controlPoints);

        intermediatePoints = new List<Transform>(GetIntermediatePoints());

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
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
