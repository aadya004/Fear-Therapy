using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils; // Required for XR Origin
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Transform[] levelStartPoints; // 0 - Ground, 1 - Level 1, etc.
    public GameObject[] levelUIs;
    public GameObject[] spikeUIs;
    public GameObject emergencyScene; // Peaceful scene GameObject
    public float alertThreshold = 100f;
    public float emergencyThreshold = 140f;
    public XROrigin xrOrigin; // Reference to the XR Origin in your scene

    private bool[] spikeCooldowns = new bool[3];

    void Update()
    {
        int heartbeat = HeartbeatAPI.GetHeartRate();

        if (heartbeat > emergencyThreshold)
        {
            LoadEmergencyScene();
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            if (heartbeat > alertThreshold && !spikeCooldowns[i])
            {
                ShowSpikeAlert(i);
            }
        }
    }

    public void GoToLevel(int levelIndex)
    {
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin is not assigned!");
            return;
        }

        Transform target = levelStartPoints[levelIndex];

        // Move the XR Origin's position to the new location, while keeping head height intact
        Vector3 offset = xrOrigin.Camera.transform.position - xrOrigin.transform.position;
        xrOrigin.transform.position = target.position - offset;
    }

    public void ShowLevelUI(int levelIndex)
    {
        levelUIs[levelIndex].SetActive(true);
    }

    public void HandleSpikeDecision(int levelIndex, bool continueChallenge)
    {
        spikeUIs[levelIndex].SetActive(false);
        if (continueChallenge)
        {
            StartCoroutine(SpikeCooldownTimer(levelIndex, 600)); // 10 minutes
        }
        else
        {
            GoToLevel(0); // Ground floor
        }
    }

    void ShowSpikeAlert(int levelIndex)
    {
        spikeUIs[levelIndex].SetActive(true);
    }

    IEnumerator SpikeCooldownTimer(int levelIndex, float seconds)
    {
        spikeCooldowns[levelIndex] = true;
        yield return new WaitForSeconds(seconds);
        spikeCooldowns[levelIndex] = false;
    }

    void LoadEmergencyScene()
    {
        SceneManager.LoadScene("PeacefulScene");
    }

    public void HandleFinalChoice(string choice)
    {
        if (choice == "party")
        {
            // Trigger celebration animation or event
        }
        else if (choice == "sleep")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
