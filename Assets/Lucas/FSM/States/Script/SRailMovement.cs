using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Moves back and fort between two predetermined positions
/// </summary>
public class SRailMovement : State
{
    private Rigidbody rigidbody;
    private MovementInfo movementInfo;
    private PatrolPoints patrolPoints;
    private Transform nextPoint;
    private Vector3 dirToPoint;
    public override void Enter(Context contex)
    {
        base.Enter(contex);
        //Get relevant variables to move between 2 positions
        if (!rigidbody)
            rigidbody = contex.GetComponent<Rigidbody>();
        if (!movementInfo)
            movementInfo = contex.GetComponent<MovementInfo>();
        if (!patrolPoints)
            patrolPoints = contex.GetComponent<PatrolPoints>();
        if (nextPoint == null)
            nextPoint = patrolPoints.PointA;
        dirToPoint = GetDirToPoint(nextPoint);
    }

    public override void Exit(Context contex)
    {
        rigidbody.velocity = Vector3.zero;
    }

    public override void UpdateState(Context contex)
    {
        if (AtPoint(nextPoint.position))
        {
            SwitchNextPoint(); // start moving towards other point instead
            dirToPoint = GetDirToPoint(nextPoint);
            rigidbody.velocity = movementInfo.Speed * dirToPoint;
        }



        rigidbody.velocity = movementInfo.Speed * dirToPoint;
    }

    private Vector3 GetDirToPoint(Transform point)
    {
        return (point.position - rigidbody.transform.position).normalized;
    }

    private bool AtPoint(Vector3 point)
    {
        return Vector3.Distance(rigidbody.transform.position, point) <= 0.5f;
    }

    private void SwitchNextPoint()
    {
        if (nextPoint == patrolPoints.PointA)
        {
            nextPoint = patrolPoints.PointB;
        }
        else
            nextPoint = patrolPoints.PointA;
    }

}
