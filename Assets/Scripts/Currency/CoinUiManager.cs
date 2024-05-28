using UnityEngine;
using TMPro;


	public class CoinUiManager : MonoBehaviour
	{
		public CoinManager coinManager;
		public TextMeshProUGUI coinText;

		void Start()
		{
			DontDestroyOnLoad(gameObject);
			if (coinManager != null)
			{
				coinManager.onCoinCountChanged += UpdateCoinUI;
			}
		}

		void UpdateCoinUI(int newCoinCount)
		{
			coinText.text = newCoinCount.ToString();
		}

		void OnDestroy()
		{
			if (coinManager != null)
			{
				coinManager.onCoinCountChanged -= UpdateCoinUI;
			}
		}
	}