using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public Text text;
    public List<Material> hairMaterials;

    public int selectedMaterialIndex;

    public string selectedMaterial;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        
    }

    private void Update()
    {
        UpdateHair();
    }
    public void UpdateHair()
    {
        selectedMaterial = text.text;

        if (selectedMaterial == "Red")
        {
            selectedMaterialIndex = 0;
        }
        else if(selectedMaterial == "White")
        {
            selectedMaterialIndex = 1;
        }
        else if (selectedMaterial == "Blonde")
        {
            selectedMaterialIndex = 2;
        }
        else
        {
            selectedMaterialIndex = 3;
        }
    }

    
}
