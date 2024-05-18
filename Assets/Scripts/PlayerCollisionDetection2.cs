using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerCollisionDetection2 : MonoBehaviour
{
    public WeaponController wc;

    public Stats BBoss;
    public Stats Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BossWeapon" && wc.IsAttacking)
        {
            if (BossAnimController.death == false) other.GetComponent<Animator>().SetTrigger("Hit");
            Player.DealDamage(BBoss.gameObject);
        }

    }
}