using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class EventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action list;

    public void Invoke() => list?.Invoke();
}
