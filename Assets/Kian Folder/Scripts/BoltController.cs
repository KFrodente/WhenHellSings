using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float boltSpeed;

    public Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = direction * boltSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
