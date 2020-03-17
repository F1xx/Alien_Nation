using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Shootable : MonoBehaviour
{
    public float Speed = 10.0f;
    public float Damage = 100.0f;

    PooledObject Pool;

    public void Fire(Vector2 pos, Quaternion rot, Vector2 dir, PooledObject pool)
    {
        transform.position = pos;
        transform.rotation = rot;
        Pool = pool;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(dir * Speed, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            Deactivate();
            return;
        }

        Health hp = collision.gameObject.GetComponentInParent<Health>();

        if(hp)
        {
            hp.TakeDamage(Damage);
        }

        SpawnExplosion();
        Deactivate();
    }

    void Deactivate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;
        }

        Pool.Deactivate();
    }

    void SpawnExplosion()
    {
        ExplosionSpawner.SpawnExplosion(transform.position);
    }
}