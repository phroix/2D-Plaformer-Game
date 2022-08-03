using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySystem : MonoBehaviour
{
    private string[] quests = { 
        "Kill 4 Slimes and 4 Bees!",
        "Kill 5 Spiders!", 
        "Save the villagers and kill all enemys!", 
        "Find the golden key in cave2!", 
        "Kill 20 Normal Skeletons!",
        "Kill 3 Caster Skeletons!", 
        "Find the lost villager in the caves!", 
        "Kill the Boss!" };

    private bool currentQuestCompleted = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] GetQuests()
    {
        return quests;
    }


}
