using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Line
{
    public bool captainActive;
    public bool onionActive;
    public string line;
}


[CreateAssetMenu]
public class DialogData : ScriptableObject
{
    //public bool captainActive;
    //public bool onionActive;
    //public string[] lines;
    public Line[] lines;
}
