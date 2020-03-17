using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    [Range(0.0f, 100.0f)]
    public float CurrentHealth = 100.0f;
    bool IsAlive = true;

    public virtual void TakeDamage(float amount)
    {
        if (IsAlive)
        {
            CurrentHealth -= amount;
            CheckHealthBounds();
        }
    }
    public virtual void Heal(float amount)
    {
        CurrentHealth += amount;
        CheckHealthBounds();
    }

    public virtual void OnDeath()
    {
        IsAlive = false;
        SendMessage("OnDeathEvent", SendMessageOptions.DontRequireReceiver);
    }

    public virtual void Respawn()
    {
        IsAlive = true;
        CurrentHealth = MaxHealth;
        SendMessage("OnRespawnEvent", SendMessageOptions.DontRequireReceiver);
    }

    public bool IsLiving()
    {
        return IsAlive;
    }

    protected virtual void CheckHealthBounds()
    {
        if(IsAlive)
        {
            if(CurrentHealth <= 0.0f)
            {
                OnDeath();
            }
            else if(CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}
