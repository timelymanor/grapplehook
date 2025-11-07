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
            Death();
        }

        iFrames--;
        if (iFrames <= 0)
        {
            iFrames = 0;
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
