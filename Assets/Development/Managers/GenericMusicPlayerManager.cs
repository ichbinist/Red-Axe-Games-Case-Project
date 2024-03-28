using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMusicPlayerManager : Singleton<GenericMusicPlayerManager>
{
    public AudioSource AudioSource;

    private void Update()
    {
        AudioSource.volume = AudioVolumeManager.Instance.GetMusicVolume();
    }
}
