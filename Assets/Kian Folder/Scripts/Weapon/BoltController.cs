using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float boltSpeed;
    public int boltDamage;

    public float stunTime;

    public float lifespan;

    [HideInInspector]public Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PauseManager.instance.paused) return;

        rb.velocity = direction * boltSpeed;
        lifespan -= Time.deltaTime;
        if (lifespan < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PauseManager.instance.paused) return;

        if (other.tag == "Enemy")
            other.GetComponent<EnemyParent>().TakeDamage(boltDamage, stunTime);

        if (other.tag != "Player" && other.tag != "EnemyWall" && other.tag != "Bounce" && other.tag != "Trigger")
            Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
