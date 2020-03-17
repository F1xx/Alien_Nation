using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public float ScoreAmount = 50.0f;

    public override void OnDeath()
    {
        HUD.AddScore(ScoreAmount);
        base.OnDeath();
    }

    public override void Respawn()
    {
        base.Respawn();
    }
}
