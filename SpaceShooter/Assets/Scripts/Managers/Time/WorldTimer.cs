using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTimer : Singleton<WorldTimer>
{
    public float TimePassed { get; private set; }

    public bool IsRunning { get; private set; }

    protected override void OnAwake()
    {
        TimePassed = 0.0f;
        IsRunning = true;
    }
    void Update()
    {
        if (IsRunning)
        {
            TimePassed += Time.deltaTime;
        }
    }
    public static string GetTimeAsFormattedString()
    {
        int intTime = (int)Instance().TimePassed;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float milliseconds = Instance().TimePassed * 1000;
        milliseconds = (milliseconds % 1000);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    public static void AddTime(float time)
    {
        Instance().TimePassed += time;
    }

    public static void RemoveTime(float time)
    {
        Instance().TimePassed -= time;
    }

    public static void StopTimer()
    {
        Instance().IsRunning = false;
    }

    public static void StartTimer()
    {
        Instance().IsRunning = true;
    }

    public static void Reset()
    {
        Instance().TimePassed = 0.0f;
    }
}
