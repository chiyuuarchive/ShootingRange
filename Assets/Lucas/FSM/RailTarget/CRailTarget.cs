using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CRailTarget : Context
{
    /*
     * State list - needs to be like this in inspector to work - alt. assign states here in start function
     * 
     * 0. RailMovement
     * 1. Inactivating
     * 2. Activating
     * 3. IdleForTime
     * 
     */

    private StateFlags flags;
    [SerializeField] private float idleTime;

    protected override void Awake()
    {
        base.Awake();
        flags = gameObject.GetComponent<StateFlags>();
    }

    protected override void Start()
    {
        GetComponent<HitHandler>().OnHit += GetHit;

        AddState(new SRailMovement());
        AddState(new SInactivating());
        AddState(new SActivating());
        AddState(new SIdleForTime(idleTime));
        SetDefaultState(states[0]);

        base.Start();
    }

    protected override int CheckTransitions()
    {

        if (!flags.Hit && !flags.Active)
            return 1; //hit when active -> deactivate
        if (flags.Active)
            return 0; // not hit and active -> move

        if (flags.Idling && flags.FinnishedIdling)
            return 2;
        if (flags.Idling && !flags.FinnishedIdling || !flags.Active && flags.Hit && !flags.Idling) //keep or start idling
            return 3;


        return defaultState;
    }

    private void GetHit()
    {
        Debug.Log("GetHit");
        flags.Active = false;
    }
}
