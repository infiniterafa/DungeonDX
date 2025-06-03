using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAudioManager : MonoBehaviour
{
    public static RoomAudioManager Instance;
    private AudioSource audioSource;

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f;

    private Coroutine currentFadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.3f; 

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReproducirSonidoCuarto(AudioClip nuevoClip, float volumen)
    {
        if (audioSource.clip == nuevoClip)
            return;

        if (currentFadeCoroutine != null)
            StopCoroutine(currentFadeCoroutine);

        currentFadeCoroutine = StartCoroutine(FadeToClip(nuevoClip, volumen));
    }

    private IEnumerator FadeToClip(AudioClip nuevoClip, float targetVolume)
    {
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();

        audioSource.clip = nuevoClip;
        audioSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
    private IEnumerator FadeToClip(AudioClip nuevoClip)
    {
        yield return FadeToClip(nuevoClip, audioSource.volume);
    }

}