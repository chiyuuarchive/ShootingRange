using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Temporary name until better name is discovered & more things r needed here
public class MovementInfo : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    //[SerializeField] private float speed;

    public float Speed { get { return speed; } }
}
