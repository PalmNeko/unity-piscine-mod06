using UnityEngine;
using System;

public class KeyInput
{
    Func<bool> getKeyStatus;
    bool result;

    public KeyInput(Func<bool> getKeyStatus)
    {
        this.getKeyStatus = getKeyStatus;
    }

    public void Update()
    {
        result = getKeyStatus();
    }

    public bool Get()
    {
        return result;
    }
}
