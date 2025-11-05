using UnityEngine;

public class RangedEnemy : EnemyBase
{

    
    [SerializeField] private GameObject projectile;
    [SerializeField] private float numOfProjectiles;
    
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            transform.LookAt(player.position);
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 spawnPos = transform.position + direction * 1f;
            agent.SetDestination(transform.position);
            Quaternion rotation = Quaternion.LookRotation(direction);
            for (int i = 0; i < numOfProjectiles; i++)
            {
                Rigidbody rb = Instantiate(projectile, spawnPos, rotation).GetComponent<Rigidbody>();
            }
        }
    }
}
