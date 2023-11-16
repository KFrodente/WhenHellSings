using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ZombieController : EnemyParent
{


    private void Update()
    {
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
    }

    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0 )
        {
            Death();
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyWall" && bounce)
        {
            Flip();
        }

        if (other.tag == "Bounce")
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        moveSpeed *= -1;
    }
}
