using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn spawn;
    public Transform spawnSpot;
    public GameObject ship;
    //Vector3 pos;


    void Awake()
    {
        spawn = this;
        //pos = spawnSpot.position;
    }

    public void SpawnShip()
    {
        Instantiate(ship, spawnSpot.position, Quaternion.identity);
    }
}
