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
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y + ((amplitude * Mathf.Sin(sinWaveTimer * 3) * 1) * Mathf.Deg2Rad));
    }
    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Death();
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }
}
