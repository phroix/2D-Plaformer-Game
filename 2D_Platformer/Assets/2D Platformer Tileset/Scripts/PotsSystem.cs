using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotsSystem : MonoBehaviour
{

    GameObject currentPot;
    GameObject healthPot;
    GameObject damageBoostPot;
    GameObject cooldownPot;
    GameObject energyPot;

    Pots pot;


    // Start is called before the first frame update
    void Start()
    {
        GetChildren();
        int randomNumb = Random.Range(1, 5);

        switch (randomNumb)
        {
            case 1:
                //currentPot = healthPot;
                setCurrentPot(healthPot, true,false,false,false);
                break;
            case 2:
                //currentPot = damageBoostPot;
                setCurrentPot(damageBoostPot, false, true, false, false);
                break;
            case 3:
                //currentPot = cooldownPot;
                setCurrentPot(cooldownPot, false, false, true, false);
                break;
            case 4:
                //currentPot = energyPot;
                setCurrentPot(energyPot, false, false, false, true);
                break;

        }
        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void GetChildren()
    {
        healthPot = gameObject.transform.GetChild(0).gameObject;
        damageBoostPot = gameObject.transform.GetChild(1).gameObject;
        cooldownPot = gameObject.transform.GetChild(2).gameObject;
        energyPot = gameObject.transform.GetChild(3).gameObject;
    }

    private void setCurrentPot(GameObject g,bool b1, bool b2, bool b3, bool b4)
    {
        healthPot.SetActive(b1);
        damageBoostPot.SetActive(b2);
        cooldownPot.SetActive(b3);
        energyPot.SetActive(b4);
        currentPot = g;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.name == "Player") return;

        switch (gameObject.name)
        {
            case "Health":
                //currentPot = healthPot;
                pot.IncreaseHealthpot();
                break;
            case "Damageboost":
                //currentPot = damageBoostPot;
                pot.IncreaseDamageBoosPot();
                break;
            case "Cooldown":
                //currentPot = cooldownPot;
                pot.IncreaseCooldownPot();
                break;
            case "Energy":
                //currentPot = energyPot;
                pot.IncreaseEnergyPot();
                break;

        }
        Destroy(gameObject);
    }


}
