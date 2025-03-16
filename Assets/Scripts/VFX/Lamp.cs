using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class Lamp : MonoBehaviour, IEngineEffect
{
    public float swingSpeed = 1.5f;
    public float swingAmount = 15f;
    public float swingRandomness = 5f;

    public int flickerCount = 10;
    public float minFlickerTime = 0.5f;
    public float maxFlickerTime = 0.15f;
    public float flickerChance = 0.01f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;

    public float maxSwingAngle = 20f; // ����������� ������������� ���� �������
    public float maxSwingSpeed = 2.5f; // ����������� �������� ������������
    public float transitionDuration = 1f; // ����� ��� �������� ������ � ���������

    private Color initialColor;
    private Light lamp;
    private float flickerTimer;
    private float baseSwingSpeed;
    private float baseSwingAmount;
    private float randomSwingFactor;
    private float swingTimer;
    private bool isFlickering;

    // ��� �������� ������ � ���������
    private float currentSwingSpeed;
    private float currentSwingAmount;
    private float swingSpeedVelocity;
    private float swingAmountVelocity;
    private float baseSwingRandomness;

    private void Start()
    {
        lamp = GetComponentInChildren<Light>();
        initialColor = lamp.color;
        minIntensity += lamp.intensity;
        maxIntensity += lamp.intensity;
        ResetFlickerTimer();
    }

    private void Update()
    {
        // ��������� ���������� ������� ������� � ������� ������
        swingTimer = Time.deltaTime * 0.5f;
        randomSwingFactor = Mathf.Lerp(randomSwingFactor, Mathf.PerlinNoise(swingTimer, 0) * baseSwingRandomness, Time.deltaTime*2);

        // ������� ��������� ��������� � �������� ������������
        currentSwingSpeed = Mathf.SmoothDamp(currentSwingSpeed, Mathf.Clamp(baseSwingSpeed + randomSwingFactor, 0.5f, maxSwingSpeed), ref swingSpeedVelocity, transitionDuration);
        currentSwingAmount = Mathf.SmoothDamp(currentSwingAmount, Mathf.Clamp(baseSwingAmount + randomSwingFactor, 5f, maxSwingAngle), ref swingAmountVelocity, transitionDuration);

        // ������������ ������������ ��������� � �������� ������������
        float angle = Mathf.Sin(Time.time * currentSwingSpeed) * currentSwingAmount;
        transform.localRotation = Quaternion.Euler(angle, 0, 0);

        // ���� ����� �������� ��������
        lamp.color = Color.Lerp(initialColor, Color.white, Mathf.PerlinNoise(Time.time, 0));

        // ������ ����� � ������� ���������� �������������
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0)
        {
            if (Random.value < flickerChance && isFlickering)
            {
                StartCoroutine(FlickerEffect());
            }
            ResetFlickerTimer();
        }
    }

    private IEnumerator FlickerEffect()
    {
        isFlickering = true;
        int flickerCountTemp = Random.Range(-5, 5) + flickerCount;
        float baseIntensity = lamp.intensity;

        for (int i = 0; i < flickerCountTemp; i++)
        {
            lamp.intensity = (i % 2 == 0) ? 0 : Random.Range(minIntensity, maxIntensity); // �������� ����� 0 � ���������� ������
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f)); // ������� ���������
        }

        lamp.intensity = baseIntensity; // ������� � �������� ���������
        isFlickering = false;
    }

    private void ResetFlickerTimer()
    {
        flickerTimer = Random.Range(minFlickerTime, maxFlickerTime);
    }

    // ������� ��� ������ �������
    public void StartEffect()
    {
        baseSwingSpeed = swingSpeed;
        baseSwingAmount = swingAmount;
        baseSwingRandomness = swingRandomness;
    }

    // ������� ��� ��������� �������
    public void StopEffect()
    {
        baseSwingSpeed = 0f;
        baseSwingAmount = 0f;
        baseSwingRandomness = 0;
    }
}
