using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mensajeTMP;

    public void MostrarMensaje(string mensaje, float tiempo = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarTemporal(mensaje, tiempo));
    }

    private IEnumerator MostrarTemporal(string mensaje, float tiempo)
    {
        mensajeTMP.text = mensaje;
        mensajeTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(tiempo);
        mensajeTMP.gameObject.SetActive(false);
    }

    [SerializeField] private TextMeshProUGUI mensajeExtraTMP;

    public void MostrarMensajeExtra(string mensaje, float tiempo = 2f)
    {
        StartCoroutine(MostrarExtraTemporal(mensaje, tiempo));
    }

    private IEnumerator MostrarExtraTemporal(string mensaje, float tiempo)
    {
        if (mensajeExtraTMP == null) yield break;

        mensajeExtraTMP.text = mensaje;
        mensajeExtraTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(tiempo);
        mensajeExtraTMP.gameObject.SetActive(false);
    }
}