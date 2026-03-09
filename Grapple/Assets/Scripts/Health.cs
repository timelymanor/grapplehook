using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")] [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] private float iFrames;
    [SerializeField] private float maxIFrames;
    [SerializeField] private bool hit;
    [SerializeField] private GameObject gameOverScreen;
    private float originalFixedDeltaTime;
    public bool isDead = false;

    void Start()
    {
        health = maxHealth;
        iFrames = 0;
        originalFixedDeltaTime = Time.fixedDeltaTime;
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
        StartCoroutine(SlowDownToPause());

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);

            // Unlock the cursor so the player can actually click 'Restart'
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    IEnumerator SlowDownToPause()
    {
        float startScale = Time.timeScale;
        float endScale = 0f;
        float timer = 0f;

        while (timer < 3)
        {
            // Interpolate the timeScale from the current value to 0
            Time.timeScale = Mathf.Lerp(startScale, endScale, timer / 3);

            // Adjust fixedDeltaTime to ensure smooth physics at different time scales
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;

            // Use unscaledDeltaTime for the timer to ensure it runs correctly while time is slowing down
            timer += Time.unscaledDeltaTime;
            yield return null; // Wait for the next frame
        }
    }

}
