using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    // Character Movement
    public CharacterController controller;  
    public Animator animator;
    private Vector3 playerVelocity;
    private bool grounded;
    public float playerSpeed =2f;
    private float gravity = -9.81f;
    //Player Stats
    public int playerHealth;
    public int ammo;
    public int maxAmmo;
    public int damage;
    // UI
    public Slider healthBar;
    //Spawn Bullet TODO See function DestroyBullet
    public Rigidbody bullet;
    public Transform spawnPoint;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        playerHealth = 100;
        ammo = 30;
        maxAmmo = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0 ){
           StartCoroutine(Death());
        }
        //Add to UI
        healthBar.value = playerHealth;
        
        grounded = controller.isGrounded;
        if (grounded && playerSpeed < 0)
        {
           playerVelocity.y = 0;
        }

        Vector3 movechar = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")).normalized;
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
                animator.SetBool("Rifle_Equip", true);
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
            if (ammo == 0)
            {
                ammo = 30; // minim ammo
                maxAmmo = maxAmmo - 30; // Ammo - 30
            }
            if (maxAmmo == 0) 
            {
                Debug.Log("Dang!"); // replace with sound
                animator.SetBool("Rifle_Equip", false);
            }
            else
            {
                ammo--;
                Rigidbody bulletInstance;
                bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation) as Rigidbody; // This is where I don't understand why?!?!
                bulletInstance.AddForce(spawnPoint.forward * 300f);
            }
        } else {
                animator.SetTrigger("attack");
            }
    }

 IEnumerator Death()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
