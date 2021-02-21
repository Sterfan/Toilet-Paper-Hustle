using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float camOffset;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        camOffset = Mathf.Abs(transform.position.z - hand.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-hand.transform.forward.x * camOffset, transform.position.y, -hand.transform.forward.z * camOffset);
    }
}
