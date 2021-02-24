using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 camOffset;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        camOffset = hand.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = -hand.transform.forward;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Debug.Log(hand.transform.forward.normalized);
            transform.position = new Vector3(hand.transform.position.x + camOffset.x * direction.normalized.x, 0f, hand.transform.position.z + camOffset.z * direction.normalized.z);
        }
        else
        {
            transform.position = new Vector3(0f, 0f, hand.transform.position.z + camOffset.z * direction.normalized.z);
        }
    }
}
