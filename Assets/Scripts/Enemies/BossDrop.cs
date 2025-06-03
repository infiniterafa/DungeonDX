using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrop : MonoBehaviour
{
    [SerializeField] private GameObject rupiaBoss;
    [SerializeField] private EnemyScript enemyScript;

    private bool dropped = false;

    private void OnDisable()
    {
        if (!dropped && enemyScript.hp <= 0)
        {
            dropped = true;

            if (rupiaBoss != null)
            {
                Instantiate(rupiaBoss, transform.position + Vector3.up, Quaternion.identity);
                Debug.Log("Gema dropeada por el Boss");
            }
        }
    }
}