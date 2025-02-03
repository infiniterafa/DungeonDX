using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public EnemyStatsSO stats;
    public Rigidbody rb;

    public NavMeshAgent agent;
    public Transform agentDestination;

    public float atkTimer;

    private void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (stats.hp > 0)
        {
            Movement();
        }
        else
            gameObject.SetActive(false);
    }

    private void Movement()
    {
        agent.destination = agentDestination.position;
    }

    public void TakeDamage(int _dmg)
    {
        stats.hp -= _dmg;
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
