using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float iFrames;
    [SerializeField] private float maxIFrames;

    void Start()
    {
        health = maxHealth;
        iFrames = 0;
    }

    void Update()
    {
        if (health <= 0)
        {
            
        }

        iFrames--;
    }

    public void TakeDamage(float damage)
    {
        if (iFrames > 0)
        {
            health -= damage;
            iFrames = maxIFrames;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
