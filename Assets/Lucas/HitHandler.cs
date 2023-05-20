using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public event Action OnHit;
    private void Awake()
    {
    }
    public virtual void GetHit()
    {
        OnHit?.Invoke();
        //healthSystem.TakeDamage(damageAmount);
    }

    //public virtual void GetHit(float damageAmount, Vector2 impactPoint)
    //{
    //    GetHit(damageAmount);
    //}
}
