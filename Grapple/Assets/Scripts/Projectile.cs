using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float projectileSpread;
    [SerializeField] private float damageAmount;
    private Rigidbody rb;
    private Transform player;

    
    public enum ProjectileType
    {
        normal,
        fire
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Despawn();
    }
    

    private void Despawn()
    {
        Destroy(gameObject, 15f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
        Destroy(gameObject);
    }
}
