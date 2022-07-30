using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject canvasOverlay;
    public GameObject settingCanvasOverlay;

    bool escOverlayOpened = false;
    bool settingsOverlayOpened = false;

    private void Awake()
    {
        int numbOfGameSession = FindObjectsOfType<GameController>().Length;

        if (numbOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        settingCanvasOverlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        OpenESCOverlay();
        Debug.Log(Time.timeScale);
    }

    private void OpenESCOverlay()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escOverlayOpened = !escOverlayOpened;
            canvasOverlay.SetActive(true);
        }

        if (!escOverlayOpened)
        {
            canvasOverlay.SetActive(false);
            settingCanvasOverlay.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void CloseESCOverlay()
    {
        canvasOverlay.SetActive(false);
        escOverlayOpened = !escOverlayOpened;
    }
    public void OpenSettingsOverlay()
    {
        settingCanvasOverlay.SetActive(true);
    }

    public void CloseSettingsOverlay()
    {
        settingCanvasOverlay.SetActive(false);
    }

    public void ChangeMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
