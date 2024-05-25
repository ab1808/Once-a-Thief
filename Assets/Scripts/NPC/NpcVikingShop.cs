using UnityEngine;

namespace NPC
{
	public class NpcVikingShop : Npc
	{
		public GameObject shopUI;
		public override void Interact()
		{
			Debug.Log("Interacting with Viking Shop");
			//Open Shop UI
			shopUI.SetActive(true);
		}
	}
}