using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // For API fetching
using System.Text.RegularExpressions; // For number parsing

public class FlickerLights : MonoBehaviour
{
    public Light[] pointLights;             // Lights to control
    public float flickerSpeedLow = 0.05f;    // Fast flicker when scared
    public float flickerSpeedMedium = 0.2f;  // Moderate flicker
    public float flickerSpeedHigh = 1f;      // No flicker (lights stay ON)

    public float flickerDurationLow = 2f;
    public float flickerDurationMedium = 1f;
    public float flickerDurationHigh = 0f;

    public float blackoutDurationLow = 2f;
    public float blackoutDurationMedium = 1f;
    public float blackoutDurationHigh = 0f;

    public string bpmApiUrl = "https://api.thinkv.space/channels/ad809a9e-f821-47e6-9e15-796e8df4d393"; // Your API URL

    private float currentBPM = 80f;   // Default BPM
    private bool bpmFetchFailed = false; // To know if API failed

    private void Start()
    {
        InvokeRepeating(nameof(FetchBPMFromAPI), 2f, 5f); // Fetch BPM every 5 seconds
        Invoke(nameof(StartLightSequence), 3f);           // Start flickering after 3 seconds
    }

    void StartLightSequence()
    {
        StartCoroutine(LightSequence());
    }

    IEnumerator LightSequence()
    {
        while (true)
        {
            if (bpmFetchFailed)
            {
                // API fetch failed - Run old default behavior
                yield return StartCoroutine(DefaultLightBehavior());
            }
            else
            {
                // API fetch succeeded - Run based on BPM
                if (currentBPM < 90f)
                {
                    // Heavy flicker
                    yield return StartCoroutine(FlickerEffect(flickerSpeedLow, flickerDurationLow));

                    SetLightsState(false);
                    yield return new WaitForSeconds(blackoutDurationLow);

                    SetLightsState(true);
                    yield return new WaitForSeconds(1f);
                }
                else if (currentBPM >= 90f && currentBPM <= 120f)
                {
                    // Medium flicker
                    yield return StartCoroutine(FlickerEffect(flickerSpeedMedium, flickerDurationMedium));

                    SetLightsState(false);
                    yield return new WaitForSeconds(blackoutDurationMedium);

                    SetLightsState(true);
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    // No flicker, lights stay on
                    SetLightsState(true);
                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }

    IEnumerator DefaultLightBehavior()
    {
        // Old behavior (no BPM check)
        yield return StartCoroutine(FlickerEffect(0.1f, 2f));

        SetLightsState(false);
        yield return new WaitForSeconds(2f);

        SetLightsState(true);
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(FlickerEffect(0.1f, 2f));
    }

    IEnumerator FlickerEffect(float flickerSpeed, float flickerDuration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < flickerDuration)
        {
            foreach (Light light in pointLights)
            {
                light.enabled = Random.value > 0.5f;
            }

            yield return new WaitForSeconds(flickerSpeed);
            elapsedTime += flickerSpeed;
        }

        SetLightsState(true); // Restore lights after flicker
    }

    void SetLightsState(bool state)
    {
        foreach (Light light in pointLights)
        {
            light.enabled = state;
        }
    }

    void FetchBPMFromAPI()
    {
        StartCoroutine(GetBPMCoroutine());
    }

    IEnumerator GetBPMCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(bpmApiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            if (json.Contains("field1"))
            {
                int index = json.IndexOf("field1");
                string part = json.Substring(index);
                string numberOnly = new string(Regex.Replace(part, "[^0-9]", "").ToCharArray());

                if (int.TryParse(numberOnly, out int bpm))
                {
                    currentBPM = bpm;
                    bpmFetchFailed = false;
                    Debug.Log("Fetched BPM: " + currentBPM);
                }
                else
                {
                    Debug.LogWarning("BPM parsing failed, using default behavior.");
                    bpmFetchFailed = true;
                }
            }
            else
            {
                Debug.LogWarning("BPM field missing in API response.");
                bpmFetchFailed = true;
            }
        }
        else
        {
            Debug.LogWarning("Failed to fetch BPM: " + request.error);
            bpmFetchFailed = true;
        }
    }
}
