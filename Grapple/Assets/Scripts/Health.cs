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
    [SerializeField] private GameObject gameOverScreen;
    public bool isDead = false;

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
        if (isDead) return;
        isDead = true;
        if (gameObject.CompareTag("Player"))
        {
            
            GetComponent<Sliding>().enabled = false;
            GetComponent<PlayerMovementAdvanced>().enabled = false;
            GetComponent<WallRunning>().enabled = false;
            GetComponent<Grappling>().enabled = false;
            GetComponent<SwingingDone>().enabled = false;

            
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.freezeRotation = false; 
            rb.useGravity = true;

            
            rb.AddForce(Vector3.forward * 2f, ForceMode.Impulse); 

            
            ShowGameOver();
        }
        else 
        {
            Destroy(gameObject);
        }


        
    }
    private void ShowGameOver()
    {
        while (Time.timeScale > 0f)
        {
            Time.timeScale -= 0.1f;
            Debug.Log(Time.timeScale);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            
            // Unlock the cursor so the player can actually click 'Restart'
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
