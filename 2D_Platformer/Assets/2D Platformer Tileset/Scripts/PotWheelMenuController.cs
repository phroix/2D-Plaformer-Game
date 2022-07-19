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
    
    Pots myPot;
    PlayerHealthXpSystem myplayerHealthXpSystem;

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



    void Start()
    {
        myplayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
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
            Debug.Log("Fill image");
            damageBoostImage.fillAmount = damageBoostImage.fillAmount + (1 / damageBoostPotCooldown * Time.deltaTime);

            if (damageBoostImage.fillAmount == 1)
            {
                damageBoostImage.fillAmount = 0;
                damageBoostPotIsActive = false;
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
            Debug.Log("Fill image");
            cooldownImage.fillAmount = cooldownImage.fillAmount + (1 / cooldownPotCooldown * Time.deltaTime);

            if(cooldownImage.fillAmount == 1)
            {
                cooldownImage.fillAmount = 0;
                cooldownPotIsActive = false;
            }
        }

        cooldownPotActive.SetActive(cooldownPotIsActive);
    }

    private void OpenMenuWheel()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
                if (!damageBoostPotIsActive)
                {
                    myPot.UseDamageBoosPot(); //UseEnergyPot
                    damageBoostPotIsActive = true;
                }
                break;
            case 3:
                if (!cooldownPotIsActive)
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
}
