using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Shoot
{
    [SerializeField]
    int PlayerLevel = 0;
    [SerializeField]
    float TimeTillLevelUp = 5.0f;

    public List<MuzzleObj> ListOfMuzzles = null;

    Timer LevelUpTimer = null;
    Timer IncreasedrateOfFireTimer = null;

    protected override void Awake()
    {
        base.Awake();

        LevelUpTimer = new Timer(TimeTillLevelUp, LevelUp);
        IncreasedrateOfFireTimer = new Timer(1.0f, EndIncreasedFire);
        LevelUpTimer.Start();
    }

    public override void Fire()
    {
        if (OwnerHealth.IsLiving())
        {
            switch(PlayerLevel)
            {
                case 0:
                    MuzzleFire(ListOfMuzzles[0]);
                    break;
                case 1:
                    MuzzleFire(ListOfMuzzles[1]);
                    break;
                case 2:
                    MuzzleFire(ListOfMuzzles[2]);
                    break;
            }
        }
    }

    public void EndIncreasedFire()
    {
        ResetFireRate();
        IncreasedrateOfFireTimer.Reset();
    }

    public void IncreaseFireRate(float amount, float howLong)
    {
        ChangeRateOfFire(RateOfFire + amount);
        IncreasedrateOfFireTimer.SetDuration(howLong);
        IncreasedrateOfFireTimer.Restart();
    }

    public void LevelUp()
    {
        PlayerLevel++;

        PlayerLevel = Mathf.Clamp(PlayerLevel, 0, 2);
    }

    public void ResetLevel()
    {
        LevelUpTimer.Reset();
        PlayerLevel = 0;
    }

    protected override void Update()
    {
        base.Update();
        LevelUpTimer.Update();
        IncreasedrateOfFireTimer.Update();
    }

    void MuzzleFire(MuzzleObj obj)
    {
        foreach (GameObject muzzle in obj.list)
        {
            FireRound(muzzle);
        }
    }
}

[System.Serializable]
public class MuzzleObj
{
    public List<GameObject> list;
}
