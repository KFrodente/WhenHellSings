using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class EnemyParent : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int contactDamage;
    [SerializeField, Tooltip("When the player touches this enemy, how many invincibility time do they get?")] protected float contactITime;
    [SerializeField] public float moveSpeed;
    public bool facingRight;

    public float currentStun { get; protected set; }

    [SerializeField]protected Rigidbody2D rb;

    protected Vector2 originPos;
    [SerializeField] protected float agroRadius;
    protected bool aggressive;
    protected bool inHome = true;

    public bool bounce;
    protected bool shouldBounce;

    public Color flashColor;
    protected int flashSpeed = 5;
    protected bool flashOn;

    protected SpriteRenderer theSR;

    protected void flash()
    {
        flashSpeed--;
        if (flashOn)
        {
            theSR.color = flashColor;
            if (flashSpeed <= 0)
            {
                flashSpeed = 5;
                flashOn = false;
            }
        }
        else
        {
            theSR.color = Color.white;
            if (flashSpeed <= 0)
            {
                flashSpeed = 5;
                flashOn = true;
            }
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shouldBounce = bounce;
        originPos = transform.position;
    }

    public void Knockback(int knockbackPower)
    {
        if (facingRight)
            rb.AddForce(new Vector2(-2.0f * knockbackPower, 0));
        else
            rb.AddForce(new Vector2(2.0f * knockbackPower, 0));
    }

    public abstract void TakeDamage(int amount, float stunTime);
    protected abstract void Death();
}
