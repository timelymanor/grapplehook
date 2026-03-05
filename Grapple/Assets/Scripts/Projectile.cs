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
        projectileSpread = pma.getPlayerSpeed() / 2;
        rb = GetComponent<Rigidbody>();
        float randomX = Random.Range(-projectileSpread, projectileSpread);
        float randomY = Random.Range(-projectileSpread, projectileSpread);
        transform.Rotate(randomX, randomY, 0);
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
            health.TakeDamage(damageAmount, 3f);
        }
        if (other.tag != "Enemy" || other.tag != "Projectile")
            Destroy(gameObject);
    }
}
