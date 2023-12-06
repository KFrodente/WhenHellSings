using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public Animator animator;
    private void SetBools(bool idle, bool walking, bool attacking, bool whipping)
    {
        animator.SetBool("Idle", idle);
        animator.SetBool("Walking", walking);
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Whipping", whipping);
    }
}
