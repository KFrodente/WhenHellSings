using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTriggerController : MonoBehaviour
{
    [SerializeField] private GameObject bird;
    [SerializeField] private bool fromLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (fromLeft)
            {
                Instantiate(bird, new Vector3(transform.position.x - 15, transform.position.y, 0), transform.rotation);
            }
            else
            {
                GameObject recentBird = Instantiate(bird, new Vector3(transform.position.x + 15, transform.position.y , 0), transform.rotation);
                recentBird.GetComponent<EnemyParent>().moveSpeed *= -1;
            }

            Destroy(gameObject);
        }

    }
}
