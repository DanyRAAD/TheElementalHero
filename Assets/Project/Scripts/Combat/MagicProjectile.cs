using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 25f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Desaparece tras X segundos si no choca
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Evita dañar al jugador o a sí mismo
        if (other.CompareTag("Player")) return;

        // Intentar hacer daño
        EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null && enemyHealth.enemyType == EnemyType.Golem)
        {
            enemyHealth.TakeDamage(damage, false); // false = ataque mágico
        }

        // Destruir siempre que colisione con algo
        Destroy(gameObject);
    }
}
