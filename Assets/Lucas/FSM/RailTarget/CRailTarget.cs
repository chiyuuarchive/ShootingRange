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

    public IntEventSO updateScoreEvent;
    private StateFlags flags;


    private void Awake()
    {
        flags = gameObject.GetComponent<StateFlags>();
    }

    protected override void Start()
    {
        base.Start();
        GetComponent<HitHandler>().OnHit += GetHit;
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
        updateScoreEvent?.Invoke(1);
        //increase score
    }
}
