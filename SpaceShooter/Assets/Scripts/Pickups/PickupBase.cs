using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    public float FallSpeed = 1.0f;
    BoxCollider Trigger = null;

    public AudioClip Clip = null;

    private void Awake()
    {
        Trigger = GetComponent<BoxCollider>();
        Trigger.isTrigger = true;
    }

    private void Update()
    {
        //disable if it goes off screen
        if (transform.position.y < -7.0f)
        {
            gameObject.transform.root.gameObject.SetActive(false);
            return;
        }

        //get grabbed if we hit the player
        foreach (var c in Physics.OverlapBox(transform.position, Trigger.size * 0.5f))
        {
            if (c.gameObject.CompareTag("Player"))
            {
                Player player = c.GetComponentInParent<Player>();
                player.PlayClip(Clip);
                TriggerEnter(c.gameObject);
            }
        }

        Vector3 pos = transform.position;

        pos.y -= FallSpeed * Time.deltaTime;
        transform.position = pos;
    }

    //do our thing and disable
    protected virtual void TriggerEnter(GameObject collision)
    {
        transform.root.gameObject.SetActive(false);
    }
}
