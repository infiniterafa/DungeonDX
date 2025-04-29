using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemDoor : MonoBehaviour
{
    public bool doorOpen = false; //Verificar si la puerta esta cerrada o abierta.
    public float doorOpenAngle = 95f; //Angulo de la puerta al estar abierta.
    public float doorCloseAngle = 0.0f; //Angulo de la puerta al estar cerrada.
    public float smooth = 3.0f; // Velocidad con la que rotara la puerta.

    public AudioClip openDoor;
    public AudioClip closeDoor;
    
    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
    }

    void Update()
    {
        if (doorOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(closeDoor, transform.position, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TriggerDoor")
        {
            AudioSource.PlayClipAtPoint(openDoor, transform.position, 1);
        }
    }
}