using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonCount : MonoBehaviour
{
    [HideInInspector] public int chestsCollected;
    private int totalChests = 10;
    public QuestManager questManager;
    void Start()
    {

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            chestsCollected = 0;
        }
    }
}
