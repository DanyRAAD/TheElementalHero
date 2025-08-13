using UnityEngine;
using System.Collections;

public class DestructibleObject : MonoBehaviour
{
    public float health = 50f;
    public float damageFlashDuration = 0.5f; 
    public Color damageColor = Color.red;

    private Material objectMaterial;
    private Color originalColor;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            objectMaterial = rend.material;  
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
