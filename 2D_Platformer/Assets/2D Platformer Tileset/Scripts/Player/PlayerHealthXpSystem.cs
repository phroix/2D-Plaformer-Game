using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthXpSystem : MonoBehaviour
{
    [Header("Player Health")]
    public int maxHealth = 120;
    public int currentHealth;

    public HealthBar healthBar;
    public Text healthText;

    [Header("Player Energy")]
    public int maxEnergy = 60;
    public int currentEnergy;

    public EnergyBar energyBar;
    public Text energyText;

    [Header("Player XP")]
    public int maxLevel = 8;
    public int maxXP = 2800;
    public int currentXP;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        energyText.text = currentEnergy + "/" + maxEnergy;

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            IncreaseEnergy(10);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void IncreaseEnergy(int energy)
    {
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);
    }
}
