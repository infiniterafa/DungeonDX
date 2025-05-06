using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeteccionJugador : MonoBehaviour
{
    private AudioSource audioSource;
    public float tiempoEspera = 5f; // Tiempo entre sonidos
    private float tiempoUltimoSonido = -Mathf.Infinity;

    private void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= tiempoUltimoSonido + tiempoEspera)
        {
            audioSource?.Play();
            tiempoUltimoSonido = Time.time;
        }
    }
}
