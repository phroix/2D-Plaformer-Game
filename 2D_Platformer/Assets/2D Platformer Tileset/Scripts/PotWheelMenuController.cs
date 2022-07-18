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




    public GameObject damageBoostPotActive;
    private bool damageBoostPotIsActive;
    float nextdamageBoostPot = 0f;



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

    }

    private void CooldownPotActive()
    {
        if (!cooldownPotIsActive) return;
        if(Time.time >= nextCooldownPot && !cooldownPotIsActive)
        {
            cooldownPotIsActive = true;
            cooldownImage.fillAmount = 0;
            nextCooldownPot = Time.time;
        }

        if (cooldownPotIsActive)
        {
            cooldownImage.fillAmount = cooldownImage.fillAmount + (1 / cooldownPotCooldown * Time.deltaTime);

            if(cooldownImage.fillAmount == 1)
            {
                cooldownPotActive.SetActive(false);
                cooldownPotIsActive = false;
            }
        }
    }

    private void OpenMenuWheel()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            potWheelSelected = !potWheelSelected;
        }

        if (potWheelSelected)
        {
            myAnim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
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
                myPot.UseDamageBoosPot(); //UseEnergyPot
                damageBoostPotIsActive = true;
                damageBoostPotActive.SetActive(damageBoostPotIsActive);
                break;
            case 3:
                myPot.UseCooldownPot();
                cooldownPotIsActive = true;
                cooldownPotActive.SetActive(cooldownPotIsActive);
                break;
            case 4:
                myPot.UseEnergyPot(); //UseDamageBoostPot
                myplayerHealthXpSystem.IncreaseEnergy(20);
                break;
        }
        potID = 0;

    }
}
