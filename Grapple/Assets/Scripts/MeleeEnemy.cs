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
        if(Physics.SphereCast(attackPoint.position, hitRange, transform.forward,  out RaycastHit hit, 1f, LayerMask.NameToLayer("Player")))
        {
            hit.collider.gameObject.GetComponentInParent<Health>().TakeDamage(6);}
    }
}
