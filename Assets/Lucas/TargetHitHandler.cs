using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitHandler : HitHandler
{
    [Header("Score")]
    [SerializeField] private IntEventSO updateScoreEvent;
    [SerializeField] private int scoreOnHit;

    private StateFlags flags;

    private void Start()
    {
        flags = GetComponent<StateFlags>();
    }

    public override void GetHit()
    {
        if (!flags.Active) return;

        base.GetHit();
        updateScoreEvent?.Invoke(scoreOnHit);
    }
}
