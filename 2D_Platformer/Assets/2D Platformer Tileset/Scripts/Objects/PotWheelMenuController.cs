using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotWheelMenuController : MonoBehaviour
{
    public Animator myAnim;
    public static int potID;
    private bool potWheelSelected = false;
    

    public GameObject cooldownPotActive;
    public Image cooldownImage;
    private bool cooldownPotIsActive;
    float nextCooldownPot = 0f;
    float cooldownPotCooldown = 10f;
    float potRate = 2f;

    public GameObject damageBoostPotActive;
    public Image damageBoostImage;
    private bool damageBoostPotIsActive;
    float nextdamageBoostPot = 0f;
    float damageBoostPotCooldown = 10f;


    int bowCurrentDmg;
    int swordCurrentDmg;

    Pots myPot;
    PlayerHealthXpSystem myplayerHealthXpSystem;
    PlayerBowCombat myPlayerBowCombat;
    PlayerSwordCombat myPlayerSwordCombat;
    PlayerDamageSystem myPlayerDamageSystem;

    void Start()
    {
        myplayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
        myPlayerBowCombat = FindObjectOfType<PlayerBowCombat>();
        myPlayerSwordCombat = FindObjectOfType<PlayerSwordCombat>();
        myPlayerDamageSystem = FindObjectOfType<PlayerDamageSystem>();
        myPot = FindObjectOfType<Pots>();
    }
    // Update is called once per frame
    void Update()
    {
        OpenMenuWheel();
        UsePots();
        CooldownPotActive();
        DamageBoostPotActive();
    }

    private void DamageBoostPotActive()
    {
        if (!damageBoostPotIsActive) return;

        if (Time.time >= nextdamageBoostPot && !damageBoostPotIsActive)
        {
            damageBoostPotIsActive = true;
            damageBoostImage.fillAmount = 0;
            nextdamageBoostPot = Time.time + (1 / potRate);
        }

        if (damageBoostPotIsActive)
        {
            myPlayerDamageSystem.SetBowCurrentDMG(bowCurrentDmg + 10);
            myPlayerDamageSystem.SetSwordCurrentDMG(swordCurrentDmg + 15);

            damageBoostImage.fillAmount = damageBoostImage.fillAmount + (1 / damageBoostPotCooldown * Time.deltaTime);

            if (damageBoostImage.fillAmount == 1)
            {
                damageBoostImage.fillAmount = 0;
                damageBoostPotIsActive = false;
                myPlayerDamageSystem.SetBowCurrentDMG(bowCurrentDmg);
                myPlayerDamageSystem.SetSwordCurrentDMG(swordCurrentDmg);
            }
        }

        damageBoostPotActive.SetActive(damageBoostPotIsActive);
    }

    private void CooldownPotActive()
    {
        if (!cooldownPotIsActive) return;
        if(Time.time >= nextCooldownPot && !cooldownPotIsActive)
        {
            cooldownPotIsActive = true;
            cooldownImage.fillAmount = 0;
            nextCooldownPot = Time.time + (1/potRate);
        }

        if (cooldownPotIsActive)
        {
            myPlayerBowCombat.SetQCooldown(3.5f);
            myPlayerBowCombat.SetECooldown(2f);

            myPlayerSwordCombat.SetQCooldown(3.5f);
            myPlayerSwordCombat.SetECooldown(2f);

            cooldownImage.fillAmount = cooldownImage.fillAmount + (1 / cooldownPotCooldown * Time.deltaTime);

            if(cooldownImage.fillAmount == 1)
            {
                cooldownImage.fillAmount = 0;
                cooldownPotIsActive = false;
                myPlayerBowCombat.SetQCooldown(5f);
                myPlayerBowCombat.SetECooldown(3f);

                myPlayerSwordCombat.SetQCooldown(5f);
                myPlayerSwordCombat.SetECooldown(3f);
            }
        }

        cooldownPotActive.SetActive(cooldownPotIsActive);
    }

    private void OpenMenuWheel()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            potWheelSelected = !potWheelSelected;
        }

        if (potWheelSelected)
        {
            Time.timeScale = 0.5f;
            myAnim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            Time.timeScale = 1f;
            myAnim.SetBool("OpenWeaponWheel", false);
        }
    }

    private void UsePots()
    {
        switch (potID)
        {
            case 1:
                myPot.UseHealthpot();
                myplayerHealthXpSystem.IncreaseHP(20);
                break;
            case 2:
                if (!damageBoostPotIsActive && myPot.GetDamageBoosPotNumb() > 0)
                {
                    bowCurrentDmg = myPlayerDamageSystem.GetBowCurrentDMG();
                    swordCurrentDmg = myPlayerDamageSystem.GetSwordCurrentDMG();
                    myPot.UseDamageBoosPot(); //UseEnergyPot
                    damageBoostPotIsActive = true;
                }
                break;
            case 3:
                if (!cooldownPotIsActive && myPot.GetCooldownPotNumb() > 0 )
                {
                    myPot.UseCooldownPot();
                    cooldownPotIsActive = true;
                }              
                break;
            case 4:
                myPot.UseEnergyPot(); //UseDamageBoostPot
                myplayerHealthXpSystem.IncreaseEnergy(20);
                break;
        }
        potID = 0;

    }

    public bool GetPotWheelSelected()
    {
        return potWheelSelected;
    }
}
