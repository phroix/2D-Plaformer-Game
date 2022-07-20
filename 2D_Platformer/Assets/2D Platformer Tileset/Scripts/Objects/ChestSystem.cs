using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSystem : MonoBehaviour
{
    Animator myAnimator;
    PlayerCoinController myPlayerCoinController;

    bool playerDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myPlayerCoinController = FindObjectOfType<PlayerCoinController>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        if(Input.GetKeyDown(KeyCode.F) && playerDetected && myPlayerCoinController.GetCurrentKeys() > 0)
        {
            myAnimator.SetTrigger("OpenChest");
            myPlayerCoinController.DecreaseCurrentKeys(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        playerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        playerDetected = false;
    }
}
