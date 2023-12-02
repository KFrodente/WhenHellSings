
using UnityEngine;

public class ZombieController : EnemyParent
{
    private void Start()
    {
        theSR = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (currentStun > 0)
        {
            currentStun -= Time.deltaTime;
            flash();
            return;
        }

        theSR.color = Color.white;

        CheckAggression();

        if (aggressive)
        {
            if (PlayerMovement.instance.transform.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (PlayerMovement.instance.transform.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }

        if (!aggressive)
        {
            if (!inHome)
            {
                if (originPos.x > transform.position.x && !facingRight) Flip();
                else if (originPos.x < transform.position.x && facingRight) Flip();

                if (Mathf.Abs(transform.position.x - originPos.x) < 1f) inHome = true;
            }
        }
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
    }

    private void CheckAggression()
    {
        if (Vector2.Distance(PlayerMovement.instance.transform.position, transform.position) < agroRadius)
        {
            aggressive = true;
            shouldBounce = false;
            inHome = false;
        }
        else
        {
            aggressive = false;
            if (inHome)
                if (bounce) shouldBounce = true;
        }
    }

    public override void TakeDamage(int amount, float stunTime)
    {
        this.currentStun = stunTime;
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
        if (other.tag == "EnemyWall" && shouldBounce)
        {
            Flip();
        }

        if (other.tag == "Player")
        {
            PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            Debug.Log("Hit player");
        }

        if (other.tag == "Bounce")
        {
            if (!aggressive) Flip();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.HitPlayer(contactITime, contactDamage);
            Debug.Log("Hit player");
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        moveSpeed *= -1;
    }
}
