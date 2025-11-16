using UnityEngine;

public class BodyController
{
    Rigidbody rb;
    float speed = 10.0f;

    public BodyController(Rigidbody rb)
    {
        this.rb = rb;
    }

    public void ToLeft()
    {
        rb.AddForce(Vector3.left * speed);
    }

    public void ToRight()
    {
        rb.AddForce(Vector3.right * speed);
    }

    public void ToBack()
    {
        rb.AddForce(Vector3.back * speed);
    }

    public void ToForward()
    {
        rb.AddForce(Vector3.forward * speed);
    }
}
