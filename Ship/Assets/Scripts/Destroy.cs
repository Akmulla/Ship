using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public void DestroyShip()
    {
        GameController.gc.ShipReached(false);
        Destroy(gameObject, 0.5f);
        
    }
}
