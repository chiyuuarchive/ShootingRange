using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    protected int id;

    public int ID { get { return id; } set { id = value; } }

    public abstract void Enter(Context contex);
    public abstract void UpdateState(Context contex);
    public abstract void Exit(Context contex);

    public enum States
    {
        Move,
        Idle,
        Inactivating,
        Activating
    }
}
