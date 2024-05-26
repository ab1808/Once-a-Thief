using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGender : MonoBehaviour
{
    public CharacterCustomization custom;
    void Start()
    {
        custom = GameObject.FindGameObjectWithTag("gender").GetComponent<CharacterCustomization>();

        if(custom.gender == "female")
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }

}
