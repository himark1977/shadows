using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    private Vector3 playerVelocity;
    private bool grounded;
    public float playerSpeed =2f;
    private float gravity = -9.81f;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        if (grounded && playerSpeed < 0)
        {
           playerVelocity.y = 0;
        }

        Vector3 movechar = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        controller.Move(movechar * playerSpeed * Time.deltaTime);
        animator.SetFloat("Speed", movechar.magnitude);
        if (movechar != Vector3.zero)
        {
            transform.forward = movechar;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Mouse0) && grounded)
        {
            Attack();
        }
    }


    void Attack() {
        animator.SetTrigger("attack");
    }

}
