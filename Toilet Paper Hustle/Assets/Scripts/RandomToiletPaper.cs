using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomToiletPaper : MonoBehaviour
{
    public GameObject tp;
    
    public Transform[] tpSpawnPoints;

    void Start()
    {
        int rand = Random.Range(0, tpSpawnPoints.Length);
        tp.transform.position = tpSpawnPoints[rand].position;
        tp.transform.rotation = tpSpawnPoints[rand].rotation;
    }

}
