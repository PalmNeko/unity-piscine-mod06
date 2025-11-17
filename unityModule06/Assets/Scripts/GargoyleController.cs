using UnityEngine;

public class GargoyleController : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GhostController.emergencyMode = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GhostController.emergencyMode = false;
    }
}
