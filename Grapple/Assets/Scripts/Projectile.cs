using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
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
        rb.linearVelocity = direction * speed;
        Despawn();
    }
    

    private void Despawn()
    {
        Destroy(gameObject, 15f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hi");
        Destroy(gameObject);
    }

    void OnCollisionStay(Collision collision)
    {
        collided = true;
    }
}
