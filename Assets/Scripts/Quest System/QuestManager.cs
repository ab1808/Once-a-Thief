using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestType
{
    Story,
    Side
}
public class QuestManager : MonoBehaviour
{
    [SerializeField] public List<QuestSO> questStorySOs;
    [SerializeField] public List<QuestSO> questSideSOs;
    [SerializeField] List<Text> questStoryTexts;
    [SerializeField] List<Text> questSideTexts;
    public int currentStoryQuest;
    public int currentSideQuest;

    public int sideQuestProgress;
    public int storyQuestProgress;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentStoryQuest = 0;
        currentSideQuest = 0;
        //initialize quest ui
    }

    void Update()
    {
        //dummy input
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateStoryLevelProgression(sideQuestProgress, QuestType.Side);
            UpdateStoryLevelProgression(storyQuestProgress, QuestType.Story);
        }
        
    }

    public void UpdateStoryLevelProgression(int progress, QuestType type)
    {
        if (type == QuestType.Story)
        {
            var txt = questStoryTexts[currentStoryQuest];
            questStorySOs[currentStoryQuest].completionLevel = progress;
            txt.text = progress.ToString() + "%";
            if(progress >= 100)
            {
                txt.color = Color.green;
                progress = 0;
                CompleteQuest(type);
            }
            else
            {
                txt.color = Color.red;
            }
        }
        else
        {
            var txt2 = questSideTexts[currentSideQuest];
            questSideSOs[currentSideQuest].completionLevel = progress;
            txt2.text = progress.ToString() + "%";
            if (progress >= 100)
            {
                txt2.color = Color.green;
                progress = 0;
                CompleteQuest(type);
            }
            else
            {
                txt2.color = Color.red;
            }
        }
        
    }

    public void CompleteQuest(QuestType type)
    {
        if (type == QuestType.Story)
        {
            questStorySOs[currentStoryQuest].isCompleted = true;
        }
        else if(type == QuestType.Side) 
        {
            questSideSOs[currentSideQuest].isCompleted = true;
        }
        
        IncrementQuest(type);
        //make changes in ui
    }
    void IncrementQuest(QuestType type)
    {
        //move up a quest
        if (type == QuestType.Story && currentStoryQuest < questStorySOs.Count-1 )
        {
            currentStoryQuest++;
        }
        else if(type == QuestType.Side && currentSideQuest < questSideSOs.Count - 1)
        {
            currentSideQuest++;
        }
    }
}
