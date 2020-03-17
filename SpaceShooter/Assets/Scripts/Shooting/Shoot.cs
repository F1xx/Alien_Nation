using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shoot : MonoBehaviour
{
    [Tooltip("This is in Shots per-second"), Range(0.0f, 20.0f)]
    public float RateOfFire = 1.0f;
    public bool ShootOnAwake = false;

    public Shootable Projectile = null;
    public GameObject[] Muzzle = null;

    protected float OriginalRateOfFire = 0.0f;

    protected Health OwnerHealth = null;
    protected Timer ShootTimer = null;
    AudioSource Source = null;

    protected virtual void Awake()
    {
        ShootTimer = new Timer(1 / RateOfFire, Fire);

        if (ShootOnAwake)
        {
            ShootTimer.Start();
        }

        OwnerHealth = GetComponentInParent<Health>();
        OriginalRateOfFire = RateOfFire;
        Source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            Source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    protected virtual void Update()
    {
        ShootTimer.Update();
    }

    public bool IsShooting()
    {
        return ShootTimer.IsTimerRunning();
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (UnityEditor.EditorApplication.isPlaying && ShootTimer != null)
        {
            ChangeRateOfFire(RateOfFire);
        }
    }
#endif

    public void ChangeRateOfFire(float ShotsPerSecond)
    {
        RateOfFire = ShotsPerSecond;
        ShootTimer.SetDuration(1 / RateOfFire);

        if (ShootTimer.IsTimerRunning())
        {
            ShootTimer.Restart();
        }
        else
        {
            ShootTimer.Reset();
        }
    }

    public void ResetFireRate()
    {
        ChangeRateOfFire(OriginalRateOfFire);
        ShootTimer.Restart();
    }

    public void ToggleShooting(bool shouldShoot)
    {
        if(shouldShoot)
        {
            ShootTimer.Restart();
        }
        else
        {
            ShootTimer.Reset();
        }
    }

    public virtual void Fire()
    {
        if (OwnerHealth.IsLiving())
        {
            foreach (GameObject muzzle in Muzzle)
            {
                FireRound(muzzle);
            }
        }
    }

    protected virtual void FireRound(GameObject muzzle)
    {
        //get bullet from pool
        PooledObject bullet = ObjectPoolManager.Get(Projectile.gameObject);

        //get the actual bullet part from the bullet
        Shootable shootBase = bullet.gameObject.GetComponent<Shootable>();

        //actually shoot it
        shootBase.Fire(muzzle.transform.position, muzzle.transform.rotation, muzzle.transform.up, bullet);
        Source.Play();
    }
}
