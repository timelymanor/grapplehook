using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] private float iFrames;
    [SerializeField] private float maxIFrames;
    [SerializeField] private bool hit;

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
        
        if (hit)
        {
            iFrames++;
        }
        else
        {
            iFrames--;
        }

        if (iFrames <= 0)
        {
            iFrames = 0;
        }

        if (iFrames >= maxIFrames)
        {
            iFrames = maxIFrames;
            hit = false;
        }
    }

    public void TakeDamage(float damage, float attackIFrames)
    {
        maxIFrames = attackIFrames;
        if (iFrames != 0) return;
        health -= damage;
        hit = true;
        

    }

    public void Death()
    {
        Destroy(gameObject);
    }
    
}
