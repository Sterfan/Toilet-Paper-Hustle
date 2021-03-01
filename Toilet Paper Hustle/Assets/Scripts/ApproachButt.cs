using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachButt : MonoBehaviour
{
    public AudioManager audioManager;
    

    public int soundID = 0;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            audioManager.PlaySound(soundID);
        }
    }
}
