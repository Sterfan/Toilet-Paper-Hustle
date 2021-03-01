using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    public Sound[] sounds;
    AudioManager audioManager;
    public bool pickedUp = false;
    bool added = false;

    public int probabilityToPlay = 3;

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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            if (!added)
            {
                PlayAudio(CheckProbability());
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
            Sound s = sounds[tracksPlayed];
            s.source.Play();
            tracksPlayed++;
            if (tracksPlayed > sounds.Length)
            {
                tracksPlayed = 0;
            }
        }
    }
}
