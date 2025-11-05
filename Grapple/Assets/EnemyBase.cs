using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected NavMeshAgent agent;
    protected Transform player;
    [Header("AttackSettings")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected bool alreadyAttacked;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    protected virtual void AttackPlayer()
    {
    }
    
    protected virtual void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
