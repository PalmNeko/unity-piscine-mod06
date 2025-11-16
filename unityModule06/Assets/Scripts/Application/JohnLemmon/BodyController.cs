using UnityEngine;

public class BodyController
{
    Transform transform;
    float speed = 0.1f;

    public BodyController(Transform transform)
    {
        this.transform = transform;
    }

    public void ToLeft()
    {
        RotateTo(Vector3.left);
    }

    public void ToRight()
    {
        RotateTo(Vector3.right);
    }

    public void ToBack()
    {
        RotateTo(Vector3.back);
    }

    public void ToForward()
    {
        RotateTo(Vector3.forward);
    }

    private void RotateTo(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);
    }
}
