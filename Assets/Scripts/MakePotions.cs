using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePotions : MonoBehaviour
{
    public PlayerInteractions playerInteractions;
    public AudioSource potionAudio;
    //subtract inventory

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreatePotion();
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
    }




}
