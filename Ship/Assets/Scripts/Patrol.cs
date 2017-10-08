using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public Path path;
    int step = 0;

    void Start ()
    {
        //path = GetComponent<Path>();
        rb = GetComponent<Rigidbody2D>();
	}

    float e = 0.05f;
	void FixedUpdate ()
    {

        rb.velocity = (path.points[step] - (Vector2)transform.position).normalized * speed;
        if ((path.points[step] - (Vector2)transform.position).magnitude<e)
        {
            step++;
            if (step >= path.points.Length)
                step = 0;
        }
	}

    
    
}
