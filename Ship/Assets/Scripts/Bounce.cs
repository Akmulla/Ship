using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ship"))
        {
            SoundManager.sm.SingleSound(SoundSample.Bounce);
        }
    }
}
