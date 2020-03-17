using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : Singleton<PauseManager>
{
    private float m_WorldTime = 0.0f;

    private int m_TimesPauseCalled = 0;

    protected override void OnAwake()
    {
        m_WorldTime = Time.timeScale;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static void Pause()
    {
        Instance().m_TimesPauseCalled++;

        if (Time.timeScale != 0.0f)
        {
            //scale all time
            Instance().m_WorldTime = Time.timeScale;
            Time.timeScale = 0.0f;

            //enable cursor
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    public static void Unpause()
    {
        Instance().m_TimesPauseCalled--;

        if (Time.timeScale == 0.0f && Instance().m_TimesPauseCalled == 0)
        {
            //scale all time
            Time.timeScale = Instance().m_WorldTime;

            //enable cursor
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    public static bool IsPaused()
    {
        return Instance().m_TimesPauseCalled > 0;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    //resets variables to what they should be when started
    private void ResetPause()
    {
        //scale all time
        Time.timeScale = 1.0f;

        m_WorldTime = 0.0f;
        m_TimesPauseCalled = 0;

        //enable cursor
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetPause();
    }
}
