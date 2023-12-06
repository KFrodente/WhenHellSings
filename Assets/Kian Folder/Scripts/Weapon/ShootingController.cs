using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform firePoint;

    public GameObject currentBolt;

    public GameObject nBolt;
    public GameObject lBolt;
    public AudioClip shootSound;

    [Range(0f, 3f)] public float firerate;

    public float pauseMoveTime;
    private float pauseMoveCounter;
    private bool frozen = false;


    private float counter;

    private void Start()
    {
        counter = firerate;
        currentBolt = nBolt;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.instance.paused) return;

        if (Input.GetButtonDown("SwitchBolt"))
        {
            if (currentBolt == nBolt) currentBolt = lBolt;
            else if (currentBolt == lBolt) currentBolt = nBolt;
        }

        counter -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && counter <= 0)
        {
            float pitch = Random.Range(0.5f, 1.2f);
            AudioController.instance.PlaySound(shootSound, GetComponent<AudioSource>(), pitch);
            PlayerMovement.instance.animator.SetBool("Attacked", true);
            GameObject bolt = Instantiate(currentBolt, firePoint.position, transform.rotation);
            bolt.GetComponent<BoltController>().direction = (PlayerMovement.instance.facingRight) ? Vector2.right : Vector2.left;
            counter = firerate;
            pauseMoveCounter = pauseMoveTime;
        }

        //after the player shoots this stops them from moving for an amount of time that equals the "pauseMoveTime" variable
        if (pauseMoveCounter > 0)
        {
            pauseMoveCounter -= Time.deltaTime;
            if (!frozen)
            {
                PlayerMovement.instance.canMove = false;
                frozen = true;
            }
        }
        if (pauseMoveCounter <= 0 && frozen)
        {
            PlayerMovement.instance.animator.SetBool("Attacked", false);
            PlayerMovement.instance.canMove = true;
            frozen = false;
        }

    }
}
