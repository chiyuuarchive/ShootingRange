using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField]
    int ammoCount;

    public int AmmoCount => ammoCount;
}
