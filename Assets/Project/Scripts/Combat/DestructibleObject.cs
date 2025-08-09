using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour
{
    public float health = 50f;
    public float damageFlashDuration = 0.5f; // tiempo que dura el parpadeo
    public Color damageColor = Color.red;

    private Material objectMaterial;
    private Color originalColor;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            objectMaterial = rend.material;  // importante: instancia material para que no afecte a otros objetos que usan el mismo material
            originalColor = objectMaterial.color;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            StartCoroutine(DamageEffectAndDestroy());
        }
        else
        {
            // Opcional: puedes hacer un parpadeo de daño también cuando aún no muere
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        if (objectMaterial == null) yield break;

        objectMaterial.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        objectMaterial.color = originalColor;
    }

    IEnumerator DamageEffectAndDestroy()
    {
        if (objectMaterial != null)
        {
            // Parpadea rojo rápido antes de destruir
            for (int i = 0; i < 3; i++)
            {
                objectMaterial.color = damageColor;
                yield return new WaitForSeconds(0.15f);
                objectMaterial.color = originalColor;
                yield return new WaitForSeconds(0.15f);
            }
        }
        Destroy(gameObject);
    }
}
