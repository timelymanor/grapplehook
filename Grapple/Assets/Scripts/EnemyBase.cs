using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected NavMeshAgent agent;
    protected Transform player;
    [SerializeField] protected LayerMask whatIsGround, whatIsPlayer;
    [Header("AttackSettings")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected bool alreadyAttacked;
    [Header("Behaviors")]
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected float chaseSpeed;

    protected float chaseTime;
    [SerializeField] protected float maxChaseTime;
    protected bool sightObstructed;
    protected Vector3 walkPoint;
    protected bool walkPointSet;
    protected Transform hitPoint;
    [SerializeField] protected float walkPointRange;

    

    //States
    [SerializeField] protected float sightRange, attackRange;
    [SerializeField] protected bool playerInSightRange, playerInAttackRange;

    protected virtual void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if ((!playerInSightRange && !playerInAttackRange) || sightObstructed) Patroling();
        if (playerInSightRange && !playerInAttackRange && !sightObstructed) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && !sightObstructed) AttackPlayer();
        
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit, sightRange, whatIsGround))
        {
            if (chaseTime < 0)
            {
                sightObstructed = true;
                chaseTime = 0;
            }
        }
        else
        {
            sightObstructed = false;
        }
    }

    protected virtual void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        agent.speed = patrolSpeed;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    protected virtual void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    protected virtual void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    protected virtual void AttackPlayer()
    {
        transform.LookAt(player);
        
        
    }
    
    protected virtual void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
