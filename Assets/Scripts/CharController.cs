using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    private Vector3 playerVelocity;
    private bool grounded;
    public float playerSpeed =2f;
    private float gravity = -9.81f;
    public int playerHealth;
    public Slider healthBar;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
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

        if(Input.GetKeyDown(KeyCode.Q) && grounded)
        {
            if(animator.GetBool("Rifle_Equip") == false)
            {
                RifleEquip();
            } else{
                animator.SetBool("Rifle_Equip", false);
            }
        }
    
        // Debug Only
        if(Input.GetKeyDown(KeyCode.E) && grounded)
        {
            playerHealth -= 10;
        }
    
    
    
    }


    void Attack() {
        if(animator.GetBool("Rifle_Equip") == true)
            {
                Debug.Log("Rifle Attack");
            } else {
                animator.SetTrigger("attack");
            }
    }

    void RifleEquip(){
        animator.SetBool("Rifle_Equip", true);
    }


}
