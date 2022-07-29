using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildNPCNormalSystem : MonoBehaviour
{
    public Text interactText;

    private String[] messages = {"1", "2" , "3" , "4" , "5", "6","7","8" };

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomMessage()
    {
        int randomNumb = UnityEngine.Random.Range(1, 9);
        interactText.text = messages[randomNumb-1];

    }

}
