using UnityEngine;

public class ZonaDeSonido : MonoBehaviour
{
    public AudioClip sonido;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoomAudioManager.Instance.ReproducirSonidoCuarto(sonido, 0.2f);
        }
    }
}
