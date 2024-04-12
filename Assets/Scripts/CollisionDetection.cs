using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public WeaponController wc;

    void OnTriggerEnter (Collider other){
        if(other.tag == "EnemyWeapon" && wc.IsAttacking)
        {
            other.GetComponent<Animator>().SetTrigger("Hit");

        }

    }

}
