using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public static Port port;

    public GameObject particle;

    void Awake()
    {
        port=this;
    }
	public void Delivery()
    {
        particle.SetActive(true);
    }
}
