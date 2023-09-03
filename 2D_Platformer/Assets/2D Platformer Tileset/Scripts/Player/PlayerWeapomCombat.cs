using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapomCombat : MonoBehaviour
{
    PlayerMovement myPlayerMovement;

    public GameObject qAbility;
    public GameObject eAbility;

    //Child component
    Animator childAnimator;

    ////Weapons
    GameObject currentWeapon;
    GameObject defaultWeapon;
    GameObject swordWeapon;
    GameObject bowWeapon;
    GameObject spearWeapon;

    //Weapons holding
    bool holdingSwordWeapon = false;
    bool holdingBowWeapon = false;
    bool holdingSpearWeapon = false;


    // Start is called before the first frame update
    void Start()
    {
        GetChildren();
        SetWeaponActive(true, false, false, false, defaultWeapon);
        currentWeapon = defaultWeapon;
        if (myPlayerMovement != null) myPlayerMovement.SetCurrentWeapon(currentWeapon);
        myPlayerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentWeapon();
        if (myPlayerMovement != null) myPlayerMovement.SetCurrentWeapon(currentWeapon);
        //Debug.Log("currentWeapon2: " + currentWeapon.name);


    }

    //Get Children of this GameObject 
    private void GetChildren()
    {
        defaultWeapon = gameObject.transform.GetChild(0).gameObject;
        swordWeapon = gameObject.transform.GetChild(1).gameObject;
        bowWeapon = gameObject.transform.GetChild(2).gameObject;
        spearWeapon = gameObject.transform.GetChild(3).gameObject;
    }

    //Gets current equipped weapon
    private void SetCurrentWeapon()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))//Check if 1 is pressed
        {
            SetWeaponActive(true, false, false, false, defaultWeapon);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && holdingSwordWeapon)//Check if 2 is pressed
        {
            SetWeaponActive(false, true, false, false, swordWeapon);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && holdingBowWeapon)//Check if 3 is pressed
        {
            SetWeaponActive(false, false, true, false, bowWeapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && holdingSpearWeapon)//Check if 3 is pressed
        {
            SetWeaponActive(false, false, false, true, spearWeapon);
        }


           if(childAnimator!=null) childAnimator = currentWeapon.GetComponent<Animator>(); //gets component of child Animator
            //Debug.Log("!=null");

      
    }

    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }

    private void SetWeaponActive(bool b1, bool b2, bool b3, bool b4, GameObject g)
    {
        defaultWeapon.SetActive(b1);
        swordWeapon.SetActive(b2);
        bowWeapon.SetActive(b3);
        spearWeapon.SetActive(b4);
        if (g != null) currentWeapon = g;

        qAbility.SetActive(!b1);
        eAbility.SetActive(!b1);

    }

    public void SetSwordHolding()
    {
        holdingSwordWeapon = true;
    }

    public void GetSwordHolding()
    {
        holdingSwordWeapon = true;
    }

    public void SetBowHolding()
    {
        holdingBowWeapon = true;
    }

    public void GetBowHolding()
    {
        holdingBowWeapon = true;
    }

    public void SetSpearHolding()
    {
        holdingSpearWeapon = true;
    }

    public void GetSpearHolding()
    {
        holdingSpearWeapon = true;
    }

    public bool GetBowHoldingVar()
    {
        return holdingBowWeapon;
    }

    public bool GetSwordHoldingVar()
    {
        return holdingSwordWeapon;
    }

    public GameObject GetChildAnimator()
    {
        return currentWeapon;
    }

    //private void CheckForScene()
    //{
    //    var currentScene = SceneManager.GetActiveScene();
    //    if (currentScene.name == "TutorialScene")
    //    {
    //        holdingSwordWeapon = true;
    //        holdingBowWeapon = true;
    //        holdingSpearWeapon = true;
    //        swordWeapon.GetComponent<PlayerSwordCombat>().SetEnergyCost(0, 0);
    //        bowWeapon.GetComponent<PlayerBowCombat>().SetEnergyCost(0, 0,100);
    //        spearWeapon.GetComponent<PlayerSpearCombat>().SetEnergyCost(0, 0);

    //    }
    //    else
    //    {
    //        holdingSwordWeapon = false;
    //        holdingBowWeapon = false;
    //        holdingSpearWeapon = false;
    //        swordWeapon.GetComponent<PlayerSwordCombat>().SetEnergyCost(30, 20);
    //        bowWeapon.GetComponent<PlayerBowCombat>().SetEnergyCost(30, 20, 0);
    //        spearWeapon.GetComponent<PlayerSwordCombat>().SetEnergyCost(30, 20);
    //    }

    //}

}
