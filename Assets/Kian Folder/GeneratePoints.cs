using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePoints : MonoBehaviour
{
    public GameObject point;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(point, new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), transform.rotation);
        }
    }
}
