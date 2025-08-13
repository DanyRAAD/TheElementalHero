using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 10f;                
    public float damage = 7f;                
    public float lifetime = 5f;              

    private Transform target;


    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        
        Vector3 targetPosition = target.position + Vector3.up * 1.5f; 
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        if (target == null)
        {
           // Destroy(gameObject);
            return;
        }

        
        transform.position += transform.forward * speed * Time.deltaTime;

        
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy"))
        {
            
            Destroy(gameObject);
        }
    }
}
