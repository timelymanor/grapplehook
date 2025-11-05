using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float projectileSpread;
    private Rigidbody rb;
    private Transform player;
    private bool collided;

    
    public enum ProjectileType
    {
        normal,
        fire
    }

    void Start()
    {
        collided = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 randomOffset = Random.insideUnitSphere * projectileSpread;
        Vector3 finalDirection = (direction + randomOffset).normalized;
        transform.rotation = Quaternion.LookRotation(finalDirection);
        rb.linearVelocity = transform.forward * speed;
        Despawn();
    }
    

    private void Despawn()
    {
        Destroy(gameObject, 15f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
