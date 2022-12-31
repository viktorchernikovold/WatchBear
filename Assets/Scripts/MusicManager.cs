using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager main;
    public AudioSource soundSource;
    [SerializeField] AudioClip Title;
    [SerializeField] AudioClip NightLight;

    private void Awake()
    {
        main = this;
        PlayClip(Title);
    }
    
    public void PlayClip(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.Play();
    }
    public void StopMusic()
    {
        soundSource.Stop();
    }
}
