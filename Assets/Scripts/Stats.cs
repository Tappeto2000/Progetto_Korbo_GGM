using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Hp;
    public int Dmg;
    public GameObject Thing;

    public void TakeDamage(int damage)
    {
            Hp -= damage;
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<Stats>();
        if (atm != null)
        {
            atm.TakeDamage(Dmg);
        }
    }

    void Update()
    {
        /*if (Hp <= 0)
        {
            
            Destroy(Thing);
        } */
    }       
}
