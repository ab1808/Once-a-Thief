using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void IncreaseStat(GameObject statGameObject, int amount)
	{
		Text statText = statGameObject.GetComponent<Text>();
		if (statText != null)
		{
			int currentStatValue = int.Parse(statText.text);
			currentStatValue += amount;
			statText.text = currentStatValue.ToString();
			Debug.Log("Stat increased by: " + amount + ". New stat: " + currentStatValue);
		}
		else
		{
			Debug.LogWarning("No Text component found on stat GameObject.");
		}
	}
}