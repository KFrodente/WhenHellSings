using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public abstract class EnemyParent : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int contactDamage;
    [SerializeField, Tooltip("When the player touches this enemy, how many invincibility frames do they get?")] protected int contactIFrames;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool facingRight;

    protected Rigidbody2D rb;

    protected Vector2 originPos;
    [SerializeField] protected float agroRadius;
    protected bool aggressive;
    protected bool inHome = true;

    public bool bounce;
    protected bool shouldBounce;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shouldBounce = bounce;
        originPos = transform.position;
    }

    public abstract void TakeDamage(int amount);
    protected abstract void Death();
}
