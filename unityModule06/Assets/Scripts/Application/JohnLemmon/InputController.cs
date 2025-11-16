using UnityEngine;
using System.Collections.Generic;

public class InputController
{
    private Dictionary<string, KeyInput> inputs = new();

    public void AddInput(string label, KeyInput input)
    {
        inputs.Add(label, input);
    }

    public void Update()
    {
        foreach (string key in inputs.Keys)
        {
            inputs[key].Update();
        }
    }

    public bool Get(string label)
    {
        return inputs[label].Get();
    }
}
