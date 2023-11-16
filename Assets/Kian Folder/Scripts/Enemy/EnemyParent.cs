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

    protected Rigidbody2D rb;

    protected Vector2 originPos;

    [SerializeField] protected bool bounce;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void TakeDamage(int amount);
    protected abstract void Death();
}
