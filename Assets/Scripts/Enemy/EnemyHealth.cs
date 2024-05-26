using NPCE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public Animator anim;
    public AudioSource deathSound;
    public AudioSource hurtSound;

    public bool isDead = false;
        
    private void Start()
    {
        enemyHealth = 100;
        isDead = false;
    }
    
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log(enemyHealth);
        if(enemyHealth <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
            Destroy(this.gameObject);
        }
        else
        {
            hurtSound.Play();
            anim.SetTrigger("Hurt");
        }
        
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
