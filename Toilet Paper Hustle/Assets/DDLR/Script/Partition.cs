using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Partition : MonoBehaviour
{
    [SerializeField]
    TextAsset partition;
    string wholePartitionAsOneString;
    public List<string> eachLine = new List<string>();
    List<int> noteAsIndex = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        wholePartitionAsOneString = partition.text;
        eachLine.AddRange(wholePartitionAsOneString.Split("\n"[0]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
