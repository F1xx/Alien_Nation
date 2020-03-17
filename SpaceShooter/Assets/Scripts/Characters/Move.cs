using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    public float MoveSpeedVert = 2.0f;
    public float MoveSpeedHoriz = 6.0f;
    protected float DirectionValue = 1.0f;//right

    protected Health OwnerHealth = null;

    protected virtual void Awake()
    {
        OwnerHealth = GetComponent<Health>();
    }

    protected virtual void OnEnable()
    {
        RandomizeDirection();
    }

    protected void RandomizeDirection()
    {
        float rand = Random.value;

        if(rand < 0.5f)
        {
            DirectionValue = -1.0f;
        }
        else
        {
            DirectionValue = 1.0f;
        }
    }

    protected void FixedUpdate()
    {
        if (OwnerHealth.IsLiving())
        {
            HandleMovement();
            CheckWalls();
        }
    }

    public abstract void HandleMovement();

    protected void MoveDown()
    {
        Vector3 pos = transform.position;

        pos.y -= MoveSpeedVert * Time.deltaTime;
        transform.position = pos;
    }

    protected void MoveUp()
    {
        Vector3 pos = transform.position;

        pos.y += MoveSpeedVert * Time.deltaTime;
        transform.position = pos;
    }


    protected virtual void MoveSideways()
    {
        Vector3 pos = transform.position;

        pos.x += MoveSpeedHoriz * Time.deltaTime * DirectionValue;
        transform.position = pos;
    }

    protected virtual bool CheckWalls()
    {
        Vector3 pos = transform.position;

        if(pos.x < -2.5f)
        {
            pos.x = -2.5f;
            DirectionValue = 1.0f;
            return true;
        }
        else if(pos.x > 2.5f)
        {
            pos.x = 2.5f;
            DirectionValue = -1.0f;
            return true;
        }

        return false;
    }
}
