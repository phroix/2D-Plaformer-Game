using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pots : MonoBehaviour
{

    public int healthPotNumb = 0;
    public int damageBoosPotNumb = 0;
    public int cooldownPotNumb = 0;
    public int energyPotNumb = 0;

    public Text healthPotText;
    public Text damageBoosPotText;
    public Text cooldownPotText;
    public Text energyPotText;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        healthPotText.text = healthPotNumb.ToString();
        damageBoosPotText.text = damageBoosPotNumb.ToString();
        cooldownPotText.text = cooldownPotNumb.ToString();
        energyPotText.text = energyPotNumb.ToString();
    }

    public void IncreaseHealthpot()
    {
        ++healthPotNumb;
    }

    public void IncreaseDamageBoosPot()
    {
        ++damageBoosPotNumb;
    }

    public void IncreaseCooldownPot()
    {
        ++cooldownPotNumb;
    }

    public void IncreaseEnergyPot()
    {
        ++energyPotNumb;
    }
}
