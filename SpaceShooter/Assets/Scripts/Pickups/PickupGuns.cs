using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGuns : PickupBase
{
    [SerializeField]
    float RateOfFireIncrease = 5.0f;
    [SerializeField]
    float LengthOfTimeFirerateIncreased = 4.0f;

    protected override void TriggerEnter(GameObject collision)
    {
        PlayerShoot shot = collision.GetComponentInChildren<PlayerShoot>();

        shot.LevelUp();
        shot.IncreaseFireRate(RateOfFireIncrease, LengthOfTimeFirerateIncreased);

        base.TriggerEnter(collision);
    }
}
