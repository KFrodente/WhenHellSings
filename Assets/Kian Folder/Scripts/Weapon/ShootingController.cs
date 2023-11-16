using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Transform firePoint;

    public GameObject currentBolt;

    [Range(0f, 3f)] public float firerate;

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
        }
    }
}
