using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //public Collider swordCollider;
    public List<Transform> positionList;
    public int damage = 5;

    public int bossdamage = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            if (other.transform.gameObject.TryGetComponent(out EnemyScript enemy))

            other.transform.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);

            else
            {
                other.transform.gameObject.GetComponent<GolemScript>().TakeDamage(bossdamage);
            }
               

        }
    }

    public void SetPosition(int index)
    {
        transform.localPosition = positionList[index].localPosition;
        transform.localScale = positionList[index].localScale;
    }
}
