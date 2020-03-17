using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : Character
{
    CharacterController Controller = null;
    Vector2 CenterScreen = Vector2.zero;
    Vector3 Movement = Vector3.zero;
    Vector3 Startingpos = Vector3.zero;
    SpriteRenderer SpriteRend = null;
    PlayerShoot Gun = null;
    AudioSource Source = null;
    BoxCollider PlayerCollider = null;

    protected override void Awake()
    {
        base.Awake();
        CenterScreen = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Controller = GetComponent<CharacterController>();
        Input.simulateMouseWithTouches = true;
        SpriteRend = GetComponentInChildren<SpriteRenderer>();
        Startingpos = transform.position;
        Gun = GetComponentInChildren<PlayerShoot>();
        Source = GetComponent<AudioSource>();
        Source.playOnAwake = false;
        PlayerCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && OwnerHealth.IsLiving())
        {
            Vector3 Pos = Input.mousePosition;
            //Left
            if (Pos.x < CenterScreen.x)
            {
                Movement = -Vector3.right * Speed * Time.deltaTime;
            }
            //right
            else if (Pos.x > CenterScreen.x)
            {
                Movement = Vector3.right * Speed * Time.deltaTime;
            }
        }
        else
        {
            Movement = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (OwnerHealth.IsLiving())
        {
            Move();
            ResetY();
        }
    }

    private void Move()
    {
        if (Movement.magnitude > 0.0f)
        {
            Controller.Move(Movement);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            Source.volume = PlayerPrefs.GetFloat("SFXVolume");
        }

        Source.clip = clip;
        Source.Play();
    }

    public override void OnDeathEvent()
    {
        base.OnDeathEvent();
        SpriteRend.enabled = false;
        PlayerCollider.enabled = false;
        Controller.enabled = false;
        EnemySpawner.DecreaseDifficulty(2);
    }

    public override void OnRespawnEvent()
    {
        ResetPosition();
        SpriteRend.enabled = true;
        PlayerCollider.enabled = true;
        Controller.enabled = true;
        Gun.ResetLevel();
        Gun.ResetFireRate();
        base.OnRespawnEvent();
    }

    public void ResetPosition()
    {
        transform.position = Startingpos;
    }

    void ResetY()
    {
        Vector3 pos = transform.position;

        if(pos.y != Startingpos.y)
        {
            pos.y = Startingpos.y;
            transform.position = pos;
        }
    }
}
