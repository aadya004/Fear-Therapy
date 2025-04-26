using System.Collections;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    public Light[] pointLights;
    public float flickerSpeed = 0.1f;

    private void Start()
    {
        Invoke("StartLightSequence", 3f); // Optional delay before starting
    }

    void StartLightSequence()
    {
        StartCoroutine(LightCycleLoop());
    }

    IEnumerator LightCycleLoop()
    {
        while (true)
        {
            // 1. Lights ON for 2 minutes
            SetLightsState(true);
            yield return new WaitForSeconds(120f);

            // 2. Flicker for 10 seconds
            yield return StartCoroutine(FlickerEffect(10f));

            // 3. Lights OFF for 45 seconds
            SetLightsState(false);
            yield return new WaitForSeconds(45f);
        }
    }

    IEnumerator FlickerEffect(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            foreach (Light light in pointLights)
            {
                light.enabled = Random.value > 0.5f;
            }

            yield return new WaitForSeconds(flickerSpeed);
            elapsedTime += flickerSpeed;
        }

        SetLightsState(true); // Reset to lights ON after flicker
    }

    void SetLightsState(bool state)
    {
        foreach (Light light in pointLights)
        {
            light.enabled = state;
        }
    }
}



