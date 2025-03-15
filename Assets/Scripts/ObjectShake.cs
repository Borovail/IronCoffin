using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float intensity = 0.01f;
    public float speed = 10f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float shakeX = (Mathf.PerlinNoise(Time.time * speed, 0) - 0.5f) * intensity;
        float shakeY = (Mathf.PerlinNoise(0, Time.time * speed) - 0.5f) * intensity;

        transform.localPosition = startPos + new Vector3(shakeX, shakeY, 0);
    }
}