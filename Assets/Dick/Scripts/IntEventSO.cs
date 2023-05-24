using System;
using UnityEngine;

[CreateAssetMenu(menuName = "IntegerEvent")]
public class IntEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<int> list;

    // Invoke with a integer type input parameter
    public void Invoke(int value) => list?.Invoke(value);
}
