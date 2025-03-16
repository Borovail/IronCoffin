using UnityEngine;
using System.Collections;
using Assets.Scripts;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>, IEngineEffect, IShootEffect
{
    public float engineSoundVolume = 0.5f;
    public float trackSoundVolume = 0.3f;
    public float fadeDuration = 2f;

    public float deafDuration = 3f; // Время оглушения
    public float minCutoff = 500f; // Насколько глухой звук
    public float maxCutoff = 22000f; // Обычное звучание

    public AudioClip ShootSound;
    public AudioClip DeathSound;
    public AudioClip RingingSound;

    ///without AudioMixer
    public AudioSource FinalMusicAudioSource;

    public AudioSource engineSoundSource;
    public AudioSource TrackSoundSource;
    public AudioMixer audioMixer;

    private AudioSource soundSource;
    private bool isEngineRunning = false;

    void Start()
    {
        // Добавляем два аудио-сорса
        // Настраиваем оба сорса
        ConfigureAudioSource(engineSoundSource);
        ConfigureAudioSource(TrackSoundSource);
        ConfigureAudioSource(FinalMusicAudioSource, false, 0.2f);

        soundSource = GetComponent<AudioSource>();
    }

    private void ConfigureAudioSource(AudioSource source, bool loop = true, float volume = 0f)
    {
        source.volume = 0f;  // Начинаем с нуля
        source.loop = loop;
        source.playOnAwake = false;
    }

    public void PlaySound(AudioClip clip, float volume, AudioSource source = null)
    {
        if (source == null) source = soundSource;
        source.volume = volume;
        source.PlayOneShot(clip);
    }

    public void ShootEffect()
    {
        PlaySound(ShootSound, 1f);
        StartCoroutine(ApplyShootEffect());
    }

    public IEnumerator ApplyDeafEffect()
    {
        float startCutoff;
        audioMixer.GetFloat("LowPassCutoff", out startCutoff);  // Получаем текущую частоту среза перед началом эффекта

        // Плавно снижаем cutoff до минимального значения
        float elapsed = 0f;
        while (elapsed < 0.5f) // Можно сделать небольшой промежуток времени для плавного эффекта
        {
            elapsed += Time.deltaTime;
            float newCutoff = Mathf.Lerp(startCutoff, minCutoff, elapsed / 0.25f);  // Линейная интерполяция
            audioMixer.SetFloat("LowPassCutoff", newCutoff);
            yield return null;
        }

        audioMixer.SetFloat("LowPassCutoff", minCutoff); // Устанавливаем точно минимальное значение
    }

    public IEnumerator StartFinalMusic()
    {
        StartCoroutine(FadeSound(engineSoundSource, FinalMusicAudioSource, 0f, 1f));
        yield return new WaitForSecondsRealtime(FinalMusicAudioSource.clip.length);
        PlaySound(DeathSound, 1f, FinalMusicAudioSource);
    }

    private IEnumerator ApplyShootEffect()
    {
        StartCoroutine(ApplyDeafEffect());

        yield return new WaitForSeconds(1f); // Держим эффект на минимальной частоте среза
        PlaySound(RingingSound, 1f);

        // Плавно восстанавливаем cutoff до исходного значения
        float elapsed = 0f;
        while (elapsed < deafDuration)
        {
            elapsed += Time.deltaTime;
            float newCutoff = Mathf.Lerp(minCutoff, maxCutoff, elapsed / deafDuration);  // Линейная интерполяция
            audioMixer.SetFloat("LowPassCutoff", newCutoff);
            yield return null;
        }

        audioMixer.SetFloat("LowPassCutoff", maxCutoff); // Возвращаем нормальный звук
    }

    public void StartEffect()
    {
        if (!isEngineRunning)
        {
            isEngineRunning = true;
            StartCoroutine(FadeSound(engineSoundSource, TrackSoundSource, engineSoundVolume, trackSoundVolume));
        }
    }

    public void StopEffect()
    {
        if (isEngineRunning)
        {
            isEngineRunning = false;
            StartCoroutine(FadeSound(TrackSoundSource, engineSoundSource, trackSoundVolume, engineSoundVolume));
        }
    }

    private IEnumerator FadeSound(AudioSource fadeOutSource, AudioSource fadeInSource, float fadeOutMaxVolume, float fadeInMaxVolume)
    {
        if (!fadeInSource.isPlaying)
            fadeInSource.Play();

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float progress = elapsedTime / fadeDuration;

            fadeOutSource.volume = Mathf.Lerp(fadeOutMaxVolume, 0f, progress);
            fadeInSource.volume = Mathf.Lerp(0f, fadeInMaxVolume, progress);

            yield return null;
        }

        fadeOutSource.volume = 0f;
        fadeOutSource.Stop();  // Останавливаем звук после заглушения
        fadeInSource.volume = fadeInMaxVolume;
    }

}
