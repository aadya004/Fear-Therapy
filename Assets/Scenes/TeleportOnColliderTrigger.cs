using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportOnColliderTrigger : MonoBehaviour
{
    public Transform ground;
    public Transform one;
    public Transform two;
    public Transform three;
    public Transform groundui;

    public Transform door1;
    public Transform door2;

    private void OnTriggerEnter(Collider other)
    {
        // Check the name of the collider that was entered
        if (other.name == "Cube1")
        {
            TeleportTo(two);
            groundui.gameObject.SetActive(false);
        }
        else if (other.name == "Cube2")
        {
            TeleportTo(three);
        }
        else if (other.name == "Cube3")
        {
            TeleportTo(ground);
            groundui.gameObject.SetActive(true);
        }
        else if (other.name == "door1")
        {
            SceneManager.LoadScene("mainmenu");
        }
        else if (other.name == "door2")
        {
            SceneManager.LoadScene("Acro");
        }
    }

    void TeleportTo(Transform target)
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
            Debug.Log("Teleported to: " + target.name);
        }
        else
        {
            Debug.LogWarning("Teleport target not set!");
        }
    }
    
}
