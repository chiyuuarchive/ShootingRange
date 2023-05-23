using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "State/RailMovement")]
/// <summary>
/// Moves back and fort between two predetermined positions
/// </summary>
public class SRailMovement : State
{
    private Rigidbody rigidbody;
    private MovementInfo movementInfo;
    private PatrolPoints patrolPoints;
    private Vector3 nextPoint;

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

    }

    public override void Exit(Context contex)
    {
        rigidbody.velocity = Vector3.zero;
    }

    public override void UpdateState(Context contex)
    {
        if (AtPoint(nextPoint))
            SwitchNextPoint(); // start moving towards other point instead

        Vector3 dirToPoint = GetDirToPoint(nextPoint);


        rigidbody.velocity = dirToPoint * movementInfo.Speed * Time.deltaTime;
    }

    private Vector3 GetDirToPoint(Vector3 point)
    {
        return (point - rigidbody.transform.position).normalized;
    }

    private bool AtPoint(Vector3 point)
    {
        // may need to change how close to point we need to be
        return Vector3.Distance(rigidbody.transform.position, point) < 0.1f;
    }

    private void SwitchNextPoint()
    {
        if (nextPoint == patrolPoints.PointA)
            nextPoint = patrolPoints.PointB;
        else
            nextPoint = patrolPoints.PointA;
    }

}
