using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRailTarget : Context
{
    public bool active = true;
    protected override int CheckTransitions()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (active)
                active = false;
            else
                active = true;

            if (active)
                return 1;
            else 
                return 2;
        }

        return defaultState;
    }
}
