using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public GameObject promptUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
        {
            promptUI.SetActive(true);
        }
    }
}
