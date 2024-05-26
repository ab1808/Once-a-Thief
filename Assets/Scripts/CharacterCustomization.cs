using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public string gender;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SelectFemale()
    {
        gender = "female";
    }
    
    public void SelectMale()
    {
        gender = "male";
    }
}
