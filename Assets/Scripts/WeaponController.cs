using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = false;
    public float AttackCooldown = 0.5f;
    public bool IsAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }

    /*
     * Non va RAAAAAAH!!!
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoint = collision.contacts;
        if (!CanAttack)
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                if (contactPoint)
                {

                }
            }
        }
    }*/
}
