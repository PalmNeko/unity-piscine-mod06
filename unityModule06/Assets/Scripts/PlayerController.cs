using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public GameObject TPSCamera;
    public GameObject FPSCamera;
    public Ending ending_ui;
    public int keyCount = 0;
    public AudioSource footAudio;
    public AudioSource gameOverAudio;
    public AudioSource gameClearAudio;
    InputController inputController = new();
    BodyController bodyController;
    AnimationController animationController;
    bool isShowClear;
    bool canMove = true;

    void OnEnable()
    {
        Dictionary<string, Func<bool>> inputs = new (){
            {"Left", () => Input.GetKey("a")},
            {"Right", () => Input.GetKey("d")},
            {"Forward", () => Input.GetKey("w")},
            {"Back", () => Input.GetKey("s")},
            {"ToggleCamera", () => Input.GetKeyDown("c")},
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
            isWalk &= canMove;
            animator.SetBool("Walking", isWalk);
            footAudio.enabled = isWalk;
        });
        gameOverAudio.enabled = false;
        gameClearAudio.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ending_ui.gameObject.SetActive(false);
        keyCount = 0;
        isShowClear = false;
    }

    void FixedUpdate()
    {
        if (canMove == false)
            return ;
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

        if (inputController.Get("ToggleCamera"))
        {
            TPSCamera.SetActive(!TPSCamera.activeSelf);
            FPSCamera.SetActive(!FPSCamera.activeSelf);
            if (FPSCamera.activeSelf)
            {
                Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
            }
            else
            {
                Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Player"));
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanClear())
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                GameClear();
            }
            return ;
        }
        if (canMove && other.gameObject.CompareTag("Respawn"))
        {
            canMove = false;
            GameOver();
        }
        if (other.CompareTag("Key"))
        {
            keyCount += 1;
            Destroy(other.gameObject);
        }
    }

    void GameOver()
    {
        Action gameOver = async () => {
            ending_ui.gameObject.SetActive(true);
            ending_ui.SetImage(ImageType.GameOverImage);
            await ending_ui.FadeIn();
            ending_ui.canRestart = true;
        };
        gameOver();
        GameManager.instance.GameOver();
        gameOverAudio.enabled = true;
    }

    public void GameClear()
    {
        if (isShowClear)
            return;
        isShowClear = true;
        Action gameClear = async () => {
            ending_ui.gameObject.SetActive(true);
            ending_ui.SetImage(ImageType.GameClearImage);
            await ending_ui.FadeIn();
            ending_ui.canRestart = true;
        };
        gameClear();
        GameManager.instance.GameClear();
        gameClearAudio.enabled = true;
    }

    public bool CanClear()
    {
        return keyCount >= 3;
    }
}
