using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject promptUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "potionMaker" || other.gameObject.tag == "treasureChest")
        {
            promptUI.SetActive(true);
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
