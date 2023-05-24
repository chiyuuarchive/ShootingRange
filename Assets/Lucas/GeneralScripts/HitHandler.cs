using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public event Action OnHit;

    public virtual void GetHit()
    {
        OnHit?.Invoke();
    }

}
