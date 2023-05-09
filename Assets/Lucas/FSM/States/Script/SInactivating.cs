using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenuAttribute(menuName = "State/Inactivating")]
public class SInactivating : State
{

    Animator animator;

    public override void Enter(Context contex)
    {
        if (animator == null)
            animator = contex.GetComponent<Animator>();

        animator.SetBool("hit", true);
    }

    public override void Exit(Context contex)
    {
    }

    public override void UpdateState(Context contex)
    {
    }
}
