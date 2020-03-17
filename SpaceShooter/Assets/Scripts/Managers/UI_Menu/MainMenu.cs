using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuBase
{
    public GameObject SettingsPanel = null;

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
}
