using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HeartbeatManager : MonoBehaviour
{
    public float checkInterval = 5f; // seconds between checks
    public string heartbeatApiUrl = "https://your-api-url.com/heartbeat";

    public int spikeThreshold = 100; // for spike alert UI
    public int dangerThreshold = 140; // for peaceful scene switch

    public GameObject[] spikeAlertUIs; // set these in inspector: [level1, level2, level3]
    public Transform[] teleportPoints; // teleport targets: [ground, level1, level2, level3]
    public GameObject xrOrigin;

    private bool[] spikeCooldown = new bool[3];

    void Start()
    {
        StartCoroutine(CheckHeartbeatRoutine());
    }

    IEnumerator CheckHeartbeatRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(GetHeartbeatFromAPI());
            yield return new WaitForSeconds(checkInterval);
        }
    }

    IEnumerator GetHeartbeatFromAPI()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(heartbeatApiUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                HeartbeatData data = JsonUtility.FromJson<HeartbeatData>(json);
                int bpm = data.heartbeat;

                Debug.Log("Current Heartbeat: " + bpm);

                if (bpm >= dangerThreshold)
                {
                    SceneManager.LoadScene("PeacefulScene"); // change to your peaceful scene
                }
                else if (bpm >= spikeThreshold)
                {
                    HandleSpike(bpm);
                }
            }
            else
            {
                Debug.LogWarning("API Error: " + request.error);
            }
        }
    }

    void HandleSpike(int bpm)
    {
        int levelIndex = GetCurrentLevelIndex();

        if (levelIndex >= 0 && !spikeCooldown[levelIndex])
        {
            spikeAlertUIs[levelIndex].SetActive(true);
            StartCoroutine(StartCooldown(levelIndex));
        }
    }

    IEnumerator StartCooldown(int index)
    {
        spikeCooldown[index] = true;
        yield return new WaitForSeconds(600); // 10 minutes
        spikeCooldown[index] = false;
    }

    int GetCurrentLevelIndex()
    {
        Vector3 playerPos = xrOrigin.transform.position;
        float minDistance = float.MaxValue;
        int closest = -1;

        for (int i = 0; i < teleportPoints.Length; i++)
        {
            float dist = Vector3.Distance(playerPos, teleportPoints[i].position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = i - 1; // index matches level 1 to 3 (exclude ground)
            }
        }

        return closest;
    }

    [System.Serializable]
    public class HeartbeatData
    {
        public int heartbeat;
    }
}
