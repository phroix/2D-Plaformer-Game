using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNPCWeaponSystem : MonoBehaviour
{
    public Text interactText;

    public GameObject swordFg;
    public int sowrdCost = 75;
    bool swordBuy = false;

    public GameObject bowFg;
    public int bowCost = 150;
    bool bowBuy = false;

    public GameObject arrowFg;
    public int arrowCost = 2;
    bool arrowBuy = false;

    PlayerCoinController myPlayerCoinController;
    PlayerBowCombat myPlayerBowCombat;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerCoinController = FindObjectOfType<PlayerCoinController>();
        myPlayerBowCombat = FindObjectOfType<PlayerBowCombat>();

        interactText.text = "Weapon Shop";
    }

    // Update is called once per frame
    void Update()
    {
        BuyController();
    }

    private void BuyController()
    {

        if (swordBuy || sowrdCost > myPlayerCoinController.GetCurrentCoins())
        {
            swordFg.SetActive(true);
        }
        else
        {
            swordFg.SetActive(false);
        }

        if (bowBuy || bowCost > myPlayerCoinController.GetCurrentCoins())
        {
            bowFg.SetActive(true);
        }
        else
        {
            bowFg.SetActive(false);
        }

        if (arrowCost > myPlayerCoinController.GetCurrentCoins())
        {
            arrowFg.SetActive(true);
        }
        else
        {
            arrowFg.SetActive(false);
        }
    }

    public void BuySword()
    {
        if(sowrdCost <= myPlayerCoinController.GetCurrentCoins() && !swordBuy)
        {
            myPlayerCoinController.DecreaseCurrentCoins(sowrdCost);
            swordBuy = true;
            swordFg.SetActive(true);
        }
    }

    public void BuyBow()
    {
        if (bowCost <= myPlayerCoinController.GetCurrentCoins() && !bowBuy)
        {
            myPlayerCoinController.DecreaseCurrentCoins(bowCost);
            bowBuy = true;
            bowFg.SetActive(true);
        }
    }

    public void BuyArrow()
    {
        if (arrowCost <= myPlayerCoinController.GetCurrentCoins() )
        {
            myPlayerBowCombat.IncreaseArrows();
            myPlayerCoinController.DecreaseCurrentCoins(arrowCost);
        }
    }




}
