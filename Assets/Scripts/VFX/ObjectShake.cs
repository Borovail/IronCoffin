using Assets.Scripts;
using UnityEngine;

public class ObjectShake : MonoBehaviour,IEngineEffect
{
    public float intensity = 0.01f; // Базовая интенсивность
    public float speed = 10f; // Скорость тряски
    public float transitionDuration = 1f; // Время для плавного старта и остановки

    private Vector3 startPos;
    private float targetIntensity;
    private float currentIntensity;
    private float intensityVelocity;

 
    public void StartEffect()
    {
        targetIntensity = intensity * 2; // Увеличиваем интенсивность
    }

    public void StopEffect()
    {
        targetIntensity = intensity / 2; // Останавливаем тряску
    }

    void Start()
    {
        startPos = transform.localPosition;
        targetIntensity = 0f; // Изначально тряска выключена
    }

    void Update()
    {
        // Плавно меняем интенсивность
        currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityVelocity, transitionDuration);

        // Генерация тряски
        float shakeX = (Mathf.PerlinNoise(Time.time * speed, 0) - 0.5f) * currentIntensity;
        float shakeY = (Mathf.PerlinNoise(0, Time.time * speed) - 0.5f) * currentIntensity;

        transform.localPosition = startPos + new Vector3(shakeX, shakeY, 0);
    }

    
}