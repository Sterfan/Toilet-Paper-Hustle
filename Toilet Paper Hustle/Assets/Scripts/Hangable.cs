using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangable : MonoBehaviour
{
    Rigidbody rb;
    public bool dropped = false;
    public bool fruit = false;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dropped)
        {
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                dropped = false;
                timer = 0f;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (dropped)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = collision.rigidbody;
                joint.breakForce = 1200;
                Debug.Log("Fixed");
            }
        }
    }
}
