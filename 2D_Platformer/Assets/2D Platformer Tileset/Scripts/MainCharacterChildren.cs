using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterChildren : MonoBehaviour
{

    //Players components
    GameObject noWeaponPlayer;
    GameObject swordPlayer;
    GameObject bowPlayer;
    GameObject cam;
    GameObject currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GetChildren();
        bowPlayer.SetActive(false);
        swordPlayer.SetActive(false);
        currentPlayer = noWeaponPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeChild();
        if (currentPlayer != null) Debug.Log("update 2: " + currentPlayer);
        cam.transform.position = currentPlayer.transform.position;
    }

    private void GetChildren()
    {
        noWeaponPlayer = gameObject.transform.GetChild(0).gameObject;
        swordPlayer = gameObject.transform.GetChild(1).gameObject;
        bowPlayer = gameObject.transform.GetChild(2).gameObject;
        cam = gameObject.transform.GetChild(3).gameObject;
    }


    private void ChangeChild()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1");
            noWeaponPlayer.SetActive(false);
            bowPlayer.SetActive(false);
            swordPlayer.SetActive(true);
            currentPlayer = swordPlayer;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2");
            noWeaponPlayer.SetActive(false);
            swordPlayer.SetActive(false);
            bowPlayer.SetActive(true);
            currentPlayer = bowPlayer;
        }
    }
    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
    public GameObject GetNoWeaponPlayer()
    {
        return noWeaponPlayer;
    }

    public GameObject GetSwordPlayer()
    {
        return swordPlayer;
    }

    public GameObject GetBowPlayer()
    {
        return bowPlayer;
    }
}
