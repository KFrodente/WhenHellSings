using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : EnemyParent
{
    private float sinWaveTimer;
    [SerializeField] private float amplitude;
    private void Update()
    {
        sinWaveTimer += Time.deltaTime;
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y + ((amplitude * Mathf.Sin(sinWaveTimer * 5) * 1) * Mathf.Deg2Rad));
    }
    public override void TakeDamage(int amount)
    {
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
            PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            Debug.Log("Hit player");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            Debug.Log("Hit player");
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }
}
