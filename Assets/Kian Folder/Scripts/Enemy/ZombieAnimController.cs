using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimController : MonoBehaviour
{
    public Animator animator;

    //private float oMoveSpeed;

    //private float moveTimer;
    //private void Start()
    //{
    //    oMoveSpeed = GetComponent<EnemyParent>().moveSpeed;
    //}

    private void Update()
    {
        //moveTimer -= Time.deltaTime;
        var playerDistance = Mathf.Abs(transform.position.x - PlayerMovement.instance.transform.position.x);
        if (GetComponent<EnemyParent>().currentStun <= 0)
        {
            animator.speed = 1;

        }
        else
            animator.speed = 0;

        animator.SetFloat("PlayerDist", playerDistance);
        //if (playerDistance <= 1)
        //{
        //    GetComponent<EnemyParent>().moveSpeed = 0;
        //    moveTimer = 1;
        //}

        //if (moveTimer <= 0 && GetComponent<EnemyParent>().moveSpeed == 0)
        //{
        //    GetComponent<EnemyParent>().moveSpeed = oMoveSpeed;
        //}
    }
}
