using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageSystem : MonoBehaviour
{
    public Text dmgText;

    public int swordStartDmg = 25;
    public int swordCurrentDmg;
    


    public int bowStartDmg = 15;
    public int bowCurrentDmg;

    PlayerHealthXpSystem myPlayerHealthXpSystem;
    PlayerMovement myPlayerMovement;


    // Start is called before the first frame update
    void Start()
    {
        SetDamageStats();
        GetParentComponents();
    }

    private void GetParentComponents()
    {
        myPlayerMovement = GetComponent<PlayerMovement>();
        myPlayerHealthXpSystem = GetComponent<PlayerHealthXpSystem>();
    }

    private void SetDamageStats()
    {
        bowCurrentDmg = bowStartDmg;
        swordCurrentDmg = swordStartDmg;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayDamage();
    }

    public void IncreaseDmgPerLevel(int currenLevel)
    {
        int extraSwordDMG = 0;
        int extraBowDMG = 0;
        switch (currenLevel)
        {
            case 0:
                break;
            case 1:
                extraSwordDMG = 5;
                extraBowDMG = 5;
                break;
            case 2:
                extraSwordDMG = 5;
                extraBowDMG = 5;
                break;
            case 3:
                extraSwordDMG = 5;
                extraBowDMG = 5;
                break;
            case 4:
                extraSwordDMG = 10;
                extraBowDMG = 10;
                break;
            case 5:
                extraSwordDMG = 15;
                extraBowDMG = 10;
                break;
            case 6:
                extraSwordDMG = 20;
                extraBowDMG = 15;
                break;
            case 7:
                extraSwordDMG = 25;
                extraBowDMG = 15;
                break;
            case 8:
                extraSwordDMG = 30;
                extraBowDMG = 25;
                break;

        }
        swordCurrentDmg += extraSwordDMG;
        bowCurrentDmg += extraBowDMG;
    }


    private void DisplayDamage()
    {
        if (myPlayerMovement.GetCurrentWeapon().ToString() == "Sword (UnityEngine.GameObject)")
        {
            //Debug.Log("Sword active");
            dmgText.text = "DMG: " + swordCurrentDmg.ToString();
        }
        else if (myPlayerMovement.GetCurrentWeapon().ToString() == "Bow (UnityEngine.GameObject)")
        {
            //Debug.Log("Bow active");
            dmgText.text = "DMG: " + bowCurrentDmg.ToString();
        }else
        {
            //Debug.Log("Default active");
            dmgText.text = "DMG: 0";
        }
    }

    public void SetSwordCurrentDMG(int s)
    {
        swordCurrentDmg = s;
    }

    public void SetBowCurrentDMG(int b)
    {
        bowCurrentDmg = b;
    }

    public int GetSwordCurrentDMG()
    {
        return swordCurrentDmg;
    }

    public int GetBowCurrentDMG()
    {
        return bowCurrentDmg;
    }
}
