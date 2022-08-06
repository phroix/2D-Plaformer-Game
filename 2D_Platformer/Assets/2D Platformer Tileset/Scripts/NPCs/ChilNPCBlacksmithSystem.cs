using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChilNPCBlacksmithSystem : MonoBehaviour
{
    public Text interactText;

    public Text swordLevelText;
    public Text bowLevelText;

    public GameObject swordUpgradeFg;
    public GameObject bowUpgradeFg;

    private static int sowrdUpgradeClicked = 0;
    private static int bowUpgradeClicked = 0;

    PlayerHealthXpSystem myPlayerHealthXpSystem;
    PlayerDamageSystem myPlayerDamageSystem;


    // Start is called before the first frame update
    void Start()
    {
        myPlayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
        myPlayerDamageSystem = FindObjectOfType<PlayerDamageSystem>();
        interactText.text = "Weapon Upgrade";
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayLevelText();
        //CehckForUpgrade();
        Debug.Log("Current Lvl: " + myPlayerHealthXpSystem.GetCurrentLevel());
    }
    private void DisplayLevelText()
    {
        swordLevelText.text = sowrdUpgradeClicked.ToString();
        bowLevelText.text = bowUpgradeClicked.ToString();

        SetActiveBowSword();

    }

    private void SetActiveBowSword()
    {
        if (CehckForSwordUpgrade()) swordUpgradeFg.SetActive(false);
        else swordUpgradeFg.SetActive(true);
        if (CehckForBowUpgrade()) bowUpgradeFg.SetActive(false);
        else bowUpgradeFg.SetActive(true);
    }

    public bool CehckForSwordUpgrade()
    {
        if(myPlayerHealthXpSystem.GetCurrentLevel() > sowrdUpgradeClicked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CehckForBowUpgrade()
    {
        if (myPlayerHealthXpSystem.GetCurrentLevel() > bowUpgradeClicked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwordDmgUpgrade()
    {
        if (CehckForSwordUpgrade())
        {
            ++sowrdUpgradeClicked;
            myPlayerDamageSystem.IncreaseSwordDmgUpgrade(3);

        }
    }

    public void BowDmgUpgrade()
    {
        if (CehckForBowUpgrade())
        {
            ++bowUpgradeClicked;
            myPlayerDamageSystem.IncreaseBowDmgUpgrade(3);

        }
    }


}
