using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] pointObjs;
    [HideInInspector]
    public Vector2[] points;
    

    void Start()
    {
        points = new Vector2[pointObjs.Length];
        for (int i=0;i<pointObjs.Length;i++)
        {
            points[i] = pointObjs[i].position;
        }
    }


}
