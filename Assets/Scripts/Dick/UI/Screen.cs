using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Screen : MonoBehaviour
{
    protected abstract void InitiateScreen();
    protected abstract void OnDestroyScreen();
    protected abstract void OnUpdateScreen(int msg);

    void Start()
    {
        InitiateScreen();
    }

    private void OnDestroy()
    {
        OnDestroyScreen();
    }

    public void UpdateScreen(int msg)
    {
        OnUpdateScreen(msg);
    }
}
