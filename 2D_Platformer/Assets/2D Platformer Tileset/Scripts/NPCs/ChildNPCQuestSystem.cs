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

    private string[] quests;

    int xpPerQuest = 100;


    StorySystem myStorySystem;

    PlayerHealthXpSystem myPlayerHealthXpSystem;
    // Start is called before the first frame update
    void Start()
    {
        interactText.text = "Quests";
        questText.text = "Quest start at lvl 1";
        myPlayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
        myStorySystem = FindObjectOfType<StorySystem>();
        quests = myStorySystem.GetQuests();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayQuests();
        //Debug.Log("Quest: " + currentQuestCompleted);
        //Debug.Log("Quest: " + questCounter);

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
