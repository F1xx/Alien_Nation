using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public float Speed = 10.0f;

    protected Health OwnerHealth = null;

    protected virtual void Awake()
    {
        OwnerHealth = GetComponent<Health>();
    }

    public virtual void OnDeathEvent()
    {
    }

    public virtual void OnRespawnEvent()
    {
    }
}
