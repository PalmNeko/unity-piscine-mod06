using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    InputController inputController = new();
    BodyController bodyController;
    AnimationController animationController;

    void OnEnable()
    {
        Dictionary<string, Func<bool>> inputs = new (){
            {"Left", () => Input.GetKey("a")},
            {"Right", () => Input.GetKey("d")},
            {"Forward", () => Input.GetKey("w")},
            {"Back", () => Input.GetKey("s")},
        };
        foreach (string label in inputs.Keys)
        {
            inputController.AddInput(label, new KeyInput(inputs[label]));
        }
        Animator animator = GetComponent<Animator>();
        bodyController = new(transform);
        animationController = new(() =>
        {
            bool isWalk = inputController.Get("Left")
                || inputController.Get("Forward")
                || inputController.Get("Right")
                || inputController.Get("Back");
            animator.SetBool("Walking", isWalk);
        });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (inputController.Get("Left"))
            bodyController.ToLeft();
        if (inputController.Get("Right"))
            bodyController.ToRight();
        if (inputController.Get("Forward"))
            bodyController.ToForward();
        if (inputController.Get("Back"))
            bodyController.ToBack();
    }

    // Update is called once per frame
    void Update()
    {
        inputController.Update();
        animationController.Update();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Defeat");
        }
    }
}
