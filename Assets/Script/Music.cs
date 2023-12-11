using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Music : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            PlayRandomMusic();


        }

    }



    private void PlayRandomMusic()
    {
        if (musicTracks.Length > 0)
        {

            int randomIndex = Random.Range(0, musicTracks.Length);
            audioSource.clip = musicTracks[randomIndex];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }
}