using System.Collections;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public float swingSpeed = 1.5f;
    public float swingAmount = 15f;
    public float swingRandomness = 5f;

    public float minFlickerTime = 0.05f;
    public float maxFlickerTime = 0.2f;
    public float flickerChance = 0.1f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    public float maxSwingAngle = 20f; // Ограничение максимального угла качания
    public float maxSwingSpeed = 2.5f; // Ограничение скорости раскачивания

    private Light lamp;
    private float flickerTimer;
    private float baseSwingSpeed;
    private float baseSwingAmount;
    private float randomSwingFactor;
    private float swingTimer;

    private void Start()
    {
        lamp = GetComponentInChildren<Light>();
        ResetFlickerTimer();
        baseSwingSpeed = swingSpeed;
        baseSwingAmount = swingAmount;
    }

    private void Update()
    {
        // Генерация случайного фактора качания с плавной сменой
        swingTimer += Time.deltaTime * 0.5f;
        randomSwingFactor = Mathf.Lerp(randomSwingFactor, Mathf.PerlinNoise(swingTimer, 0) * swingRandomness, Time.deltaTime * 2f);

        // Ограничиваем максимальную амплитуду и скорость раскачивания
        float currentSwingSpeed = Mathf.Clamp(baseSwingSpeed + randomSwingFactor, 0.5f, maxSwingSpeed);
        float currentSwingAmount = Mathf.Clamp(baseSwingAmount + randomSwingFactor, 5f, maxSwingAngle);

        float angle = Mathf.Sin(Time.time * currentSwingSpeed) * currentSwingAmount;
        float zAngle = Mathf.Sin(Time.time * 1.2f) * 5f;
        transform.localRotation = Quaternion.Euler(angle, 0, zAngle);

        // Цвет света меняется случайно
        lamp.color = Color.Lerp(Color.yellow, Color.white, Mathf.PerlinNoise(Time.time, 0));

        // Фликер света с плавным изменением интенсивности
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0)
        {
            if (Random.value < flickerChance)
            {
                StartCoroutine(FlickerEffect());
            }
            ResetFlickerTimer();
        }
    }

    private IEnumerator FlickerEffect()
    {
        float targetIntensity = lamp.intensity > 0 ? 0 : Random.Range(minIntensity, maxIntensity);
        float duration = 0.1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            lamp.intensity = Mathf.Lerp(lamp.intensity, targetIntensity, elapsed / duration);
            yield return null;
        }
    }

    private void ResetFlickerTimer()
    {
        flickerTimer = Random.Range(minFlickerTime, maxFlickerTime);
    }
}
