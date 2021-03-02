using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApproachButt : MonoBehaviour
{
    public AudioManager audioManager;
    SpaceShipRB hand;
    public bool trash = false;

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
                    gameObject.GetComponent<FadetoBlack>().FadeToBlack();
                    Invoke("RestartScene", 5.0f);
                }
                else if (hand.objectTag == "Towel")
                {
                    audioManager.PlaySound(towel);
                }
                else
                {
                    audioManager.PlaySound(soundID);
                    if (destroyOnPlay)
                    {
                        gameObject.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
        else
        {
            if (trashed >= 4 && other.tag != "TP")
            {
                RandomizeSound();
            }
        }

    }

    void RandomizeSound()
    {
        int chanceToPlay = Random.Range(0, 4);
        if (chanceToPlay == 1)
        {
            int rand = Random.Range(5, 7);
            audioManager.PlaySound(rand);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
