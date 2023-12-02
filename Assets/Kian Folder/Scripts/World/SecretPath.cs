using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPath : MonoBehaviour
{
    [Tooltip("What object should be moved when this is hit with electric bolt?")] public GameObject thingToMove;
    [Tooltip("How fast should the Thing To Move get to its destination?")] public float speed;
    private float currentTime;
    private Vector3 startLocation;
    [Tooltip("Where should the Thing To Move go? (This is in WORLD SPACE)")]public Vector3 endLocation;



    private bool itemMoved = false;

    private bool moveItem;

    private void Start()
    {
        startLocation = thingToMove.transform.position;
    }

    private void Update()
    {
        if (!itemMoved)
        {
            if (moveItem)
            {
                currentTime += Time.deltaTime;
                thingToMove.transform.position = Vector3.Lerp(startLocation, endLocation, currentTime / speed);

                if (currentTime >= speed)
                {
                    itemMoved = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("ElectricBolt"))
        {
            moveItem = true;
        }
    }
}
