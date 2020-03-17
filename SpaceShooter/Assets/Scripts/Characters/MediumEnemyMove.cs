using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyMove : Move
{
    Vector3 StartPos = Vector3.zero;
    public float SinAmplitude = 2.0f;
    float Ranomizer = 0.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        Randomize();
        StartPos = transform.position;
    }

    void Randomize()
    {
        Ranomizer = Random.Range(0.0f, 100.0f);
    }

    public override void HandleMovement()
    {
        MoveDown();
        MoveSin();
    }

    void MoveSin()
    {
        Vector3 pos = transform.position;

        pos.x = StartPos.x + SinAmplitude * (Mathf.Sin((Time.time + Ranomizer) * MoveSpeedHoriz));

        transform.position = pos;
    }

    protected override bool CheckWalls()
    {
        bool hitwall = base.CheckWalls();

        return hitwall;
    }
}
