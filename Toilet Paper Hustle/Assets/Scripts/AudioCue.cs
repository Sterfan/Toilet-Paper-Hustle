using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    public Sound[] sounds;
    AudioManager audioManager;
    [HideInInspector]
    public bool pickedUp = false;
    bool added = false;

    public int probabilityToPlay = 3;
    //public int timesToPlay = 2;
    public int cooldown = 1;
    int timesSincePlayed;

    int tracksPlayed = 0;
    int pickCounter = 0;




    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        timesSincePlayed = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            if (!added)
            {
                if (timesSincePlayed >= cooldown)
                {
                    PlayAudio(CheckProbability());
                }
                else
                {
                    timesSincePlayed++;
                }
                pickCounter += 1;
                added = true;
            }
        }
        else
        {
            added = false;
        }
    }

    bool CheckProbability()
    {
        int rand = Random.Range(1, probabilityToPlay + 1);
        if (rand == 1)
        {
            return true;
        }
        return false;
    }

    void PlayAudio(bool checkedProbability)
    {
        if (checkedProbability)
        {
            if (tracksPlayed > 0)
            {
                sounds[tracksPlayed - 1].source.Stop();
            }
            Sound s = sounds[tracksPlayed];
            s.source.Play();
            tracksPlayed++;
            timesSincePlayed = 0;
            if (tracksPlayed > sounds.Length - 1)
            {
                tracksPlayed = 0;
            }
        }
        else
        {
            timesSincePlayed++;
        }
    }
}
