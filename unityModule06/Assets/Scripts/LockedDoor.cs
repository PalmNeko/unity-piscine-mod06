using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public PlayerController player;
    public DoorController door;

    void Start()
    {
        door.enabled = false;
    }

    void Update()
    {
        if (player.CanClear())
            door.enabled = true;
    }
}
