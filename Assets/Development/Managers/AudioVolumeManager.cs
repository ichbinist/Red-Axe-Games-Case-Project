using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : Singleton<AudioVolumeManager>
{
    public float MainVolume;
    public float MusicVolume;

    private float endGenericVolume { get { return (MainVolume); } }
    private float endMusicVolume { get { return (MainVolume * MusicVolume); } }

    public float GetGenericVolume()
    {
        return endGenericVolume;
    }

    public float GetMusicVolume()
    {
        return endMusicVolume;
    }
}