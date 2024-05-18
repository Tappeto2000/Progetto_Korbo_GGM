using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyCollisionDetection : MonoBehaviour
{
    public BossAnimController ba;

    //bool cont = false;

    public Stats Player;
    public Stats Boss;

    void OnTriggerEnter(Collider other)
    {
        //if (cont)
        //{
            if (other.tag == "Plr" && ba.hasAttacked)
            {
                // other.GetComponent<Animator>().SetTrigger("Hit");
                Boss.DealDamage(Player.gameObject);
            }
           // cont = false;
       // }
    }
    //cont = true;
}
