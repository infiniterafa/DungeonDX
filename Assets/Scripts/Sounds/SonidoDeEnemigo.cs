using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoEnemigo : MonoBehaviour
{
    public AudioClip sonidoAtaque;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sonidoAtaque;
    }

    public void ReproducirSonido()
    {
        audioSource.Play();
    }
}

