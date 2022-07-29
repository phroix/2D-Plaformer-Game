using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinController : MonoBehaviour
{
    int startCoin = 100;
    int currentCoin = 0;

    public Text coinText;
    public Text coinTextShadder;


    int currentKeys = 0;
    public Text keyText;
    public Text keyTextShadder;

    // Start is called before the first frame update
    void Start()
    {
        currentCoin = startCoin;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCurrentCoins();
        DisplayCurrentKeys();
    }

    private void DisplayCurrentKeys()
    {
        keyText.text = currentKeys.ToString();
        keyTextShadder.text = currentKeys.ToString();
    }

    public void DisplayCurrentCoins()
    {
        coinText.text = currentCoin.ToString();
        coinTextShadder.text = currentCoin.ToString();
    }

    public void IncreaseCurrentCoins()
    {
        ++currentCoin;
    }

    public void DecreaseCurrentCoins(int i)
    {
        currentCoin -= i;
    }

    public int GetCurrentCoins()
    {
        return currentCoin;
    }

    public void IncreaseCurrentKeys()
    {
        ++currentKeys;
    }

    public void DecreaseCurrentKeys()
    {
        --currentKeys;
    }

    public int GetCurrentKeys()
    {
        return currentKeys;
    }


}
