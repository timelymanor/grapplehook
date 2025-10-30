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

    public void MeleeAttack()
    {
        agent.SetDestination(transform.position);
    }

    public void RangedAttack()
    {
        transform.LookAt(player.position);
        Vector3 targetPosition = player.position;
        Vector3 spawnPos = transform.position + targetPosition * 1f;
        agent.SetDestination(transform.position);
        Rigidbody rb = Instantiate(projectile, spawnPos, Quaternion.LookRotation(targetPosition))
            .GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
    }
}
