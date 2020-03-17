using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawnerbase : MonoBehaviour
{
    public float TimeBetweenSpawns = 10.0f;
    Timer SpawnTimer = null;
    Vector3 SpawnPos = new Vector3(0.0f, 6.0f, 0.0f); //X  must be between -25. & 2.5

    protected virtual void Awake()
    {
        SpawnTimer = new Timer(TimeBetweenSpawns, Spawn);
        SpawnTimer.Start();
    }

    protected virtual void Update()
    {
        SpawnTimer.Update();
    }

    protected abstract void Spawn();

    public virtual void Spawn(GameObject obj)
    {
        //Get a pickup of type from the pool
        PooledObject spawnee = ObjectPoolManager.Get(obj);
        spawnee.gameObject.transform.position = GetRandomSpawnPos();
        SpawnTimer.Restart();
    }

    public Vector3 GetRandomSpawnPos()
    {
        SpawnPos.x = Random.Range(-2.5f, 2.5f);
        return SpawnPos;
    }
}
