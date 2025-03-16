using System.Collections;
using UnityEngine;

public class TimeSlow : MonoBehaviour,IEndGameEffect
{
    public float slowFactor = 0.1f; // Во сколько раз замедляется время
    public float slowDuration = 5f; // Длительность замедления

    private IEnumerator SlowTimeCoroutine()
    {
        float originalTimeScale = Time.timeScale; // Сохраняем исходное значение Time.timeScale

        float elapsed = 0f;
        while (elapsed < slowDuration)
        {
            elapsed += Time.unscaledDeltaTime; // Используем unscaledDeltaTime, чтобы не зависело от времени
            Time.timeScale = Mathf.Lerp(originalTimeScale, slowFactor, elapsed / slowDuration); // Плавно замедляем

            yield return null;
        }

        Time.timeScale = 0; // Устанавливаем конечное значение времени
    }

    public void EndEffect()
    {
        StartCoroutine(SlowTimeCoroutine());
    }
}