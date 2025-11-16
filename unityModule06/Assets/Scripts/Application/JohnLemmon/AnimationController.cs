using System;
using UnityEngine;

public class AnimationController
{
    Action updater;

    public AnimationController(Action updater)
    {
        this.updater = updater;
    }

    public void Update()
    {
        updater();
    }
}
