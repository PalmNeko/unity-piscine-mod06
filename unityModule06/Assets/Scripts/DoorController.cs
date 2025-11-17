using UnityEngine;

public class DoorController : MonoBehaviour
{
    public BoxCollider doorWingCollider;
    public Animator animator;

    private bool isOpend = false;
    private float closeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpend && Time.time > closeTime)
        {
            animator.SetTrigger("Close");
            isOpend = false;
        }
    }

    void SetCloseTimer()
    {
        closeTime = Time.time + 3.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isOpend == false)
        {
            animator.SetTrigger("Open");
            isOpend = true;
            SetCloseTimer();
        }
    }

    void OnTriggerStay(Collider other)
    {
        SetCloseTimer();
    }
}
