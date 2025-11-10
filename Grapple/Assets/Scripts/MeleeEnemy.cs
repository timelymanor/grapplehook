using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private Animator am;

    protected override void Start()
    {
        base.Start();
        
        am = GetComponent<Animator>();
    }

    protected override void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        base.AttackPlayer();
        if (!alreadyAttacked)
        {
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            am.SetTrigger("Attack");
            MeleeAttack();
        }   
        
        
    }

    public void MeleeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, hitRange, 1 << LayerMask.NameToLayer("Player"));
        foreach (Collider hit in hits)
        {
            Health health = hit.GetComponentInParent<Health>();
            if (health != null)
            {
                health.TakeDamage(10f, 30f);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, hitRange);
    }
}
