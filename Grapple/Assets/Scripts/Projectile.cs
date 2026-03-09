using System;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float projectileSpread;
    private float spread;
    [SerializeField] private float damageAmount;
    private Rigidbody rb;
    private Transform player;
    private PlayerMovementAdvanced pma;

    
    public enum ProjectileType
    {
        normal,
        fire
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pma = player.GetComponent<PlayerMovementAdvanced>();
        spread = pma.getPlayerSpeed() / 4 + projectileSpread;
        rb = GetComponent<Rigidbody>();
        float randomX = Random.Range(-spread, spread);
        float randomY = Random.Range(-spread, spread);
        transform.Rotate(randomX, randomY, 0);
        rb.linearVelocity = transform.forward * speed;

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
