using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : PickupBase
{
    public float AmountToHeal = 50.0f;

    protected override void TriggerEnter(GameObject collision)
    {
        Health hp = collision.gameObject.GetComponent<Health>();
        hp.Heal(AmountToHeal);
        base.TriggerEnter(collision);
    }
}
