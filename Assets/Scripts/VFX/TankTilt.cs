using UnityEngine;
using System.Collections;

public class TankTilt : MonoBehaviour,IEngineEffect
{
    public float tiltAngle = 5f; // Угол наклона в градусах
    public float tiltDuration = 1f; // Время наклона

    private Quaternion originalRotation;
    private bool isTilting = false;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    public void StartEffect()
    {
        if (!isTilting)
        {
            StartCoroutine(TiltTank(-tiltAngle)); // Наклон назад при старте
        }
    }

    public void StopEffect()
    {
        if (!isTilting)
        {
            StartCoroutine(TiltTank(tiltAngle)); // Наклон вперёд при остановке
        }
    }

    private IEnumerator TiltTank(float targetAngle)
    {
        isTilting = true;
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(targetAngle, 0f, 0f);

        while (elapsedTime < tiltDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / tiltDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, progress);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // Небольшая задержка перед возвратом

        elapsedTime = 0f;
        while (elapsedTime < tiltDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / tiltDuration;
            transform.rotation = Quaternion.Lerp(targetRotation, originalRotation, progress);
            yield return null;
        }

        isTilting = false;
    }
}