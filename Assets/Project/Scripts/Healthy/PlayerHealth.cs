using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxShield = 50f;
    public float currentShield;

    public HealthBar healthBar;
    public HealthBar shieldBar; 

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthBar.SetMaxHealth((int)maxHealth);
        shieldBar.SetMaxHealth((int)maxShield);
    }

    public void TakeDamage(float amount)
    {
        if (currentShield > 0)
        {
            float shieldDamage = Mathf.Min(amount, currentShield);
            currentShield -= shieldDamage;
            amount -= shieldDamage;
            shieldBar.SetHealth((int)currentShield);
        }

        if (amount > 0)
        {
            currentHealth -= amount;
            healthBar.SetHealth((int)currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HealHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.SetHealth((int)currentHealth);
    }

    public void HealShield(float amount)
    {
        currentShield = Mathf.Min(currentShield + amount, maxShield);
        shieldBar.SetHealth((int)currentShield);
    }
}
