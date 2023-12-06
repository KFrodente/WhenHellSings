using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhip : MonoBehaviour
{
    public static PlayerWhip instance;

    public AudioClip whipSound;

    public float oWhipHealCooldown;
    [HideInInspector] public float whipHealCounter;

    public GameObject whipArea;

    public GameObject whipSprite;

    
    [Tooltip("How fast the player can attack with the whip")]public float oWhipAttackCooldown;
    private float whipAttackCounter;

    [Tooltip("How long the whip stays active")]public float whipLifespan;
    private float whipLifespanCounter;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PauseManager.instance.paused) { return; }

        whipHealCounter -= Time.deltaTime;


        if (whipAttackCounter > 0)
        {
            whipAttackCounter -= Time.deltaTime;
        }

        if (whipLifespanCounter > 0)
        {
            PlayerMovement.instance.canMove = false;
            whipLifespanCounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Fire2"))
        {
            if (whipAttackCounter <= 0)
            {
                AudioController.instance.PlaySound(whipSound, GetComponent<AudioSource>());

                PlayerMovement.instance.animator.SetBool("Whipped", true);
                whipArea.SetActive(true);
                whipArea.GetComponent<WhipController>().hitEnemies.Clear();

                whipAttackCounter = oWhipAttackCooldown;
                whipLifespanCounter = whipLifespan;
            }
        }

        if (whipLifespanCounter <= 0 && whipArea.activeInHierarchy)
        {
            PlayerMovement.instance.animator.SetBool("Whipped", false);
            PlayerMovement.instance.canMove = true;
            whipArea.SetActive(false);
        }
    }


}
