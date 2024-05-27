using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHairColor : MonoBehaviour
{
    public CharacterCustomization custom;
    public Renderer objectRenderer;
    void Start()
    {
        custom = GameObject.FindGameObjectWithTag("hair").GetComponent<CharacterCustomization>();
        //objectRenderer.material = materials[index];
        objectRenderer.material = custom.hairMaterials[custom.selectedMaterialIndex];
    }

}
