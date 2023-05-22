using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitHandler : HitHandler
{
    [Header("Score")]
    [SerializeField] private IntEventSO updateScoreEvent;
    [SerializeField] private int scoreOnHit;

    public override void GetHit()
    {
        base.GetHit();
        updateScoreEvent?.Invoke(scoreOnHit);
    }
}
