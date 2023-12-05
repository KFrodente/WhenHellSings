using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (PlayerMovement.instance.theRB.velocity.x == 0)
        {
            //animator.
        }
    }
}
