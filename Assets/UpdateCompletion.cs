using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCompletion : MonoBehaviour
{
    public QuestManager questManager;
    Text text;
    public QuestSO questSO;
    private QuestType type;
    void Start()
    {
        text = GetComponent<Text>();
        type = questSO.type;
    }

    private void Update()
    {
        
    }
    public void UpdateCompletionText()
    {
        var completionStorylvl = questManager.questStorySOs[questManager.currentStoryQuest].completionLevel;
        if (type == QuestType.Story)
        {
            text.text = completionStorylvl.ToString() + "%";

            //update ui text to red or green
            if(completionStorylvl >= 100)
            {
                questManager.CompleteQuest(type);
                text.color = Color.green;
            }
            else
            {
                text.color = Color.red;
            }
        }
        
        else if (type == QuestType.Side)
        {
            var completionSidelvl = questManager.questSideSOs[questManager.currentSideQuest].completionLevel;
            text.text = completionSidelvl.ToString() + "%";

            //update ui text to red or green
            if (completionSidelvl >= 100)
            {
                questManager.CompleteQuest(type);
                text.color = Color.green;
            }
            else
            {
                text.color = Color.red;
            }
        }
    }
}
