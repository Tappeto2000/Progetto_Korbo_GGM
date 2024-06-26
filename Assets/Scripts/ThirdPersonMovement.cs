using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject Corpo;
    public GameObject Player;
    public Stats Plr;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public static bool death = false;

    public float deathCooldown = 3f;

    public float jumpForce = 6f;
    public float gravity = -9.81f;

    private Vector3 playerVelocity;


    //Sound thingies
    public AudioClip[] footstepSounds;
    public float minTimeBetweenFootsteps = 0.3f;
    public float maxTimeBetweenFootsteps = 0.6f;

    public AudioSource audioSource;
    private float timeSinceLastFootstep;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!death)
        {
            bool isGrounded = controller.isGrounded;

            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                Animator anim = Corpo.GetComponent<Animator>();
                anim.SetTrigger("Move");
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if(Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
                {
                    AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
                    audioSource.PlayOneShot(footstepSound);

                    timeSinceLastFootstep = Time.time;
                }

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move((moveDir.normalized * speed + playerVelocity) * Time.deltaTime);
            }
            else if (!isGrounded)
            {
                // If there's no movement input and the player is not grounded, allow lateral movement
                Vector3 downMove = new Vector3(0f, gravity, 0f).normalized;
                controller.Move(downMove * speed * Time.deltaTime);
            }
            else
            {
                Animator anim = Corpo.GetComponent<Animator>();
                anim.ResetTrigger("Move");
                anim.SetTrigger("Stop");
            }

            // Applying gravity
            playerVelocity.y += gravity * Time.deltaTime;

            // Jumping
            if ((isGrounded || Mathf.Abs(playerVelocity.y) < 0.001f) && Input.GetButtonDown("Jump"))
            {
                playerVelocity.y = jumpForce;
            }

            if (Plr.Hp <= 0)
            {
                death = true;
                Invoke(nameof(Death), deathCooldown);
            }
        } 
    }

    private void Death()
    {
        Destroy(Player);
    }
}