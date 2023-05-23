using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    protected int id;

    public int ID { get { return id; } set { id = value; } }

    public virtual void Enter(Context contex)
    {
        Debug.Log("Entering " + this.name);
    }
    public abstract void UpdateState(Context contex);
    public abstract void Exit(Context contex);
}
