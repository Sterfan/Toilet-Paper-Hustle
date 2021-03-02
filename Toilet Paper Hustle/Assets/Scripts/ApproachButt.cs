using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachButt : MonoBehaviour
{
    public AudioManager audioManager;
    SpaceShipRB hand;
    bool trash = false;

    public bool destroyOnPlay = false;

    int trashed = 4;

    public int soundID = 0;

    int tp = 7, towel = 8;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        hand = FindObjectOfType<SpaceShipRB>();
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
                if (hand.objectTag == "TP")
                {
                    audioManager.PlaySound(tp);
                }
                else if (hand.objectTag == "Towel")
                {
                    audioManager.PlaySound(towel);
                }
                else
                {
                    audioManager.PlaySound(soundID);
                }
            }
        }
        else
        {
            if (trashed >= 4)
            {
                RandomizeSound();
            }
        }
        if (destroyOnPlay)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    void RandomizeSound()
    {
        int rand = Random.Range(5, 7);
        audioManager.PlaySound(rand);
    }
}
