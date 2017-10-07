using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector2 wind;
	
	void Start ()
    {
        SetWind(wind);
	}
	
	void SetWind(Vector2 newWind)
    {
        Physics2D.gravity = newWind;
    }
}
