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
    private Animator animator;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;

        healthBar.SetMaxHealth((int)maxHealth);
        shieldBar.SetMaxHealth((int)maxShield);

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

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
        if (isDead) return;
        isDead = true;

        animator.SetTrigger("IsDead");
        StartCoroutine(RespawnAfterDelay(3f));
    }

    private System.Collections.IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Obtener posición del último checkpoint
        Vector3 respawnPos = CheckpointManager.instance.GetLastCheckpointPosition();

        if (respawnPos != Vector3.zero)
        {
            transform.position = respawnPos;
            ResetPlayer();
        }
        else
        {
            // No hay checkpoint, reinicia la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ResetPlayer()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        healthBar.SetHealth((int)currentHealth);
        shieldBar.SetHealth((int)currentShield);

        // Aquí puedes reiniciar animaciones o estados que necesites
        animator.ResetTrigger("IsDead");
        isDead = false;
    }

    public void HealHealth(float amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.SetHealth((int)currentHealth);
    }

    public void HealShield(float amount)
    {
        if (isDead) return;

        currentShield = Mathf.Min(currentShield + amount, maxShield);
        shieldBar.SetHealth((int)currentShield);
    }
}
