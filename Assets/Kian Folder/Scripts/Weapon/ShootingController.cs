using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform firePoint;

    public GameObject currentBolt;

    [Range(0f, 3f)] public float firerate;

    public float pauseMoveTime;
    private float pauseMoveCounter;

    private float counter;

    private void Start()
    {
        counter = firerate;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && counter <= 0)
        {
            GameObject bolt = Instantiate(currentBolt, firePoint.position, transform.rotation);
            bolt.GetComponent<BoltController>().direction = (PlayerMovement.instance.facingRight) ? Vector2.right : Vector2.left;
            counter = firerate;
            pauseMoveCounter = pauseMoveTime;
        }

        //after the player shoots this stops them from moving for an amount of time that equals the "pauseMoveTime" variable
        if (pauseMoveCounter > 0)
        {
            pauseMoveCounter -= Time.deltaTime;
            if (PlayerMovement.instance.canMove)
                PlayerMovement.instance.canMove = false;
        }
        if (pauseMoveCounter <= 0 && !PlayerMovement.instance.canMove)
        {
            PlayerMovement.instance.canMove = true;
        }

    }
}
