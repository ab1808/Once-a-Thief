using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public Slider healthBar;

    void Start()
    {
        health = 100;
        healthBar.value = health / 100;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(health > 0)
            {
                health = health - 10;
                healthBar.value = health / 100;
            }
            else
            {
                PlayerDeath();
            }
            
        }
    }

    void PlayerDeath()
    {
        ResetHealth();
    }
    void ResetHealth()
    {
        health = 100;
        healthBar.value = health / 100;
    }
}
