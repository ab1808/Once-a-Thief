using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
	public string playerName;
	public float playerHealth;
	public int currentStoryQuest;
	public int currentSideQuest;
	public int storyQuestProgress;	
	public int sideQuestProgress;
	
	public PlayerData(PlayerHealth player, QuestManager questManager)
	{
		playerName = player.playerName;
		playerHealth = player.health;
		currentStoryQuest = questManager.currentStoryQuest;
		currentSideQuest = questManager.currentSideQuest;
		storyQuestProgress = questManager.storyQuestProgress;
		sideQuestProgress = questManager.sideQuestProgress;
	}
	

}
