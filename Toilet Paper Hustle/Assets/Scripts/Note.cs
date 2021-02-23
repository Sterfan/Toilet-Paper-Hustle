using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Note
{
    public GameObject[] positions;
    //public bool isOn = false;

    public int currentPosition = 0;

    public Note(GameObject[] newPositions)
    {
        positions = newPositions;
    }
}
