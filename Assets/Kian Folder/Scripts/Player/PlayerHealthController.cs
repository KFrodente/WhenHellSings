using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] public float iFrameTime = 0;
    public float knockbackSpeed;

    private void Awake()
    {
        instance = this;
        health = maxHealth;
    }

    private void Update()
    {
        iFrameTime -= Time.deltaTime;
    }

    public void HitPlayer(float frameTime, int damage)
    {
        if (iFrameTime <= 0.0f)
        {
            if (PlayerMovement.instance.facingRight)
                PlayerMovement.instance.theRB.AddForce(new Vector2(-2.0f * knockbackSpeed, .75f * knockbackSpeed));
            else
                PlayerMovement.instance.theRB.AddForce(new Vector2(2.0f * knockbackSpeed, .75f * knockbackSpeed));


            Debug.Log("did frames");
            iFrameTime = frameTime;

            health -= damage;

            if (health <= 0.0f)
            {
                //TODO: Death animation to death screen or title screen
                Destroy(gameObject);
            }
        }
    }
}
