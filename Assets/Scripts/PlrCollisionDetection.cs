using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlrCollisionDetection : MonoBehaviour
{
    public WeaponController wc;

    public Stats Enemy;
    public Stats Player;

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == "EnemyWeapon" && wc.IsAttacking)
        {
            if(EnemyAnimController.death == false) other.GetComponent<Animator>().SetTrigger("Hit");
            Player.DealDamage(other.gameObject);
        }

    }
}