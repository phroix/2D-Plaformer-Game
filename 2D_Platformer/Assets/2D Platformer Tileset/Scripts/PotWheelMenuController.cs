using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotWheelMenuController : MonoBehaviour
{

    public int Id;
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
        if (selected)
        {
            selectedItem.sprite = icon;
        }
    }

    public void Selected()
    {
        selected = true;
    }

    public void DeSelected()
    {
        selected = false;
    }

    public void HoverEnter()
    {
        myAnimator.SetBool("hover", true);
    }

    public void HoverExit()
    {
        myAnimator.SetBool("hover", false);
    }
}
