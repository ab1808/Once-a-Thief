using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public string playerName;
    public float health = 100;
    public Slider healthBar;

    public Animator anim;
    public Controller controller;

    public AudioSource hurtSound;
    public AudioSource deathSound;
    void Start()
    {
        health = 100;
        healthBar.value = health / 100;
    }
    
    public void UpdatePlayerName(string name)
    {
        playerName = name;
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            deathSound.Play();
            anim.SetTrigger("Death");
            controller.enabled = false;
            healthBar.value = health / 100;
            Invoke("PlayerDeath", 3f);

        }
        else
        {
            hurtSound.Play();
            healthBar.value = health / 100;
            anim.SetTrigger("Hurt");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
