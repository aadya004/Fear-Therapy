using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class HeartbeatSceneManager : MonoBehaviour
{
    private string readUrl = "https://heartbeat-sensor.onrender.com/heartbeat";

    void Start()
    {
        InvokeRepeating("FetchBPM", 2f, 3f);
    }

    void FetchBPM()
    {
        StartCoroutine(GetHeartbeat());
    }

    IEnumerator GetHeartbeat()
    {
        UnityWebRequest www = UnityWebRequest.Get(readUrl);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            string json = www.downloadHandler.text;
            int bpm = int.Parse(json.Split(new string[] { "\"bpm\":" }, System.StringSplitOptions.None)[1].Split('}')[0]);

            Debug.Log("BPM: " + bpm);
            if (bpm < 70)
                SceneManager.LoadScene("CalmScene");
            else if (bpm < 100)
                SceneManager.LoadScene("NeutralScene");
            else
                SceneManager.LoadScene("AlertScene");
        }
        else
        {
            Debug.LogError("Error fetching BPM: " + www.error);
        }
    }
}
