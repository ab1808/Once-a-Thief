using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Quests")]

public class QuestSO : ScriptableObject
{
    public string currentQuest;
    public bool isCompleted;
    public QuestType type;
    public int completionLevel;
}