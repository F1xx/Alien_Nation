using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static int Lives = 3;
    public static float PHealth = 100.0f;

    public float TimeInvincible = 1.0f;
    public float TimeToRespawn = 1.0f;

    [SerializeField]
    bool IsInvincible = false;
    Timer InvincibilityTimer = null;
    Timer RespawnTimer = null;

    private void Awake()
    {
        InvincibilityTimer = new Timer(TimeInvincible, SetNotInvincible);
        RespawnTimer = new Timer(TimeToRespawn, Respawn);
        PHealth = CurrentHealth;
    }

    private void Update()
    {
        InvincibilityTimer.Update();
        RespawnTimer.Update();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        StartRespawnTimer();
        IsInvincible = true;
        Lives--;
        base.OnDeath();
    }

    public override void Respawn()
    {
        if (Lives >= 0)
        {
            RespawnTimer.Reset();
            base.Respawn();
            PHealth = CurrentHealth;
            StartInvincibilityTimer();
        }
    }

    public override void TakeDamage(float amount)
    {
        if (!IsInvincible)
        {
            base.TakeDamage(amount);
            StartInvincibilityTimer();
        }
    }

    public void SetNotInvincible()
    {
        InvincibilityTimer.Reset();
        IsInvincible = false;
    }

    public void StartInvincibilityTimer()
    {
        IsInvincible = true;
        InvincibilityTimer.Restart();
    }

    public void StartRespawnTimer()
    {
        RespawnTimer.Restart();
    }

    protected override void CheckHealthBounds()
    {
        base.CheckHealthBounds();
        PHealth = CurrentHealth;
    }
}
