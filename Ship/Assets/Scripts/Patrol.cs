using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public Path path;
    int step = 0;
    public bool circle;
    bool forward=true;

    void Start ()
    {
        //path = GetComponent<Path>();
        rb = GetComponent<Rigidbody2D>();
        forward = true;
    }

    float e = 0.1f;
	void FixedUpdate ()
    {

        rb.velocity = (path.points[step] - (Vector2)transform.position).normalized * speed;
        if ((path.points[step] - (Vector2)transform.position).magnitude<e)
        {
            if (circle)
            {
                step++;
                if (step >= path.points.Length)
                    step = 0;
            }
            else
            {
                if (forward)
                    step++;
                else
                    step--;

                if (step >= path.points.Length)
                {
                    forward = false;
                    step--;
                }
                else
                if (step < 0)
                {
                    forward = true;
                    step++;
                }
            }
        }
	}

    
    
}
