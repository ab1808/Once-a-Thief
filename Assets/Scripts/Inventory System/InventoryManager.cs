using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType
{
    weapon,
    food,
    potion
}
public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, int> hotbarItems = new Dictionary<string, int> { };
    public Dictionary<string, int> backpackItems = new Dictionary<string, int> { };

    public List<Sprite> icons;

    public int hotbarCount = 5;
    public int backpackCount = 35;

    public List<GameObject> hotbarSlots;
    public List<GameObject> backpackSlots;

    public int currentHotbarCount = 0;
    public int currentBackpackCount = 0;

    private string currentItem;

    private int tempSlot;

    public void SwitchItemToBackPack()
    {
        Debug.Log("switch to backpack");
        currentItem = checkHotbarButtonItem();
        var x = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform;
        //check which item
        if (backpackItems.Count < backpackCount)
        {
            backpackItems.Add(currentItem, hotbarItems[currentItem]);

            x.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "0";
            x.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[0];
            hotbarItems.Remove(currentItem);

            currentBackpackCount++;
            currentHotbarCount--;

            switch (currentItem)
            {
                case "health potion":
                    backpackSlots[currentBackpackCount-1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[1];
                    
                    break;
                case "sword":
                    backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[2];
                    break;
                case "food":
                    backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[3];
                    break;
                case "shield":
                    backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[4];
                    break;
                case "speed potion":
                    backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[5];
                    break;
                case "jump potion":
                    backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[6];
                    break;
            }

            backpackSlots[currentBackpackCount - 1].transform.GetChild(2).gameObject.GetComponent<Text>().text = backpackItems[currentItem].ToString();
        }
        //else do nothing
    }

    public void SwitchItemToHotbar()
    {
        Debug.Log("switch to hotbar");
        currentItem = checkBackpackButtonItem();
        if(hotbarItems.Count < hotbarCount)
        {
            //add and remove items to dicts
            hotbarItems.Add(currentItem, backpackItems[currentItem]);
            backpackItems.Remove(currentItem);

            //update ui image for backpack
            var x = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform;
            x.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[0];
            x.transform.GetChild(2).gameObject.GetComponent<Text>().text = "0";
            backpackItems.Remove(currentItem);

            //update counts
            currentBackpackCount--;
            currentHotbarCount++;

            //update ui on hotbar
            switch (currentItem)
            {
                case "health potion":
                    hotbarSlots[currentHotbarCount-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[1];
                    break;
                case "sword":
                    hotbarSlots[currentHotbarCount-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[2];
                    break;
                case "food":
                    hotbarSlots[currentHotbarCount-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[3];
                    break;
                case "shield":
                    hotbarSlots[currentHotbarCount-1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[4];
                    break;
                case "speed potion":
                    hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[5];
                    break;
                case "jump potion":
                    hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[6];
                    break;
            }
            //update number on hotbar
            hotbarSlots[currentHotbarCount - 1].transform.GetChild(2).GetChild(0).GetComponent<Text>().text = hotbarItems[currentItem].ToString();
        }
        //else do nothing
    }
    //check item name, then check if it exists in dictionary, then
    //check if hotbar has slots left, if it does update
    //the hotbar otherwise update backpack if both are full, then dont pick
    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;

        currentItem = CheckItem(tag);
        if(currentItem != ""){
            AddItem(currentItem);
        }
        
        
    }
    private string CheckItem(string objectTag)
    {
        switch (objectTag)
        {
            case "health potion":
                currentItem = "potion";
                return currentItem;
            case "sword":
                currentItem = "sword";
                return currentItem;
            case "food":
                currentItem = "food";
                return currentItem;
            case "shield":
                currentItem = "shield";
                return currentItem;
            case "speed potion":
                currentItem = "speed potion";
                return currentItem;
            case "jump potion":
                currentItem = "jump potion";
                return currentItem;
        }

        return "";
    }

    public void RemoveItem(string currentItem)
    {
        Debug.Log("remove irem");
        if (hotbarItems.ContainsKey(currentItem))
        {
            //increment hotbar
            hotbarItems[currentItem] -= 1;

            for (int i = 0; i < hotbarSlots.Count; i++)
            {

                if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[1] && currentItem == "health potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[2] && currentItem == "sword")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[3] && currentItem == "food")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[4] && currentItem == "shield")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[5] && currentItem == "speed potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[6] && currentItem == "jump potion")
                {
                    tempSlot = i;
                    break;
                }



            }
            var x = hotbarSlots[tempSlot].transform;

            if (hotbarItems[currentItem] <= 0)
            {
                x.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "0";
                x.GetChild(0).GetComponent<Image>().sprite = icons[0];
                currentHotbarCount--;
                hotbarItems.Remove(currentItem);
            }

            x.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = hotbarItems[currentItem].ToString();

        }
        else if (backpackItems.ContainsKey(currentItem))
        {
            //increment backpack
            backpackItems[currentItem] -= 1;

            for (int i = 0; i < hotbarSlots.Count; i++)
            {

                if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[1] && currentItem == "health potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[2] && currentItem == "sword")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[3] && currentItem == "food")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[4] && currentItem == "shield")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[5] && currentItem == "speed potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[6] && currentItem == "jump potion")
                {
                    tempSlot = i;
                    break;
                }

            }
            var x = backpackSlots[tempSlot].transform;
            if (backpackItems[currentItem]  <= 0)
            {
                x.GetChild(2).gameObject.GetComponent<Text>().text = "0";
                x.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[0];
                currentBackpackCount--;
                backpackItems.Remove(currentItem);
            }
            else
            {
                x.GetChild(2).gameObject.GetComponent<Text>().text = backpackItems[currentItem].ToString();
            }

        }

        
    }

    public void AddItem(string currentItem)
    {
        Debug.Log(" add item");
        if(hotbarItems.ContainsKey(currentItem))
        {
            //increment hotbar
            hotbarItems[currentItem] += 1;

            for (int i = 0; i < hotbarSlots.Count; i++)
            {

                if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[1] && currentItem == "health potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[2] && currentItem == "sword")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[3] && currentItem == "food")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[4] && currentItem == "shield")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[5] && currentItem == "speed potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (hotbarSlots[i].transform.GetChild(0).GetComponent<Image>().sprite == icons[6] && currentItem == "jump potion")
                {
                    tempSlot = i;
                    break;
                }
            }
            var x = hotbarSlots[tempSlot].transform.GetChild(2);
            x.GetChild(0).gameObject.GetComponent<Text>().text = hotbarItems[currentItem].ToString();
        }

        else if (hotbarItems.ContainsKey(currentItem))
        {
            //increment backpack
            backpackItems[currentItem] += 1;

            for (int i = 0; i < hotbarSlots.Count; i++)
            {

                if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[1] && currentItem == "health potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[2] && currentItem == "sword")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[3] && currentItem == "food")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[4] && currentItem == "shield")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[5] && currentItem == "speed potion")
                {
                    tempSlot = i;
                    break;
                }

                else if (backpackSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == icons[6] && currentItem == "jump potion")
                {
                    tempSlot = i;
                    break;
                }


            }
            var x = backpackSlots[tempSlot].transform;
            x.GetChild(2).gameObject.GetComponent<Text>().text = hotbarItems[currentItem].ToString();
        }

        else
        {
            if (hotbarItems.Count < hotbarCount)
            {
                //add to hotbar
                hotbarItems.Add(currentItem, 1);
                currentHotbarCount++;

                switch (currentItem)
                {
                    case "health potion":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[1];
                        break;
                    case "sword":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[2];
                        break;
                    case "food":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[3];
                        break;
                    case "shield":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[4];
                        break;
                    case "speed potion":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[5];
                        break;
                    case "jump potion":
                        hotbarSlots[currentHotbarCount - 1].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = icons[6];
                        break;
                }

                hotbarSlots[currentHotbarCount-1].transform.GetChild(2).GetChild(0).GetComponent<Text>().text = hotbarItems[currentItem].ToString();

            }

            else if (backpackItems.Count < backpackCount)
            {
                //add to backpack
                backpackItems.Add(currentItem, 1);
                currentBackpackCount++;

                switch (currentItem)
                {
                    case "health potion":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[1];

                        break;
                    case "sword":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[2];
                        break;
                    case "food":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[3];
                        break;
                    case "shield":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[4];
                        break;
                    case "speed potion":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[5];
                        break;
                    case "jump potion":
                        backpackSlots[currentBackpackCount - 1].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = icons[6];
                        break;
                }

                backpackSlots[currentBackpackCount-1].transform.GetChild(2).gameObject.GetComponent<Text>().text = backpackItems[currentItem].ToString();
            }

            else
            {
                //do nothing
            }
        }
    }

    public string checkHotbarButtonItem()
    {
        var x = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform;

            if (x.GetChild(0).GetComponent<Image>().sprite == icons[1])
            {
            return "health potion";
            }

            else if (x.transform.GetChild(0).GetComponent<Image>().sprite == icons[2])
            {
            return "sword";
            }

            else if (x.GetChild(0).GetComponent<Image>().sprite == icons[3])
            {
                return "food";
            }

            else if (x.GetChild(0).GetComponent<Image>().sprite == icons[4])
            {
            return "shield";
            }

            else if (x.GetChild(0).GetComponent<Image>().sprite == icons[5])
            {
                return "speed potion";
            }

            else if (x.GetChild(0).GetComponent<Image>().sprite == icons[6])
            {
                return "jump potion";
            }
            else
            {
                return "";
            }
            
    }

    public string checkBackpackButtonItem()
    {
        var x = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform;

        if (x.GetChild(1).GetComponent<Image>().sprite == icons[1])
        {
            return "health potion";
        }

        else if (x.transform.GetChild(1).GetComponent<Image>().sprite == icons[2])
        {
            return "sword";
        }

        else if (x.GetChild(1).GetComponent<Image>().sprite == icons[3])
        {
            return "food";
        }

        else if (x.GetChild(1).GetComponent<Image>().sprite == icons[4])
        {
            return "shield";
        }

        else if (x.GetChild(1).GetComponent<Image>().sprite == icons[5])
        {
            return "speed potion";
        }

        else if (x.GetChild(1).GetComponent<Image>().sprite == icons[6])
        {
            return "jump potion";
        }
        else
        {
            return "";
        }
    }
}
