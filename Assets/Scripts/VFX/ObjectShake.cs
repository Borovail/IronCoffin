using Assets.Scripts;
using UnityEngine;

public class ObjectShake : MonoBehaviour, IEngineEffect, IShootEffect,Iinteractable
{
    public float intensity = 0.01f; // ������� �������������
    public float speed = 10f; // �������� ������
    public float transitionDuration = 1f; // ����� ��� �������� ������ � ���������
    public float shootIntensity = 10; // ������������� ��� ��������
    public float shootDuration = 1f; // ������������ ������� ��� ��������

    private Vector3 startPos;
    private float targetIntensity;
    private float currentIntensity;
    private float intensityVelocity;

    // ��� ���������� ���������
    private bool isShooting = false;

    public void StartEffect()
    {
        targetIntensity = intensity * 2; // ����������� �������������
    }

    public void StopEffect()
    {
        targetIntensity = intensity / 2; // ������������� ������
    }

    public void ShootEffect()
    {
        StartCoroutine(ShakeBurst());
    }

    private System.Collections.IEnumerator ShakeBurst()
    {
        float originalIntensity = intensity;
        targetIntensity = intensity * shootIntensity; // ������ ������
        isShooting = true; // �������, ��� ������ ���������� �������
        yield return new WaitForSeconds(shootDuration); // ������ ������
        isShooting = false;
        targetIntensity = originalIntensity; // ������ ������������
    }

    void Start()
    {
        startPos = transform.localPosition;
        targetIntensity = 0f; // ���������� ������ ���������
    }

    void Update()
    {
        if (isShooting)
        {
            // ���������� Lerp ��� ������� ��������� ������������� ��� ��������
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * speed);
        }
        else
        {
            // ������ ������ ������������� � ������� SmoothDamp ��� ��������
            currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityVelocity, transitionDuration);
        }

        // ��������� ������
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
