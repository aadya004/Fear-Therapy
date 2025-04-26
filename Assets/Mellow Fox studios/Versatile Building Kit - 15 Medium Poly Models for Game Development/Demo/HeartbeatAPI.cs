using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HeartbeatAPI : MonoBehaviour
{
    public static HeartbeatAPI Instance;
    public string apiUrl = "https://your-api-endpoint.com/heartbeat";
    public string apiKey = "your-api-key-here";

    public int currentHeartRate = 70;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(PollHeartbeat());
    }

    IEnumerator PollHeartbeat()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                int heartRate;
                if (int.TryParse(request.downloadHandler.text, out heartRate))
                {
                    currentHeartRate = heartRate;
                }
                else
                {
                    Debug.LogWarning("HeartbeatAPI: Failed to parse heart rate.");
                }
            }
            else
            {
                Debug.LogError("HeartbeatAPI: " + request.error);
            }

            yield return new WaitForSeconds(5f); // Check every 5 seconds
        }
    }

    public static int GetHeartRate()
    {
        return Instance != null ? Instance.currentHeartRate : 0;
    }
}
