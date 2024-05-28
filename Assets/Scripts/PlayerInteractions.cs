using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject promptUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "potionMaker" || other.gameObject.tag == "treasureChest")
        {
            promptUI.SetActive(true);
        }

        if(other.gameObject.tag == "nextScene")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "potionMaker" || other.gameObject.tag == "treasureChest")
        {
            promptUI.SetActive(false);
        }
    }

    
}
