using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacks : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    private Transform player;
    [Header("AttackSettings")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;
    [SerializeField] private GameObject projectile;

    [SerializeField] public enum EnemyType
    {
        melee,
        ranged,
        flyer
    }

    [SerializeField] public EnemyType enemyType;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    public void AttackPlayer()
    {
        //Make sure enemy doesn't move
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if(enemyType == EnemyType.ranged)
                RangedAttack();
            else if (enemyType == EnemyType.melee)
                MeleeAttack();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void MeleeAttack()
    {
        agent.SetDestination(transform.position);
    }

    private void RangedAttack()
    {
        transform.LookAt(player.position);
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 spawnPos = transform.position + direction * 1f;
        agent.SetDestination(transform.position);
        Quaternion rotation = Quaternion.LookRotation(direction);
        Rigidbody rb = Instantiate(projectile, spawnPos, rotation)
            .GetComponent<Rigidbody>();
    }
}
