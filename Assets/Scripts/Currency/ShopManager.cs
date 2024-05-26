using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	public CoinManager coinManager;
	public ShopItem[] shopItems;

	private void Start()
	{
		if (coinManager != null)
		{
			coinManager.onCoinCountChanged += UpdateShopUI;
			UpdateShopUI(coinManager.GetCoinCount());
		}
	}

	private void UpdateShopUI(int coinBalance)
	{
		foreach (ShopItem item in shopItems)
		{
			Color color = item.itemImage.color;
			color.a = coinBalance >= item.cost ? 1f : 0.2f; // 1f for full opacity, 0.2f for reduced opacity
			item.itemImage.color = color;
		}
	}

	public void OnItemClick(int index)
	{
		if (index < 0 || index >= shopItems.Length) return;

		ShopItem item = shopItems[index];
		if (coinManager.GetCoinCount() >= item.cost)
		{
			coinManager.DecrementCoins(item.cost);
			PlayerStats.Instance.IncreaseStat(item.statGameObject, item.statIncrease);
		}
		else
		{
			Debug.Log("Not enough coins to purchase this item.");
		}
	}

	private void OnDestroy()
	{
		if (coinManager != null)
		{
			coinManager.onCoinCountChanged -= UpdateShopUI;
		}
	}
}