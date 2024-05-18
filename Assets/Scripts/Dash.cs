using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashMovement : MonoBehaviour
{
    public CharacterController controller;

    public float dashSpeed = 40f; // Velocità del dash
    public float dashDuration = 0.2f; // Durata del dash
    private float dashCooldown = 1.5f; // Tempo di ricarica del dash
    private float lastDashTime; // Tempo dell'ultimo dash

    public GameObject Corpo;

    void Update()
    {
        // Dashing
        if (!ThirdPersonMovement.death) 
        {
            if (Time.time >= lastDashTime + dashCooldown && Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash());
                Animator anim = Corpo.GetComponent<Animator>();
                anim.SetTrigger("Dash");
            }
        }   
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
        lastDashTime = Time.time;
    }
}

