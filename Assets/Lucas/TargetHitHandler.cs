using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitHandler : HitHandler
{
    [Header("Score")]
    [SerializeField] private IntEventSO updateScoreEvent;
    [SerializeField] private EventSO updateTargetImmunityEvent;
    [SerializeField] private int scoreOnHit;

    private StateFlags flags;
    private bool isImmune;

    private void Start()
    {
        flags = GetComponent<StateFlags>();
        isImmune = true;

        updateTargetImmunityEvent.list += UpdateImmunity;
    }

    void OnDestroy() => updateTargetImmunityEvent.list -= UpdateImmunity;
    void UpdateImmunity() => isImmune = false;

    public override void GetHit()
    {
        if (!flags.Active || isImmune) return;

        base.GetHit();
        updateScoreEvent?.Invoke(scoreOnHit);
    }
}
