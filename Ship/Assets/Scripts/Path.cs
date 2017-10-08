using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] points;
    public Vector2[] path;
    int step = 0;

    void Start()
    {
        path = new Vector2[points.Length];
        for (int i=0;i<points.Length;i++)
        {
            path[i] = points[i].position;
        }
    }

    void FixedUpdate()
    {

    }
}
