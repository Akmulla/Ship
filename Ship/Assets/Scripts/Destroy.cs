using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    
	public void DestroyShip()
    {
        GameController.gc.ShipReached(false);
        Destroy(transform.parent.gameObject);
        
    }

    public void PlaySound()
    {
        SoundManager.sm.SingleSound(SoundSample.Explosion);
    }
}
