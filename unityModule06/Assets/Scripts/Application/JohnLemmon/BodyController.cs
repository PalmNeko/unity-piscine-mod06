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
        RotateTo(- Camera.main.transform.right);
    }

    public void ToRight()
    {
        RotateTo(Camera.main.transform.right);
    }

    public void ToBack()
    {
        RotateTo(- Camera.main.transform.forward);
    }

    public void ToForward()
    {
        RotateTo(Camera.main.transform.forward);
    }

    private void RotateTo(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);
    }
}
