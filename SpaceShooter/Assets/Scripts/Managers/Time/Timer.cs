using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    float Duration = 0.0f;
    float ActiveDuration = 0.0f;
    UnityEvent Callback = null;
    bool IsRunning = false;

    public Timer(float duration, UnityAction callback)
    {
        Duration = duration;

        if(Callback == null)
        {
            Callback = new UnityEvent();
        }

        Callback.AddListener(callback);
    }

    public void SetDuration(float dur)
    {
        Duration = dur;
    }

    public void Update()
    {
        if (Duration <= 0.0f)
        {
            return;
        }

        if (Callback != null && IsRunning)
        {
            if(ActiveDuration < Duration)
            {
                ActiveDuration += Time.deltaTime;

                if(ActiveDuration >= Duration)
                {
                    Callback.Invoke();
                    ActiveDuration = 0.0f;
                }
            }
        }
    }

    public void Start()
    {
        if(Duration <= 0.0f)
        {
            return;
        }

        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Reset()
    {
        Stop();
        ActiveDuration = 0.0f;
    }

    public void Restart()
    {
        Reset();
        Start();
    }

    public bool IsTimerRunning()
    {
        return IsRunning;
    }
}
