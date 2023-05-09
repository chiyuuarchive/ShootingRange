using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "State/RailMovement")]
/// <summary>
/// Moves back and fort between two predetermined positions
/// </summary>
public class SRailMovement : State
{
    private Rigidbody moveObject;
    private MovementInfo movementInfo;
    private PatrolPoints patrolPoints;
    private Vector3 point;

    public override void Enter(Context contex)
    {
        //Get relevant variables to move between 2 positions
        moveObject = contex.GetComponent<Rigidbody>();
        movementInfo = contex.GetComponent<MovementInfo>();
        patrolPoints = contex.GetComponent<PatrolPoints>();
        if (point == null)
            point = patrolPoints.PointA;

    }

    public override void Exit(Context contex)
    {
        moveObject.velocity= Vector3.zero;
    }

    public override void UpdateState(Context contex)
    {
        if (AtPoint(point))
            SwitchPoint(); // start moving towards other point instead

        Vector3 dirToPoint = GetDirToPoint(point);


        moveObject.velocity = dirToPoint * movementInfo.Speed * Time.deltaTime;
    }

    public Vector3 GetDirToPoint(Vector3 point)
    {
        return (point - moveObject.transform.position).normalized;
    }

    public bool AtPoint(Vector3 point)
    {
        // may need to change how close to point we need to be
        return Vector3.Distance(moveObject.transform.position, point) < 0.1f; 
    }

    public void SwitchPoint()
    {
        if (point == patrolPoints.PointA)
            point = patrolPoints.PointB;
        else
            point = patrolPoints.PointA;
    }

}
