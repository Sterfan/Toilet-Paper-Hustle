using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangable : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody rb;
    public bool dropped = false;
    public bool fruit = false;
    float timer = 0f;
    int cooldown = 1, timesStuck = 1;
    int soundIndex = 9;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
                if (timesStuck >= cooldown)
                {
                    audioManager.PlaySound(soundIndex);
                    timesStuck = 0;
                }
                else
                {
                    timesStuck++;
                }
            }
        }
    }
}
