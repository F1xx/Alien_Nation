using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpawner : Singleton<ExplosionSpawner>
{
    public GameObject Explosion = null;

    protected override void OnAwake()
    {
    }

    public static void SpawnExplosion(Vector3 pos)
    {
        PooledObject explosion = ObjectPoolManager.Get(Instance().Explosion, pos, Quaternion.identity);
    }
}
