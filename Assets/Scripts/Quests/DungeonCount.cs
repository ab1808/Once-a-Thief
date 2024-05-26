using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCount : MonoBehaviour
{
    [HideInInspector] public int chestsCollected;
    private int totalChests = 10;
    public QuestManager questManager;
    void Start()
    {
        chestsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(chestsCollected < totalChests)
        {
            int progress = (chestsCollected *  100)/ totalChests;
            questManager.UpdateStoryLevelProgression(progress, QuestType.Side);
        }
        if(chestsCollected >= totalChests)
        {
            //quest complete
            questManager.UpdateStoryLevelProgression(100, QuestType.Side);
            chestsCollected = 0;
        }
    }
}
