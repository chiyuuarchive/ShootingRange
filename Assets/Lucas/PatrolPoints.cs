using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [Tooltip("The starting patrolposition")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    public Vector3 PointA { get { return pointA.position; } }
    public Vector3 PointB { get { return pointB.position; } }
}
