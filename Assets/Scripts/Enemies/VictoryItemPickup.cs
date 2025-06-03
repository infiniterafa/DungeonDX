using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VictoryItemPickup : MonoBehaviour
{
    private GameObject muroADesaparecer;

    private void Start()
    {
        muroADesaparecer = GameObject.Find("PuertaFinal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameFlags.tieneGemaBoss = true;
            Debug.Log("Recogiste la gema del Boss");

            if (muroADesaparecer != null)
            {
                muroADesaparecer.SetActive(false);
            }

            Destroy(gameObject);
        }
    }
}