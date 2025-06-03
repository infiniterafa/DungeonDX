using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMensajes : MonoBehaviour
{
    [SerializeField] private SystemMessageUI mensajeUI;
    [SerializeField] private SystemMessageUI mensajeGemasUI;
    [SerializeField] private Inventorry inventorry;

    private bool mostroMensajeInicial = false;
    private bool mostroMensajeLlave = false;

    void Start()
    {
        if (mensajeUI != null)
        {
            mensajeUI.MostrarMensaje("Busca la llave que abre el cofre, para la puerta ", 10f);
            mostroMensajeInicial = true;
        }

        StartCoroutine(MostrarMensajeGemitas());
    }

    void Update()
    {
        if (mostroMensajeInicial && !mostroMensajeLlave && inventorry != null && inventorry.tieneLlaveBoss)
        {
            mensajeUI.MostrarMensaje("¡Recogiste la llave del Boss!, Ahora presiona f justo en la PERILLA de la puerta", 10f);
            mostroMensajeLlave = true;
        }
    }

    public void MostrarInstruccionPuerta()
    {
        if (!mostroMensajeLlave && mensajeUI != null)
        {
            mensajeUI.MostrarMensaje("¡Recogiste la llave del Boss!, Ahora presiona f justo en la PERILLA de la puerta", 10f);
            mostroMensajeLlave = true;
        }
    }

    private IEnumerator MostrarMensajeGemitas()
    {
        yield return new WaitForSeconds(0.1f);

        if (mensajeGemasUI != null)
        {
            mensajeGemasUI.MostrarMensajeExtra("Busca las 3 gemas, para obtener el diamante", 10f);
        }
    }
}