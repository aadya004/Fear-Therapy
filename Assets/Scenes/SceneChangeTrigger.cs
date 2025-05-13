using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [Tooltip("Name of the scene to load when player enters this trigger")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure your XR Rig or player GameObject is tagged as "Player"
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}