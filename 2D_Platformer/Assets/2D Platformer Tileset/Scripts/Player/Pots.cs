using System;
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

    public GameObject healthPotF;
    public GameObject damageBoosPotF;
    public GameObject cooldownPotF;
    public GameObject energyPotF;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePotText();
        UpdatePotF();
    }

    private void UpdatePotF()
    {
        if (healthPotNumb > 0) healthPotF.SetActive(false);
        else healthPotF.SetActive(true);

        if (damageBoosPotNumb > 0) damageBoosPotF.SetActive(false);
        else damageBoosPotF.SetActive(true);

        if (cooldownPotNumb > 0) cooldownPotF.SetActive(false);
        else cooldownPotF.SetActive(true);

        if (energyPotNumb > 0) energyPotF.SetActive(false);
        else energyPotF.SetActive(true);

    }

    private void UpdatePotText()
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
    public void UseHealthpot()
    {
        --healthPotNumb;
    }

    public void IncreaseDamageBoosPot()
    {
        ++damageBoosPotNumb;
    }
    public void UseDamageBoosPot()
    {
        --damageBoosPotNumb;
    }

    public void IncreaseCooldownPot()
    {
        ++cooldownPotNumb;
    }
    public void UseCooldownPot()
    {
        --cooldownPotNumb;
    }

    public void IncreaseEnergyPot()
    {
        ++energyPotNumb;
    }
    public void UseEnergyPot()
    {
        --energyPotNumb;
    }
}
