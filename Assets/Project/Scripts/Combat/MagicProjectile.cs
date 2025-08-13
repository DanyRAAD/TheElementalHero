using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 25f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player")) return;

        
        EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null && enemyHealth.enemyType == EnemyType.Golem)
        {
            enemyHealth.TakeDamage(damage, false); 
        }

        
        DestructibleObject destructible = other.GetComponent<DestructibleObject>();
        if (destructible != null)
        {
            destructible.TakeDamage(damage);
        }

        
        Destroy(gameObject);
    }
}
