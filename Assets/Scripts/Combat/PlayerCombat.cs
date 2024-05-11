using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    [SerializeField] float timeBetweenAttacks = 0.2f;
    [SerializeField] float timeBetweenCombos = 0.2f;
    Animator anim;
    [SerializeField] Weapon weapon;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Attack();
        }
        ExitAttack();
    }

    void Attack(int i)
    {
        if (Time.time - lastComboEnd > timeBetweenCombos)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= timeBetweenAttacks)
            {
                anim.runtimeAnimatorController = combo[i].animatorOV;
                anim.Play("Attack", 0);
                weapon.damage = combo[i].damage;
                lastClickedTime = Time.time;
            }
        }
    }

    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        lastComboEnd = Time.time;
    }

    public void OnLightAttack()
    {
        Attack(0);
    }

    public void OnHeavyAttack()
    {
        Attack(1);
    }

    public void OnDefend()
    {
        Attack(2);
    }
}
