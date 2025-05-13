using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door; // Drag the corresponding hidden cube here
    private bool doorClosed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (doorClosed) return;

        if (other.CompareTag("Player")) // XR Origin should have "Player" tag
        {
            ActivateDoor();
            doorClosed = true;
        }
    }

    private void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true); // Activates the hidden door (cube)
            Debug.Log(door.name + " activated (door closed).");
        }
    }
}
