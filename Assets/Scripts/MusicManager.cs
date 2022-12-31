using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource soundSource;
    [SerializeField] AudioClip Title;
    [SerializeField] AudioClip NightLight;

    private void Awake()
    {
        PlayClip(Title);
    }
    
    public void PlayClip(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.Play();
    }
}
