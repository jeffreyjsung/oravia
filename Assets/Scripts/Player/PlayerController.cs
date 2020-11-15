using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region playerVariables
    // the player for us to work with
    Player birdPlayer;
    Rigidbody birdBody;
    RigidbodyConstraints originalConstraints;
    #endregion

    #region movementVariables
    public float moveSpeed;

    float xInput;
    float yInput;
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float leftBorder;
    [SerializeField]
    private float rightBorder;
    #endregion

    #region attackVariables
    public float Damage;
    public float attackRange;
    private bool isAttacking;

    public float totalMana;
    private float currMana;
    public Slider ManaBar;
    public float peckCooldown;
    public float dashCooldown;
    // The following booleans could replace isAttacking later
    public float peckManaCost;
    public float dashManaCost;
    private bool isPeckReady;
    private bool isDashReady;
    #endregion

    // Uncomment this section when animations are ready
    #region Animations
    //Animator pecking;
    //Animator dashing;
    #endregion

    #region unityFunctions
    // Initialize the bird and relevant variables
    private void Awake()
    {
        birdPlayer = GetComponent<Player>();
        birdBody = GetComponent<Rigidbody>();
        originalConstraints = birdBody.constraints;
        isAttacking = false;

        /*
        pecking = GetComponent<Animator>();
        dashing = GetComponent<Animator>();
        */ 
    }

    // Might need an Update() function? Implement later for when bird does a certain action
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (birdBody.position.y >= maxHeight && yInput > 0
            || birdBody.position.y <= minHeight && yInput < 0)
        {
            yInput = 0f;
        } 
        if (birdBody.position.x <= leftBorder && xInput < 0
            || birdBody.position.x >= rightBorder && xInput > 0)
        {
            xInput = 0f;
        } 
        
        Move();
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(PeckAttack());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking)
        {
            StartCoroutine(DashAttack());
        }
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            chooseAttack();
        }
        */
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                Debug.Log(col.transform.name);
                if (col.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Attacking BasicEnemy");
                    col.transform.GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
        }
        isAttacking = false;
    }


    /*
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            birdBody.isKinematic = true;
            birdBody.constraints = RigidbodyConstraints.FreezeAll;
            float timeInvincible = 2.5f;
            Invoke("InvincibleFrames", timeInvincible);
        }
    }

    private void InvincibleFrames()
    {
        birdBody.isKinematic = false;
        birdBody.constraints = originalConstraints;
    }
    */

    #endregion

    #region movementFunctions
    private void Move()
    {
        Vector3 movement_vector = new Vector3(xInput, yInput);
        movement_vector = movement_vector.normalized;
        birdBody.MovePosition(birdBody.position + movement_vector * Time.deltaTime * moveSpeed);
    }
    #endregion

    #region attackFunctions

    IEnumerator PeckAttack()
    {
        isAttacking = true;

        // Play pecking animation
        // pecking.SetTrigger("Pecking trigger");

        yield return new WaitForSeconds(peckCooldown);
        Debug.Log("Peck Attack Cooldown: " + peckCooldown.ToString());

        isAttacking = false;
    }
    
    IEnumerator DashAttack()
    {
        isAttacking = true;

        // Play dashing animation
        // dashing.SetTrigger("Dashing trigger");
        float dashDistance = 5f;

        Vector3 movement_vector = new Vector3(xInput, yInput);
        movement_vector = movement_vector.normalized;
        birdBody.AddForce(movement_vector * dashDistance * moveSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("Dash Attack Cooldown: " + dashCooldown.ToString());

        isAttacking = false;
    }
    
    /*
    // chooseAttack might have parameters to set which elemental attack the bird will have equipped
    private void chooseAttack()
    {
        Debug.Log("Switching mode of attack!");
    }
    */
    #endregion

}
