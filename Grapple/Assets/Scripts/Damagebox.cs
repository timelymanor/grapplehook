using Unity.VisualScripting;
using UnityEngine;

public class Damagebox : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    
    void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        health.TakeDamage(damageAmount);
    }
    
}
