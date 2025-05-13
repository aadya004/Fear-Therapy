using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneLoader : MonoBehaviour
{
    public void LoadNyctophobiaScene()
    {
        LoadSceneWithSpawn("Nycto");
    }

    public void LoadClaustrophobiaScene()
    {
        LoadSceneWithSpawn("mazeeeeee");
    }

    public void LoadAcrophobiaScene()
    {
        LoadSceneWithSpawn("Acro");
    }

    public void LoadMainScene()
    {
        LoadSceneWithSpawn("mainmenu");
    }

    private void LoadSceneWithSpawn(string sceneName)
    {
        // Start the scene loading asynchronously
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Find the XR Origin in the current scene
        GameObject xrOrigin = GameObject.Find("XR Origin");

        // If the XR Origin exists, move it to the spawn point after the scene is loaded
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            // Find the PlayerSpawn object in the newly loaded scene
            GameObject spawnPoint = GameObject.Find("PlayerSpawn");

            if (spawnPoint != null && xrOrigin != null)
            {
                // Move the XR Origin to the spawn point
                xrOrigin.transform.position = spawnPoint.transform.position;
                xrOrigin.transform.rotation = spawnPoint.transform.rotation;
            }
            else
            {
                Debug.LogWarning("PlayerSpawn or XR Origin not found.");
            }
        };
    }
}
