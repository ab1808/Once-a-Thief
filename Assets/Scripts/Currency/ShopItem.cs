using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
	public Image itemImage;
	public int cost;
	public GameObject statGameObject; // GameObject that contains a Text component for the stat
	public int statIncrease;
}