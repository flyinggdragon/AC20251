using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    protected static int pointsNumber = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateIntermediatePoints(Vector3 point)
    {
        Transform parent = GameObject.Find("Intermediates").transform;

        GameObject prefab = Resources.Load<GameObject>("Prefab/Intermediate");

        GameObject p = Instantiate(prefab, point, Quaternion.identity);

        p.transform.SetParent(parent, true);
    }

    public virtual void GenerateCurve(List<Transform> controlPoints) { }
}
