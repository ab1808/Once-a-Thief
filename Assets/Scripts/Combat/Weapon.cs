using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    BoxCollider triggerBox;
    private PlayerCombat playerCombat;

    void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
        playerCombat = GetComponentInParent<PlayerCombat>(); // Get reference to PlayerCombat
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerCombat != null && playerCombat.IsAttacking())
        {
            var enemy = other.gameObject.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void EnableTrigger()
    {
        triggerBox.enabled = true;
    }

    public void DisableTrigger()
    {
        triggerBox.enabled = false;
    }
}
