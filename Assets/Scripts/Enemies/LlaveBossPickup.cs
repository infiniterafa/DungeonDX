using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveBossPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventorry inv = other.GetComponent<Inventorry>();
            if (inv != null)
            {
                inv.tieneLlaveBoss = true;
                Debug.Log("Recogiste la llave del Boss");
                Destroy(gameObject);
            }
        }
    }
}

