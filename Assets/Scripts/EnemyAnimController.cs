using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimController : MonoBehaviour
{

    public GameObject Nemico;
    public Stats Enemy;

    public NavMeshAgent agent;

    public Transform plr;

    public LayerMask IsGround, IsPlr;

    public static int hp = 3;

    public static int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    //Searching Player
    public Vector3 WalkPoint;
    bool IsWalkPointSet;
    public float WalkRange = 25f;

    //ATTACK!!!
    public float cooldown = 0.2f;
    public float deathCooldown = 5f;
    public bool hasAttacked;
    public static bool death = false;
    
    //States
    public float SightRange = 50f;
    public float AttackRange = 5f;
    public bool isPlrInSightRange, isPlrInAtkRange;



    private void Start()
    {
        plr = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        //Check for ranges
        isPlrInSightRange = Physics.CheckSphere(transform.position, SightRange, IsPlr);
        isPlrInAtkRange = Physics.CheckSphere(transform.position, AttackRange, IsPlr);

        if (death == false)
        {
            if (!isPlrInSightRange && !isPlrInAtkRange) SearchingTime();
            if (isPlrInSightRange && !isPlrInAtkRange) ChaseTime();
            if (isPlrInSightRange && isPlrInAtkRange) AttackTime();
        }

        if (Enemy.Hp <= 0 && death == false)
        {
            Animator anim = Nemico.GetComponent<Animator>();
            anim.SetTrigger("Mortis");
            death = true;
            Invoke(nameof(Death), deathCooldown);
        }

    }

    private void SearchingTime()
    {
        if (!IsWalkPointSet) SearchWalkPoint();

        if (IsWalkPointSet) agent.SetDestination(WalkPoint);

        Vector3 DistanceToWalkPoint = transform.position - WalkPoint;

        if (DistanceToWalkPoint.magnitude < 1f) IsWalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkRange, WalkRange);
        float randomX = Random.Range(-WalkRange, WalkRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(WalkPoint, -transform.up, IsGround)) IsWalkPointSet = true;
    }

    private void ChaseTime()
    {
        Animator anim = Nemico.GetComponent<Animator>();
        agent.SetDestination(plr.position);
        anim.StopPlayback();
        StartCoroutine(ResetEnemyAtk("EnemyAtk"));
        transform.LookAt(plr);

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, 180, 0);
        transform.rotation = targetRotation; 
    }

    IEnumerator ResetEnemyAtk(string trigName)
    {
        yield return new WaitForSeconds(cooldown);

        Animator anim = this.GetComponent<Animator>();
        anim.ResetTrigger(trigName);
    }

    private void AttackTime()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(plr);

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = currentRotation * Quaternion.Euler(0, 180, 0);
        transform.rotation = targetRotation;

        if (!hasAttacked)
        {
            /*
            Will add attack code in the future! The future is now.
            Btw this does NOT WORK AS INTENDED. But hey,
            it actually works better than intended, so yeah :3
             */
            Animator anim = Nemico.GetComponent<Animator>();
            anim.SetTrigger("EnemyAtk");

          //  StartCoroutine(ResetEnemyAtk("EnemyAtk"));

            hasAttacked = true;
            Invoke(nameof(ResetAttack), cooldown);
        }

    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    private void Death()
    {
        death = false;
        Destroy(Nemico);
    }
}



    //NON FUNZIONA WHYYY
    /* private void OnCollisionEnter(Collision collision)
     {
         if (collision.collider.tag == "Plr")
         {
             var healthComponent = collision.collider.GetComponent<PlrHealthSystem>();
             if (healthComponent != null)
             {
                 healthComponent.TakeDmg(1);
             }
         }
     }

     public void TakeDmg(int dmg)
     {
         HP -= dmg;

         if (HP <= 0) DestroyEnemy();

     }

     private void DestroyEnemy()
     {
         Destroy(gameObject);
     } */
     