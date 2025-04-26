using System.Collections;
using UnityEngine;

public class FlickerLights : MonoBehaviour
{
    public Light[] pointLights;
    public float flickerSpeed = 0.1f;

    // Timings
    public float initialLightOnDuration = 60f;  // First lights on
    public float flickerDuration = 15f;         // Flicker time
    public float blackoutDuration = 25f;        // Blackout time
    public float repeatLightOnDuration = 60f;   // Light on after blackout

    private void Start()
    {
        Invoke("StartLightSequence", 3f); // Optional delay before starting
    }

    void StartLightSequence()
    {
        StartCoroutine(LightSequence());
    }

    IEnumerator LightSequence()
    {
        // Step 1: Initial lights ON
        SetLightsState(true);
        yield return new WaitForSeconds(initialLightOnDuration);

        // Start looping
        while (true)
        {
            // Step 2: Flicker
            yield return StartCoroutine(FlickerEffect());

            // Step 3: Blackout
            SetLightsState(false);
            yield return new WaitForSeconds(blackoutDuration);

            // Step 4: Lights ON for 60 sec
            SetLightsState(true);
            yield return new WaitForSeconds(repeatLightOnDuration);
        }
    }

    IEnumerator FlickerEffect()
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

        SetLightsState(false); // End flicker with lights off before blackout
    }

    void SetLightsState(bool state)
    {
        foreach (Light light in pointLights)
        {
            light.enabled = state;
        }
    }
}




