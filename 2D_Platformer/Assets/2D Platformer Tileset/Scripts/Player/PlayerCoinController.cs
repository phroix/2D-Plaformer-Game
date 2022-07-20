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

    // Start is called before the first frame update
    void Start()
    {
        currentCoin = startCoin;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayCurrentCoins();
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



}
