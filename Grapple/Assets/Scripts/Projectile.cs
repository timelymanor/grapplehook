using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public enum ProjectileType
{
    normal,
    fire
}
public class Projectile : MonoBehaviour
{
    

    public float damageAmount;
    public float lifetime;
    [SerializeField] public ProjectileType projectileType;

    


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
