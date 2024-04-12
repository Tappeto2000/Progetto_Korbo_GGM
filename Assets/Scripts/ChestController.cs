using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public float activationDistance;
    public Animator anim;


    float Distance;

    GameObject plr;

    void Awake()
    {
        isOpen = false;
        plr = GameObject.FindGameObjectWithTag("Plr");
        anim = plr.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(this.transform.position, plr.transform.position);
        if (Input.GetKeyDown("e") && Distance <= activationDistance)
        {
            if(isOpen == false)
            {
                anim.enabled = true;
                anim.Play("Scene");
                isOpen = true;
            }
        }
    }
}
