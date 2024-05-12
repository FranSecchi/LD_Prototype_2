using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurEffect : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    public float maxBlurDistance = 10f;
    public float minBlurDistance = 1f;

    private DepthOfField depthOfField;

    private void Start()
    {
        postProcessVolume.profile.TryGetSettings(out depthOfField);
    }

    public void SetBlurIntensity(float blurIntensity)
    {
        // Clamp blur intensity between 0 and 1
        float normalizedBlurIntensity = Mathf.Clamp01(blurIntensity);

        // Map normalized blur intensity to distance range
        float targetFocusDistance = Mathf.Lerp(minBlurDistance, maxBlurDistance, normalizedBlurIntensity);

        // Set focus distance for depth of field effect
        depthOfField.focusDistance.value = targetFocusDistance;
    }
}
