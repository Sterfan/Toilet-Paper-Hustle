using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class IntrotoGame : MonoBehaviour
{
    public AudioSource adSource;
    public AudioClip[] adClips;

    private int index;


    private void Start()
    {
        PlayNote();
    }

    public void PlayNote()
    {
        adSource.PlayOneShot(adClips[index]);

        index = (index + 1); // % adClips.Length;

        //adClips[0].length;
    }
}
