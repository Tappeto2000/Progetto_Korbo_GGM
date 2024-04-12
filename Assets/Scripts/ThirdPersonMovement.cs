using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject Corpo;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float jumpForce = 6f;
    public float gravity = -9.81f;

    private Vector3 playerVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
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
    }
}