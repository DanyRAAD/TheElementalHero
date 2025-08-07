using UnityEngine;

public class RockProjectile : MonoBehaviour
{
    public float speed = 10f;                // Velocidad de la roca
    public float damage = 7f;                // Daño que hará
    public float lifetime = 5f;              // Tiempo para auto-destruirse si no impacta

    private Transform target;


    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        // Apunta inmediatamente hacia la cabeza
        Vector3 targetPosition = target.position + Vector3.up * 1.5f; // Ajusta según la altura de la cabeza
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

        // Mover hacia adelante
        transform.position += transform.forward * speed * Time.deltaTime;

        // Opcional: destruir después de un tiempo
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
            // Destruir roca al chocar contra otros objetos que no sean enemigos (suelo, paredes)
            Destroy(gameObject);
        }
    }
}
