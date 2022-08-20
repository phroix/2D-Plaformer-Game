using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystem : MonoBehaviour
{
    private string[] quests = { 
        "Kill 4 Slimes and 4 Bees in this City!",
        "Kill 5 Spiders, you can find them somewhere in this village!", 
        "Save the villagers and kill all enemys in this village!", 
        "Find the golden key in cave2!", 
        "Kill 20 Normal Skeletons in the Caves!",
        "Kill 3 Caster Skeletons in the Caves!", 
        "Find the lost villager in the Caves!", 
        "Kill the Boss in the Bosscave!" };

    private bool currentQuestCompleted = false;

    private int currentKillQuestCounter = 0;
    private bool currentKillQuestPickup = false;

    PlayerHealthXpSystem myPlayerHealthXpSystem;

    // Start is called before the first frame update
    void Start()
    {

        myPlayerHealthXpSystem = FindObjectOfType<PlayerHealthXpSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            IncreaseCurrentKillQuestCounter();
        }
        //Debug.Log("currentQuestCompleted: " + currentQuestCompleted);
        QuestsFunctions();
    }

    public void QuestsFunctions()
    {
        int currentLvl = myPlayerHealthXpSystem.GetCurrentLevel();

        switch (currentLvl)
        {
            case 1:
                if(currentKillQuestCounter >= 8)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
            case 2:
                if (currentKillQuestCounter >= 5)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
            case 3:
                if (currentKillQuestCounter >= 15)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
            case 4:
                if (currentKillQuestPickup)
                {
                    currentQuestCompleted = true;
                    currentKillQuestPickup = false;
                }
                break;
            case 5:
                if (currentKillQuestCounter >= 20)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
            case 6:
                if (currentKillQuestCounter >= 3)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
            case 7:
                if (currentKillQuestPickup)
                {
                    currentQuestCompleted = true;
                    currentKillQuestPickup = false;
                }
                break;
            case 8:
                if (currentKillQuestCounter >= 1)
                {
                    currentQuestCompleted = true;
                    currentKillQuestCounter = 0;
                }
                break;
        }
    }

    public string[] GetQuests()
    {
        return quests;
    }

    public bool GetCurrentQuestCompleted()
    {
        return currentQuestCompleted;
    }

    public void SetCurrentQuestCompleted(bool b)
    {
        currentQuestCompleted = b;
    }

    public void SetCurrentQuestPickup(bool b)
    {
        currentKillQuestPickup = b;
    }    
    public void IncreaseCurrentKillQuestCounter()
    {
        ++currentKillQuestCounter;
    }

}
