using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.Audio;

public class SoundManager :MonoBehaviour,IEngineEffect
{
    public AudioClip engineSoundClip;      // Звук двигателя
    public AudioClip trackSoundClip;       // Звук двигателя с гусеницами
    public float engineSoundVolume=0.5f;      // Звук двигателя
    public float trackSoundVolume=0.3f;     // Звук двигателя с гусеницами
    public float fadeDuration = 2f;        // Длительность перехода
    private AudioSource audioSource;        // Один AudioSource

    private bool isEngineRunning = false;  // Статус работы двигателя

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Изначально выключаем звук
        audioSource.volume = 0f;
        audioSource.clip = engineSoundClip;  // Устанавливаем начальный клип
        audioSource.Play();                  // Начинаем проигрывание начального звука
    }

    // Функция для старта звука с гусеницами
    public void StartEffect()
    {
        if (!isEngineRunning)
        {
            isEngineRunning = true;
            StartCoroutine(FadeSound(engineSoundClip, trackSoundClip, fadeDuration, engineSoundVolume));  // Переключаем на звук с гусеницами
        }
    }

    // Функция для остановки звука с гусеницами
    public void StopEffect()
    {
        if (isEngineRunning)
        {
            isEngineRunning = false;
            StartCoroutine(FadeSound(trackSoundClip, engineSoundClip, fadeDuration, trackSoundVolume));  // Переключаем обратно на обычный звук
        }
    }

    // Короутина для плавного бленда звуков
    private IEnumerator FadeSound(AudioClip fromClip, AudioClip toClip, float duration, float targetVolume)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        // Устанавливаем новый клип
        audioSource.clip = toClip;
        audioSource.Play();  // Запускаем новый клип

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Плавно изменяем громкость
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, progress);

            yield return null;
        }

        // После завершения бленда, устанавливаем конечную громкость
        audioSource.volume = targetVolume;
    }
}
