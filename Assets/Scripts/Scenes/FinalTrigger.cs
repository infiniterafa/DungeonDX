using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entraste al cuarto final. Cargando escena de victoria...");
            SceneManager.LoadScene("SceneVictory");
        }
    }
}
