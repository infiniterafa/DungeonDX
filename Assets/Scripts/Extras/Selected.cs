using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    public float distancia = 3f;

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia))
        {
            if (hit.collider.tag == "Door")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    hit.collider.transform.GetComponent<SystemDoor>().ChangeDoorState();
                }
            }
        }
    }
}
