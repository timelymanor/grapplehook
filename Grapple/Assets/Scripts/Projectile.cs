using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    

    [SerializeField] private float damageAmount;
    

    
    public enum ProjectileType
    {
        normal,
        fire
    }

    void Start()
    {
       
        

        Despawn();
    }
    

    private void Despawn()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount, 3f);
        }
        if (!other.CompareTag("Enemy") && !other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
