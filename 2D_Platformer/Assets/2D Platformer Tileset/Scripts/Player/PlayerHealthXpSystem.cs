using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthXpSystem : MonoBehaviour
{
    [Header("Player Health")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Text healthText;

    [Header("Player Energy")]
    public int maxEnergy = 50;
    public int currentEnergy;

    public EnergyBar energyBar;
    public Text energyText;

    [Header("Player XP")]
    public int maxLevel = 8;
    public int currentLevel;
    public int maxXP = 2800;
    public int maxEachLevelXp = 400;
    public int currentXP;
    public int toMuchXP;
    private bool reachedMaxLevel = false;

    public XpBar xpBar;
    public Text xpText;
    public Text levelText;

    PlayerDamageSystem myPlayerDamageSystem;

    // Start is called before the first frame update
    void Start()
    {
        GetBars();

        myPlayerDamageSystem = GetComponent<PlayerDamageSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        DisplayBars();

        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            DecreaseEnergy(10);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            IncreaseXP(150);
        }

    }
    private void GetBars()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);

        currentXP = 0;
        currentLevel = 0;
        xpBar.SetMaxXP(maxEachLevelXp);
        xpBar.SetXP(currentXP);
    }

    private void DisplayBars()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        energyText.text = currentEnergy + "/" + maxEnergy;
        xpText.text = currentXP + "\n/\n" + maxEachLevelXp;

        levelText.text = currentLevel.ToString();
    }

    public bool CheckForMaxHP()
    {
        if(currentHealth < maxHealth)
        {
            return false;
        }
        else
        {
            return true;
        }
    }    
    public bool CheckForMaxEnergy()
    {
        if(currentEnergy < maxEnergy)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void IncreaseHP(int hp)
    {
        if(!CheckForMaxHP())
        {
            currentHealth += hp;
            Debug.Log("CurrentHealth: " + currentHealth);
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
        else
        {
            Debug.Log("HP full");
        }
            
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void DecreaseEnergy(int energy)
    {
        if (energy <= 0) return;
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);
    }

    public void IncreaseEnergy(int energy)
    {
        if (!CheckForMaxEnergy())
        {
            currentEnergy += energy;
            if (currentEnergy > maxEnergy)
                currentEnergy = maxEnergy;
            energyBar.SetEnergy(currentEnergy);
        }
        else
        {
            Debug.Log("Energy full");
        }


    }

    public void IncreaseXP(int xp)
    {
        if (reachedMaxLevel)
            return;

        currentXP += xp;
        //Debug.Log("MaxXP: " + maxXP + " CurrentXp: " + currentXP + " xpIncreased: " +xpIncreased);
        if (currentXP == maxEachLevelXp)
        {
            //Debug.Log("currentXP == maxEachLevelXp");    
            IncreaseLevel();
            //maxXP -= currentXP;
            xpBar.ResetXP(maxEachLevelXp);
            currentXP = 0;
        } else if (currentXP > maxEachLevelXp)
        {
            //Debug.Log("currentXP > maxEachLevelXp");
            maxXP -= currentXP;
            int overXP = currentXP - maxEachLevelXp;         
            IncreaseLevel();
            //xpBar.ResetXP(maxEachLevelXp);
            currentXP = overXP;

        }

        if (reachedMaxLevel)
        {
            xpBar.SetXP(maxEachLevelXp);
            currentXP = maxEachLevelXp;
        }
        else
        { 
            xpBar.SetXP(currentXP);
        }

    }

    public void IncreaseLevel()
    {
        if(currentLevel != 8)
        {
            ++currentLevel;
            maxHealth += 20;
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            maxEnergy += 10;
            currentEnergy = maxEnergy;
            energyBar.SetMaxEnergy(maxEnergy);

            myPlayerDamageSystem.IncreaseDmgPerLevel(currentLevel);
        }
        else
        {
            reachedMaxLevel = true;
        }
    }

    public int GetCurrentLeve()
    {
        return currentLevel;
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }
}
