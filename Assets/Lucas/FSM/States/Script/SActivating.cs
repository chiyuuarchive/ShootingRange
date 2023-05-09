using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "State/Activating")]
public class SActivating : State
{
    Animator animator;
    public override void Enter(Context contex)
    {
        if (animator == null)
            animator = contex.GetComponent<Animator>();

        animator.SetBool("hit", false);
    }

    public override void Exit(Context contex)
    {
    }

    public override void UpdateState(Context contex)
    {
    }
}
