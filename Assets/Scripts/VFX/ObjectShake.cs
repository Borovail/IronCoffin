using Assets.Scripts;
using UnityEngine;

public class ObjectShake : MonoBehaviour,IEngineEffect
{
    public float intensity = 0.01f; // ������� �������������
    public float speed = 10f; // �������� ������
    public float transitionDuration = 1f; // ����� ��� �������� ������ � ���������

    private Vector3 startPos;
    private float targetIntensity;
    private float currentIntensity;
    private float intensityVelocity;

 
    public void StartEffect()
    {
        targetIntensity = intensity * 2; // ����������� �������������
    }

    public void StopEffect()
    {
        targetIntensity = intensity / 2; // ������������� ������
    }

    void Start()
    {
        startPos = transform.localPosition;
        targetIntensity = 0f; // ���������� ������ ���������
    }

    void Update()
    {
        // ������ ������ �������������
        currentIntensity = Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityVelocity, transitionDuration);

        // ��������� ������
        float shakeX = (Mathf.PerlinNoise(Time.time * speed, 0) - 0.5f) * currentIntensity;
        float shakeY = (Mathf.PerlinNoise(0, Time.time * speed) - 0.5f) * currentIntensity;

        transform.localPosition = startPos + new Vector3(shakeX, shakeY, 0);
    }

    
}