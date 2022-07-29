using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChildNPCQuestSystem : MonoBehaviour
{
    public Text interactText;

    public Text questText;

    bool currentQuestCompleted = false;
    static int questCounter = 0;

    private string[] quests = { "Quest1", "Quest2", "Quest3", "Quest4", "Quest5", "Quest6", "Quest7", "Quest8" };

    int xpPerQuest = 100;


    PlayerHealthXpSystem myPlayerHealthXpSystem;
    // Start is called before the first frame update
    void Start()
    {
        interactText.text = "Quests";
        questText.text = "Quest start at lvl 1";
        myPlayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayQuests();
        Debug.Log("Quest: " + currentQuestCompleted);
        Debug.Log("Quest: " + questCounter);

        if (Input.GetKeyDown(KeyCode.H))
        {
            currentQuestCompleted = true;
        }

    }

    private void DisplayQuests()
    {
        var currentLevel = myPlayerHealthXpSystem.GetCurrentLeve();
        if (currentLevel > questCounter && currentLevel!= 0)
        {
            questText.text = quests[questCounter];
        }
    }

    public void CompleteQuest()
    {
        if (currentQuestCompleted && !myPlayerHealthXpSystem.GetReachedMaxLevel())
        {
            ++questCounter;
            myPlayerHealthXpSystem.IncreaseXP(xpPerQuest);
            currentQuestCompleted = false;

        }
    }



}
