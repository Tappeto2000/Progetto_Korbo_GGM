using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIV1 : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform plr;

    public LayerMask IsGround, IsPlr;

    public int HP = 3;

    //Searching Player
    public Vector3 WalkPoint;
    bool IsWalkPointSet;
    public float WalkRange = 25f;

    //ATTACK!!!
    public float cooldown = 5f;
    bool hasAttacked;

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

        if (!isPlrInSightRange && !isPlrInAtkRange) SearchingTime();
        if (isPlrInSightRange && !isPlrInAtkRange) ChaseTime();
        if (isPlrInSightRange && isPlrInAtkRange) AttackTime();


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

        if(Physics.Raycast(WalkPoint, -transform.up, IsGround)) IsWalkPointSet = true;
    }

    private void ChaseTime()
    {
        agent.SetDestination(plr.position);
    }


    private void AttackTime()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(plr);

        if (!hasAttacked)
        {
            /*
            Will add attack code in the future! The future is now.
            Btw this does NOT WORK AS INTENDED. But hey,
            it actually works better than intended, so yeah :3
             */

            hasAttacked = true;
            
            Invoke(nameof(ResetAttack), cooldown);
        }

    }

    //NON FUNZIONA WHYYY
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Plr") {
            var healthComponent = collision.collider.GetComponent<PlrHealthSystem>();
            if (healthComponent != null)
            {
                healthComponent.TakeDmg(1);
            }
        }   
    }
    private void ResetAttack()
    {
        hasAttacked = false;
    }

    public void TakeDmg(int dmg)
    {
        HP -= dmg;

        if(HP <= 0) DestroyEnemy();

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}