using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float grad;
    Transform tran;
	
    void Start()
    {
        tran = GetComponent<Transform>();
    }

	void Update ()
    {
        tran.Rotate(new Vector3(0.0f, 0.0f, grad * Time.deltaTime));
	}
}
