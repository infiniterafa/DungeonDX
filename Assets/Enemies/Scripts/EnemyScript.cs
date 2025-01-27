using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyStatsSO stats;
    public Rigidbody rb;

    public float atkTimer;

    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        rb.transform.position += transform.forward * stats.speed * Time.deltaTime;
    }

    private void Attack(Player _player)
    {
        //Attack at certain frequency (needs change of logic)
        //Currently attacks UNTIL timer reaches desired frequency by mantaining contact with player

        atkTimer += Time.deltaTime;
        if (atkTimer >= stats.attackFrequency)
        {
            _player.TakeDamage(stats.attackPower);
            atkTimer = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            Attack(collision.transform.gameObject.GetComponent<Player>());
    }
}
