using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyMove : Move
{
    [SerializeField]
    bool MovingDown = true;

    Timer MoveDownTimer = null;
    Timer MoveSidewaysTimer = null;
    float TimeMovingDown = 1.5f;
    float TimeMovingSideways = 1.0f;
    float SidewaysRangeMin = 0.1f;
    float SidewaysRangeMax = 0.8f;

    bool HasDirectionBeenSwapped = false;

    protected override void Awake()
    {
        base.Awake();

        MoveDownTimer = new Timer(TimeMovingDown, SwapToSideways);
        MoveDownTimer.Start();

        TimeMovingSideways = Random.Range(SidewaysRangeMin, SidewaysRangeMax);
        MoveSidewaysTimer = new Timer(TimeMovingSideways, SwapToDown);
    }

    public override void HandleMovement()
    {
        if (MovingDown == true)
        {
            MoveDown();
        }
        else
        {
            MoveSideways();
        }
    }

    private void Update()
    {
        MoveDownTimer.Update();
        MoveSidewaysTimer.Update();
    }

    void SwapToSideways()
    {
        MovingDown = false;
        MoveDownTimer.Stop();

        if(HasDirectionBeenSwapped == false)
        {
            RandomizeDirection(); //get a random direction to move in
        }
        else
        {
            //if the direction was changed for me by the walls then I
            //acknowledge that here and set it back as this is only meant to be monitored by this function
            HasDirectionBeenSwapped = false;
        }

        //Set a random time to move sideways for
        TimeMovingSideways = Random.Range(SidewaysRangeMin, SidewaysRangeMax);
        MoveSidewaysTimer.SetDuration(TimeMovingSideways);

        MoveSidewaysTimer.Restart();
    }

    void SwapToDown()
    {
        MovingDown = true;
        MoveSidewaysTimer.Stop();
        MoveDownTimer.Restart();
    }

    protected override bool CheckWalls()
    {
        bool hitWall = base.CheckWalls();

        //if we hit a wall and are trying to move sideways, swap to down
        if (hitWall && MovingDown == false)
        {
            SwapToDown();
        }

        //if we hit the wall then the direction has been swapped
        //this is so that we don't repeatedly swap directions 
        //to avoid potentially repeatedly trying to go towards a wall
        if(hitWall)
        {
            HasDirectionBeenSwapped = true;
        }

        return hitWall;
    }
}
