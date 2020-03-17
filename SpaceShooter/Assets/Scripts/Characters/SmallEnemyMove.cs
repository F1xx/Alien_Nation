using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyMove : Move
{
    public override void HandleMovement()
    {
        MoveDown();
        MoveSideways();
    }
}
