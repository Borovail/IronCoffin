using System.Collections;
using UnityEngine;

public class FogEffect : MonoBehaviour,IEndGameEffect
{
    public float targetFogDensity = 0.1f; // Плотность тумана
    public float transitionDuration = 5f; // Время для увеличения тумана

    private float originalFogDensity;

    void Start()
    {
        originalFogDensity = RenderSettings.fogDensity;
    }

    private IEnumerator ApplyFog()
    {
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(originalFogDensity, targetFogDensity, elapsed / transitionDuration);
            yield return null;
        }

        RenderSettings.fogDensity = targetFogDensity;
    }

    public void EndEffect()
    {
        StartCoroutine(ApplyFog());
    }
}