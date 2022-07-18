using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotsWheelButton : MonoBehaviour
{
    public int id;
    private Animator myAnimator;
    public string itemName;
    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ID: " + id);
        if (selected)
        {
            Debug.Log(itemName);
        }
    }

    public void Selected()
    {
        //Debug.Log("Selected");
        selected = true;
        PotWheelMenuController.potID = id;
        
    }

    public void DeSelected()
    {
        //Debug.Log("DeSelected");
        selected = false;
        PotWheelMenuController.potID = 0;
    }

    public void HoverEnter()
    {
        //Debug.Log("hover");
        myAnimator.SetBool("hover", true);
    }

    public void HoverExit()
    {
        //Debug.Log("Dehover");
        myAnimator.SetBool("hover", false);
    }
}
