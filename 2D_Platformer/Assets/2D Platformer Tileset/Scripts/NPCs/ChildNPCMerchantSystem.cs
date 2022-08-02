using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNPCMerchantSystem : MonoBehaviour
{
    public Text interactText;

    Pots myPots;
    PlayerCoinController myPlayerCoinController;
    PlayerBowCombat myPlayerBowCombat;


    private int healthPotCost = 25;
    public GameObject healthPotCostFg;


    private int energyPotCost = 50;
    public GameObject energyPotFg;


    private int cooldownPotCost = 50;
    public GameObject cooldownPotFg;


    private int damageBoostPotCost = 25;
    public GameObject damageBoostPotFg;


    private int arrowCost = 2;
    public GameObject arrowFg;


    // Start is called before the first frame update
    void Start()
    {
        interactText.text = "Item Shop";
        myPots = FindObjectOfType<Pots>();
        myPlayerCoinController = FindObjectOfType<PlayerCoinController>();
        myPlayerBowCombat = FindObjectOfType<PlayerBowCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerBowCombat == null) Debug.Log("nzukkl");
        SetActiveItems();
    }

    private void SetActiveItems()
    {
        if (myPlayerCoinController.GetCurrentCoins() >= healthPotCost)
            healthPotCostFg.SetActive(false);
        else
            healthPotCostFg.SetActive(true);

        if (myPlayerCoinController.GetCurrentCoins() >= energyPotCost)
            energyPotFg.SetActive(false);
        else
            energyPotFg.SetActive(true);

        if (myPlayerCoinController.GetCurrentCoins() >= cooldownPotCost)
            cooldownPotFg.SetActive(false);
        else
            cooldownPotFg.SetActive(true);

        if (myPlayerCoinController.GetCurrentCoins() >= damageBoostPotCost)
            damageBoostPotFg.SetActive(false);
        else
            damageBoostPotFg.SetActive(true);

        if (myPlayerCoinController.GetCurrentCoins() >= arrowCost)
            arrowFg.SetActive(false);
        else
            arrowFg.SetActive(true);
    }

    public void BuyHealthPot()
    {
        if(myPlayerCoinController.GetCurrentCoins() >= healthPotCost)
        {
            myPots.IncreaseHealthpot();
            myPlayerCoinController.DecreaseCurrentCoins(healthPotCost);
        }
    }

    public void BuyEnergyPot()
    {
        if (myPlayerCoinController.GetCurrentCoins() >= energyPotCost)
        {
            myPots.IncreaseEnergyPot();
            myPlayerCoinController.DecreaseCurrentCoins(energyPotCost);
        }
    }

    public void BuyCooldownPot()
    {
        if(myPlayerCoinController.GetCurrentCoins() >= cooldownPotCost)
        {
            myPots.IncreaseCooldownPot();
            myPlayerCoinController.DecreaseCurrentCoins(cooldownPotCost);

        }
    }

    public void BuyDamageBoostPot()
    {
        if (myPlayerCoinController.GetCurrentCoins() >= damageBoostPotCost)
        {
            myPots.IncreaseDamageBoosPot();
            myPlayerCoinController.DecreaseCurrentCoins(damageBoostPotCost);
        }
    }

    public void BuyArrow()
    {
        if(myPlayerCoinController.GetCurrentCoins() >= arrowCost)
        {
            myPlayerBowCombat.IncreaseArrows();
            myPlayerCoinController.DecreaseCurrentCoins(arrowCost);
        }
            
    }


}
