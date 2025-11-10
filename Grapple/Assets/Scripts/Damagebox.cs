using Unity.VisualScripting;
using UnityEngine;

public class Damagebox : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    
    void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount, 10f);
        }
        
        
    }
    
}
