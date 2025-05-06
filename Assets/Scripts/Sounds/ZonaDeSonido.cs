using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaDeSonido : MonoBehaviour
{
    public AudioClip sonido;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sonido;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}

