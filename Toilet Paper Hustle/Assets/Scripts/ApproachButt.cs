using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachButt : MonoBehaviour
{
    public AudioManager audioManager;
    bool trash = false;

    int trashed = 4;

    public int soundID = 0;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (gameObject.CompareTag("Bin"))
        {
            trash = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!trash)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Play sound");
                audioManager.PlaySound(soundID);
            }
        }
        else
        {
            if (trashed >= 4)
            {
                RandomizeSound();
            }
        }
    }

    void RandomizeSound()
    {
        int rand = Random.Range(5, 7);
        audioManager.PlaySound(rand);
    }
}
