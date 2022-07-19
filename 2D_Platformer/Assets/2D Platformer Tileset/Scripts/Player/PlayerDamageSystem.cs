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


    PlayerMovement myPlayerMovement;


    // Start is called before the first frame update
    void Start()
    {
        bowCurrentDmg = bowStartDmg;
        swordCurrentDmg = swordStartDmg;
        myPlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myPlayerMovement.GetCurrentWeapon().ToString());
        DisplayDamage();
    }

    private void DisplayDamage()
    {
        if (myPlayerMovement.GetCurrentWeapon().ToString() == "Sword (UnityEngine.GameObject)")
        {
            Debug.Log("Sword active");
            dmgText.text = "DMG: " + swordCurrentDmg.ToString();
        }
        else if (myPlayerMovement.GetCurrentWeapon().ToString() == "Bow (UnityEngine.GameObject)")
        {
            Debug.Log("Bow active");
            dmgText.text = "DMG: " + bowCurrentDmg.ToString();
        }else
        {
            Debug.Log("Default active");
            dmgText.text = "DMG: 0";
        }
    }
}
