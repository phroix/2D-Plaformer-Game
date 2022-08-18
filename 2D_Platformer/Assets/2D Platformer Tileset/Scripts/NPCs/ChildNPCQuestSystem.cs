using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChildNPCQuestSystem : MonoBehaviour
{
    public GameObject acceptButtonFg;
    public GameObject submitButtonFg;    
    
    public GameObject acceptButton;
    public GameObject submitButton;

    public Text interactText;

    public Text questText;


    bool currentQuestAccepted = false;
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
        CheckForCurrentQuestCompleted();


        if (Input.GetKeyDown(KeyCode.H))
        {
            myStorySystem.SetCurrentQuestCompleted(true);
        }

    }

    private void DisplayQuests()
    {
        var currentLevel = myPlayerHealthXpSystem.GetCurrentLevel();
        if (currentLevel > questCounter && currentLevel!= 0)
        {
            questText.text = quests[questCounter];
            acceptButton.SetActive(true);
            submitButton.SetActive(true);
        }
    }

    public void CompleteQuest()
    {
        if (currentQuestCompleted && currentQuestAccepted && !myPlayerHealthXpSystem.GetReachedMaxLevel())
        {
            myPlayerHealthXpSystem.IncreaseXP(xpPerQuest);
            myStorySystem.SetCurrentQuestCompleted(false);
            ++questCounter;
            currentQuestAccepted = false;
            acceptButtonFg.SetActive(false);
            submitButtonFg.SetActive(true);

            acceptButton.SetActive(false);
            submitButton.SetActive(false);
        }


    }

    private void CheckForCurrentQuestCompleted()
    {
        currentQuestCompleted = myStorySystem.GetCurrentQuestCompleted();
    }

    public void PressAcceptButton()
    {
        currentQuestAccepted = true;
        acceptButtonFg.SetActive(true);
        submitButtonFg.SetActive(false);
    }

}
