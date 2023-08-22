using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour {
    public float fadeInDuration = 1.0f; // Duration of the fade-in effect in seconds
    public float titleFadeDelay = 0.5f; // Delay before fading in the title

    private CanvasGroup panelCanvasGroup;

    private void Start() {
        panelCanvasGroup = GetComponent<CanvasGroup>();

        // Initialize the panel's alpha to 0 to start with a faded appearance
        panelCanvasGroup.alpha = 0f;

        // Start the fading process
        StartCoroutine(FadeInRoutine());
    }

    private System.Collections.IEnumerator FadeInRoutine() {
        yield return new WaitForSeconds(titleFadeDelay);

        float currentTime = 0f;
        while (currentTime < fadeInDuration) {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeInDuration);
            panelCanvasGroup.alpha = alpha;

            currentTime += Time.deltaTime;
            yield return null;
        }

        panelCanvasGroup.alpha = 1f; // Ensure full visibility at the end of the fade
    }
}


