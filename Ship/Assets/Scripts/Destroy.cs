using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public void DestroyShip()
    {
        Destroy(gameObject, 0.5f);
        GameController.gc.Finish(false,Ship.ship.Price);
    }
}
