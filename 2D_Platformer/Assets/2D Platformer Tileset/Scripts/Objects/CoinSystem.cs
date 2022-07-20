using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    bool collectedPot = true;

    PlayerCoinController myPlayerCoinController;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerCoinController = FindObjectOfType<PlayerCoinController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name != "Player") return;

        if (collectedPot)
        {
            myPlayerCoinController.IncreaseCurrentCoins();
            Destroy(gameObject);
        }

        collectedPot = false;

    }
}
