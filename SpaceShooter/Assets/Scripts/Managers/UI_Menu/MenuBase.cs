using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase : MonoBehaviour
{
    public Slider SFXVolumeSlider = null;

    protected virtual void Awake()
    {
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ChangeVolume(float amount)
    {
         PlayerPrefs.SetFloat("SFXVolume", amount);
         PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float amount)
    {
        PlayerPrefs.SetFloat("MusicVolume", amount);
        PlayerPrefs.Save();
        MusicPlayer.ChangeVolume();
    }
}
