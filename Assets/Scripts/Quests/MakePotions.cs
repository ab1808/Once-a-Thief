using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePotions : MonoBehaviour
{
    public PlayerInteractions playerInteractions;
    public AudioSource potionAudio;
    public InventoryManager inventoryManager;
    //subtract inventory

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && 
            (inventoryManager.hotbarItems.ContainsKey("food") || inventoryManager.backpackItems.ContainsKey("food"))
            && Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 3f)
        {
            if((inventoryManager.hotbarItems["food"] >= 2 || inventoryManager.backpackItems["food"] >= 2))
            {
                CreatePotion();

                inventoryManager.RemoveItem("food");
                inventoryManager.RemoveItem("food");
            }

        }
 
    }

    private void CreatePotion()
    {
        playerInteractions.promptUI.SetActive(false);
        //decrement inventory
        
        potionAudio.Play();
        //show particle effects
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Invoke("disablePotion", 5);
    }

    private void disablePotion()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //add potion to inventory
        inventoryManager.AddItem("potion");
    }




}
