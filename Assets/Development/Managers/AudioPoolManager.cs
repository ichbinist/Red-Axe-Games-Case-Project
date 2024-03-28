using System.Collections.Generic;
using UnityEngine;

public class AudioPoolManager : Singleton<AudioPoolManager>
{
    public int poolSize = 10;

    private List<AudioSource> audioSources;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        // Create a List to hold the AudioSource instances
        audioSources = new List<AudioSource>();

        // Populate the List with new AudioSource instances
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.playOnAwake = false;

            audioSources.Add(newAudioSource);
        }
    }

    public void PlaySound(AudioClip soundClip)
    {
        // Find an available AudioSource in the pool
        AudioSource availableAudioSource = FindAvailableAudioSource();

        // If an available AudioSource is found, play the sound
        if (availableAudioSource != null)
        {
            availableAudioSource.clip = soundClip;
            availableAudioSource.volume = 0.5f;
            availableAudioSource.Play();
        }
    }
    public void PlaySound(AudioClip soundClip,float volume)
    {
        // Find an available AudioSource in the pool
        AudioSource availableAudioSource = FindAvailableAudioSource();

        // If an available AudioSource is found, play the sound
        if (availableAudioSource != null)
        {
            availableAudioSource.clip = soundClip;
            availableAudioSource.volume = volume * AudioVolumeManager.Instance.GetGenericVolume();
            availableAudioSource.Play();
        }
    }

    AudioSource FindAvailableAudioSource()
    {
        // Find the first AudioSource that is not playing
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                // If not playing, return this AudioSource
                return audioSource;
            }
        }

        // If all AudioSource instances are currently playing, create a new one
        AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;

        // Add the newly created AudioSource to the List
        audioSources.Add(newAudioSource);

        // Return the newly created AudioSource
        return newAudioSource;
    }

    private void Update()
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.pitch = Time.timeScale;
        }
    }
}
