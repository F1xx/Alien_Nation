using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : PickupBase
{
    protected override void TriggerEnter(GameObject collision)
    {
        PlayerHealth.Lives++;
        base.TriggerEnter(collision);
    }
}
