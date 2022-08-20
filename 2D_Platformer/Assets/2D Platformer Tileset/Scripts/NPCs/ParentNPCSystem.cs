using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentNPCSystem : MonoBehaviour
{
    ChildNPCNormalSystem myChildNPCNormalSystem;
    public GameObject canvasOverlay;
    bool playerDetected = false;
    static bool canvasOverlayOpened = false;


    // Start is called before the first frame update
    void Start()
    {
        canvasOverlay.SetActive(false);
        myChildNPCNormalSystem = GetComponent<ChildNPCNormalSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ParentNPCSystem canvasOverlayOpened: " + canvasOverlayOpened);
        OpenCanvasOverlay();
    }

    private void OpenCanvasOverlay()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerDetected)
        {
            canvasOverlayOpened = !canvasOverlayOpened;
            canvasOverlay.SetActive(true);
            if(myChildNPCNormalSystem!= null) myChildNPCNormalSystem.GenerateRandomMessage();

        }

        if (!canvasOverlayOpened)
            canvasOverlay.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        playerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "Player") return;
        canvasOverlayOpened = false;
        playerDetected = false;
    }

    public bool GetCanvasOverlayOpened()
    {
        return canvasOverlayOpened;
    }
}
