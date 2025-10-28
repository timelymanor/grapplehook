using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
