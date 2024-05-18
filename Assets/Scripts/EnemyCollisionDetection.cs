using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BossCollisionDetection : MonoBehaviour
{
    public EnemyAnimController ea;

    public static bool updateHealth = false;

    public Stats Player;
    public Stats Enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plr" && ea.hasAttacked)
        {
           // other.GetComponent<Animator>().SetTrigger("Hit");
            Enemy.DealDamage(Player.gameObject);

        }

    }
}
