using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentNPCSystem : MonoBehaviour
{
    public GameObject canvasOverlay;
    bool playerDetected = false;
    bool canvasOverlayOpened = false;


    // Start is called before the first frame update
    void Start()
    {
        canvasOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenCanvasOverlay();
    }

    private void OpenCanvasOverlay()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerDetected)
        {
            canvasOverlayOpened = !canvasOverlayOpened;
            canvasOverlay.SetActive(true);
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
}
