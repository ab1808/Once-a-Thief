using UnityEngine;

	public class CoinManager : MonoBehaviour
	{
		private int coinCount;

		// Event to update the UI or any other listeners
		public delegate void OnCoinCountChanged(int newCoinCount);
		public event OnCoinCountChanged onCoinCountChanged;

		private void Start()
		{
			DontDestroyOnLoad(gameObject);
			coinCount = 1000; // Initial coin count
			onCoinCountChanged?.Invoke(coinCount);
		}

		public void IncrementCoins(int amount)
		{
			coinCount += amount;
			onCoinCountChanged?.Invoke(coinCount);
		}

		public void DecrementCoins(int amount)
		{
			if (coinCount - amount >= 0)
			{
				coinCount -= amount;
				onCoinCountChanged?.Invoke(coinCount);
			}
			else
			{
				Debug.LogWarning("Not enough coins to decrement");
			}
		}

		public int GetCoinCount()
		{
			return coinCount;
		}
	}