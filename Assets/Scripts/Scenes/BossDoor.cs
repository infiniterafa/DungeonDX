using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private bool enRango = false;
    private GameObject jugador;

    private void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.F))
        {
            Inventorry inv = jugador.GetComponent<Inventorry>();
            if (inv != null && inv.tieneLlaveBoss)
            {
                AbrirPuerta();
            }
            else
            {
                Debug.Log("No tienes la llave del Boss");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.gameObject;
            enRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = null;
            enRango = false;
        }
    }

    private void AbrirPuerta()
    {
        Debug.Log("¡Puerta del Boss abierta!");
        gameObject.SetActive(false);
    }
}