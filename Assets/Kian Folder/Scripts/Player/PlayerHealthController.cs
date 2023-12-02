using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] public float iFrameTime = 0;
    public float knockbackSpeed;

    public Color flashColor;
    protected int flashSpeed = 5;
    protected bool flashOn;

    protected SpriteRenderer theSR;
    private void Awake()
    {
        instance = this;
        health = maxHealth;

        theSR = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {

        if (iFrameTime > 0)
        {
            iFrameTime -= Time.deltaTime;
            Flash();
        }
        else
        {
            theSR.color = Color.white;
        }

    }

    private void Flash()
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

    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
