using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenuAttribute(menuName = "State/Inactivating")]
public class SInactivating : State
{

    Animator animator;
    StateFlags flags;

    public override void Enter(Context contex)
    {
        base.Enter(contex);

        if (animator == null)
            animator = contex.GetComponent<Animator>();
        if (!flags)
            flags = contex.GetComponent<StateFlags>();

        animator.SetBool("hit", true);
        flags.Active = false;
    }

    public override void Exit(Context contex)
    {
    }

    public override void UpdateState(Context contex)
    {
    }
}
