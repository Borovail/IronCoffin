using Assets.Scripts;
using UnityEngine;

public class ObjectShake : MonoBehaviour, IEngineEffect, IShootEffect,Iinteractable
{
    public float intensity = 0.01f; // Базовая интенсивность
    public float speed = 10f; // Скорость тряски
    public float transitionDuration = 1f; // Время для плавного старта и остановки
    public float shootIntensity = 10; // Интенсивность при выстреле
    public float shootDuration = 1f; // Длительность эффекта при выстреле

    private Vector3 startPos;
    private float targetIntensity;
    private float currentIntensity;
    private float intensityVelocity;

    // Для управления эффектами
    private bool isShooting = false;

    public void StartEffect()
    {
        targetIntensity = intensity * 2; // Увеличиваем интенсивность
    }

    public void StopEffect()
    {
        targetIntensity = intensity / 2; // Останавливаем тряску
    }

    public void ShootEffect()
    {
        StartCoroutine(ShakeBurst());
    }

    private System.Collections.IEnumerator ShakeBurst()
    {
        float originalIntensity = intensity;
        targetIntensity = intensity * shootIntensity; // Резкий толчок
        isShooting = true; // Отметим, что сейчас происходит выстрел
        yield return new WaitForSeconds(shootDuration); // Держим тряску
        isShooting = false;
        targetIntensity = originalIntensity; // Плавно возвращаемся
    }

    void Start()
    {
        startPos = transform.localPosition;
        targetIntensity = 0f; // Изначально тряска выключена
    }

    void Update()
    {
        if (isShooting)
        {
            // Используем Lerp для резкого изменения интенсивности при выстреле
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * speed);
        }
        else
        {
            // Плавно меняем интенсивность с помощью SmoothDamp при возврате
            currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityVelocity, transitionDuration);
        }

        // Генерация тряски
        float shakeX = (Mathf.PerlinNoise(Time.time * speed, 0) - 0.5f) * currentIntensity;
        float shakeY = (Mathf.PerlinNoise(0, Time.time * speed) - 0.5f) * currentIntensity;

        transform.localPosition = startPos + new Vector3(shakeX, shakeY, 0);
    }

    public void OnInteract()
    {
    }

    public void OnRelease()
    {
    }
}
