using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SystemDoor : MonoBehaviour
{
    public bool doorOpen = false;
    public float doorOpenAngle = 95f;
    public float doorCloseAngle = 0.0f;
    public float smooth = 3.0f;

    public AudioClip openDoor;
    public AudioClip closeDoor;

    private GameObject jugador;
    private bool enRango = false;

    [SerializeField] private SystemMessageUI mensajeUI;

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }

    void Update()
    {
        if (enRango && jugador != null && Input.GetKeyDown(KeyCode.F))
        {
            Inventorry inv = jugador.GetComponent<Inventorry>();
            Debug.Log("¿Presionaste F? Sí");
            Debug.Log("¿Tiene inventario? " + (inv != null));
            Debug.Log("¿Tiene llave del Boss? " + (inv?.tieneLlaveBoss));

            if (inv != null && inv.tieneLlaveBoss)
            {
                Debug.Log("Llave del boss detectada. Abrimos puerta.");
                ChangeDoorState();
                inv.tieneLlaveBoss = false;
            }
            else
            {
                Debug.Log("No tienes la llave correcta. Puerta NO se abre.");
            }
        }

        Quaternion targetRotation = Quaternion.Euler(0, doorOpen ? doorOpenAngle : doorCloseAngle, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.gameObject;
            enRango = true;
        }

        if (other.CompareTag("TriggerDoor"))
        {
            AudioSource.PlayClipAtPoint(closeDoor, transform.position, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = null;
            enRango = false;
        }

        if (other.CompareTag("TriggerDoor"))
        {
            AudioSource.PlayClipAtPoint(openDoor, transform.position, 1f);
        }
    }
}
