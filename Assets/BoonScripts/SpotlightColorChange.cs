using UnityEngine;

public class SpotlightColorChange : MonoBehaviour {
    public AudioSource audioSource; // Reference to your audio source.
    public Color minColor = Color.blue; // Minimum color when audio is quiet.
    public Color maxColor = Color.red; // Maximum color when audio is loud.

    private Light spotlightLight;

    private void Start() {
        spotlightLight = GetComponent<Light>();
    }

    private void Update() {
        // Calculate the decibel level from the audio source.
        float decibelLevel = GetDecibelLevel();

        // Map the decibel level to a color range.
        Color newColor = Color.Lerp(minColor, maxColor, Mathf.InverseLerp(-80f, 0f, decibelLevel));

        // Set the spotlight color.
        spotlightLight.color = newColor;
    }

    private float GetDecibelLevel() {
        // Get the current RMS amplitude of the audio.
        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0);

        float sum = 0;
        foreach (var sample in samples) {
            sum += sample * sample;
        }
        float rmsValue = Mathf.Sqrt(sum / samples.Length);

        // Convert the RMS amplitude to decibels.
        float dbValue = 20f * Mathf.Log10(rmsValue / 0.1f);

        return dbValue;
    }
}
