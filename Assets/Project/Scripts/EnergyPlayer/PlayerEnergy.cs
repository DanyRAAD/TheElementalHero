using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float regenRate = 10f;

    public HealthBar energyBar;

    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxHealth((int)maxEnergy);
    }

    void Update()
    {
        
        RegenerateEnergy();
    }

    void RegenerateEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += regenRate * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
            energyBar.SetHealth((int)currentEnergy);
        }
    }

    public bool UseEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            energyBar.SetHealth((int)currentEnergy);
            return true;
        }
        return false;
    }

    public void RechargeEnergy(float amount)
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        energyBar.SetHealth((int)currentEnergy);
    }
}
