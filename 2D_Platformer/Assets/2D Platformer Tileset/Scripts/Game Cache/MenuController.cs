using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    GameController myGameController;


    // Start is called before the first frame update
    void Start()
    {
        myGameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMenuScene(bool b)
    {
        gameObject.SetActive(b);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainCity");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("MainCity");
    }

    public void OpenSettings()
    {
        myGameController.OpenSettingsOverlay();
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {

    }

}
