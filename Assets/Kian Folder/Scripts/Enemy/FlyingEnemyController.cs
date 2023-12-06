using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : EnemyParent
{
    public AudioClip gotHitSound;
    private float sinWaveTimer;
    [SerializeField] private float amplitude;

    public float lifespan;
    private float lifespanCounter;



    private void Start()
    {
        theSR = GetComponentInChildren<SpriteRenderer>();
        lifespanCounter = lifespan;
    }

    private void Update()
    {
        if (PauseManager.instance.paused) return;

        if (Mathf.Abs(Vector2.Distance(transform.position, PlayerMovement.instance.transform.position)) > 15.0f)
        {
            lifespanCounter -= Time.deltaTime;
        }
        else
        {
            if (lifespanCounter != lifespan) lifespanCounter = lifespan;
        }

        if (lifespanCounter <= 0) Destroy(gameObject);

        if (currentStun > 0)
        {
            currentStun -= Time.deltaTime;
            flash();
            return;
        }

        theSR.color = Color.white;

        sinWaveTimer += Time.deltaTime;
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y + ((amplitude * Mathf.Sin(sinWaveTimer * 5) * 1) * Mathf.Deg2Rad));

        
    }
    public override void TakeDamage(int amount, float stunTime)
    {
        //AudioController.instance.PlaySound(gotHitSound, GetComponent<AudioSource>());
        this.currentStun = stunTime;
        health -= amount;
        if (health <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (currentStun <= 0)
            {
                PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (currentStun <= 0)
            {
                PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            }
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }
}
