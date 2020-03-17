using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float TimeUntilShootingStarts = 1.0f;
    Timer DontShootTimer = null;
    Shoot Gun = null;

    protected override void Awake()
    {
        base.Awake();

        Gun = GetComponentInChildren<Shoot>();
        DontShootTimer = new Timer(TimeUntilShootingStarts, StartShooting);
    }

    private void Update()
    {
        if (Gun.IsShooting() == false)
        {
            DontShootTimer.Update();
        }
    }

    void FixedUpdate()
    {
        //disable if it goes off screen
        if (transform.position.y < -7.0f)
        {
            gameObject.transform.root.gameObject.SetActive(false);
            return;
        }
    }

    private void OnEnable()
    {
        Respawn();
    }

    public void Respawn()
    {
        OwnerHealth.Respawn();
        DontShootTimer.Restart();
    }

    public override void OnDeathEvent()
    {
        StopShooting();
        gameObject.transform.root.gameObject.SetActive(false);
    }

    public void StartShooting()
    {
        Gun.ToggleShooting(true);
    }

    public void StopShooting()
    {
        Gun.ToggleShooting(false);
    }
}
