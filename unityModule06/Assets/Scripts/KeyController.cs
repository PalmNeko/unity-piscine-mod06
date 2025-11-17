using UnityEngine;

public class KeyController : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector3 localAngle = transform.localEulerAngles;
        localAngle.z += 1.0f;
        transform.localEulerAngles = localAngle;
    }
}
