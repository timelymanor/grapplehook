using UnityEngine;

public class RangedEnemy : EnemyBase
{

    [SerializeField] public float speed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int numOfProjectiles;
    [SerializeField] private float projectileSpread;
    private PlayerMovementAdvanced pma;
    private Rigidbody rb;
    
    private float spread;

    protected void Start()
    {
        pma = GameObject.Find("Player").GetComponent<PlayerMovementAdvanced>();
    }
    
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (!alreadyAttacked && agent.enabled)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            transform.LookAt(player.position);
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 spawnPos = transform.position + direction * 1f;
            if (!sightObstructed)
            {  
                
                agent.SetDestination(transform.position);
                Quaternion rotation = Quaternion.LookRotation(direction);
                for (int i = 0; i < numOfProjectiles; i++)
                {
                    Fire();
                }
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }
    }
    
    void Fire()
    {
        // Safely get the player's Rigidbody to calculate their velocity
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        Vector3 playerVelocity = playerRb != null ? playerRb.linearVelocity : Vector3.zero;
        
        // Calculate time to target for leading the shot
        float distance = Vector3.Distance(transform.position, player.position);
        float timeToTarget = distance / speed;
        
        // Calculate the random spread based on player speed
        spread = (pma.getPlayerSpeed() / 4f) + projectileSpread;
        float randomX = Random.Range(-spread, spread);
        float randomY = Random.Range(-spread, spread);

        // Calculate lead position and base direction
        Vector3 leadPosition = player.position + (playerVelocity * timeToTarget);
        Vector3 shootDirection = (leadPosition - transform.position).normalized;

        // Spawn the bullet, initially facing the perfect shoot direction
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.LookRotation(shootDirection));
        
        // Apply the random spread rotation BEFORE applying velocity
        bullet.transform.Rotate(randomX, randomY, 0);
        
        // Apply velocity along the bullet's NEW forward direction (which includes the spread)
        bullet.GetComponent<Rigidbody>().linearVelocity = bullet.transform.forward * speed;
    }
}
