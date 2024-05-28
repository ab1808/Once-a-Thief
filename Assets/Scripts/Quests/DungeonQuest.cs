using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonQuest : MonoBehaviour
{
    public DungeonCount dungeonCount;
    public PlayerInteractions playerInteractions;
    public InventoryManager inventoryManager;
    public AudioSource chestOpenAudio;
    string item;
    private List<string> items = 
        new List<string> { "food", "shield", "food", "food", "sword", "health potion", "speed potion", "jump potion", "shield", "food"};

    private void Start()
    {
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && 
            Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 3f)
        {
            playerInteractions.promptUI.SetActive(false);

            item = items[Random.Range(0, items.Count)];
            inventoryManager.AddItem(item);
            item = items[Random.Range(0, items.Count)];
            inventoryManager.AddItem(item);

            chestOpenAudio.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            dungeonCount.chestsCollected++;
            this.enabled = false;
        }
    }

}
