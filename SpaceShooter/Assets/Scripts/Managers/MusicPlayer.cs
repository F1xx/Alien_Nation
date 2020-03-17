using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : Singleton<MusicPlayer>
{
    AudioSource Source = null;

    private void OnEnable()
    {
        ChangeVolume();
        Source.Play();
    }

    protected override void OnAwake()
    {
        Source = GetComponent<AudioSource>();
    }

    //change the PlayerPrefs and this will change to it
    public static void ChangeVolume()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            Instance().Source.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }
}
