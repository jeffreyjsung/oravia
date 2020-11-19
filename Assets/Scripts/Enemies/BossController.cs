using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : Enemy
{
    #region Boss Variables
    //[SerializeField]
    //[Tooltip("The type of boss.")]
    //private Enemy bossEnemy;

    //[SerializeField]
    //[Tooltip("A list of this boss' attacks.")]
    //private Attack[] attacks;

    [SerializeField]
    [Tooltip("The amount of time to wait before attacking (must be less than Frozen Time).")]
    private float attackWaitTime;

    [SerializeField]
    [Tooltip("The amount of time this boss is frozen to attack (must be longer than the attack animation).")]
    private float frozenTime;

    [SerializeField]
    [Tooltip("The cooldown time of this attack.")]
    private float cooldownTime;

    [SerializeField]
    [Tooltip("The range of this boss' attack.")]
    private float attackRange;

    [SerializeField]
    [Tooltip("Where to spawn the attack with respect to the boss.")]
    private Vector3 attackOffset;

    [SerializeField]
    [Tooltip("The number of times this boss can attack in one position.")]
    private int attacksPerPosition;

    [SerializeField]
    [Tooltip("The positions this boss can move to.")]
    private Vector3[] movePositions;

    [SerializeField]
    [Tooltip("The health bar of this boss.")]
    private Slider HPBar;

    [SerializeField]
    [Tooltip("The bounds of this boss.")]
    private Vector3[] bounds;

    private float attackWaitTimer;

    private float frozenTimer;

    //private float cooldownTimer;

    private Rigidbody bossRigidbody;

    //private Enemy bossEnemy;

    private float attackTime;

    private bool isAttacking = false;

    private int numAttackSoFar = 0;

    private bool attacked = false;

    private ParticleSystem attackEffect;

    private int moveIndex = 0;

    private Animator animator;
    #endregion

    #region Unity Functions
    // Initialize the boss and relevant variables
    public override void Awake()
    {
        base.Awake();
        //GetComponent<Renderer>().material.color = Color.white;
        bossRigidbody = GetComponent<Rigidbody>();
        //bossEnemy = GetComponent<Enemy>();
        frozenTimer = frozenTime;
        attackWaitTimer = attackWaitTime;
        //cooldownTimer = cooldownTime;
        attackTime = 0.6f;
        HPBar.value = currHealth / totalHealth;
        //boss = Instantiate<Enemy>(bossEnemy, movePositions[i], Quaternion.identity);
        //attackEffect = GetComponentInChildren<ParticleSystem>();
        //attackEffect.Stop();
        animator = GetComponent<Animator>();
    }

    //private void Start()
    //{
    //    moveIndex = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        if (frozenTimer > 0)
        {
            frozenTimer -= Time.deltaTime;
            if (!attacked)
            {
                attackWaitTimer -= Time.deltaTime;
            }
        } else
        {
            frozenTimer = 0;
        }

        if (attackWaitTimer <= 0)
        {
            StartCoroutine(Attack());
            attacked = true;
            //isAttacking = true;
            //numAttackSoFar++;
            attackWaitTimer = attackWaitTime;
        }

        //if (isAttacking)
        //{
        //    attackTime -= Time.deltaTime;
        //    if (attackTime <= 0)
        //    {
        //        //GetComponent<Renderer>().material.color = Color.white;
        //        isAttacking = false;
        //        attackTime = 0.6f;
        //    }
        //}

        Move();
    }
    #endregion

    #region Movement Function
    private void Move()
    {        
        // Movement using transform
        if (transform.position == movePositions[moveIndex])
        {
            frozenTimer = frozenTime;
            //attackWaitTimer = attackWaitTime;
            attacked = false;
            animator.SetBool("isAttacking", false);
            //numAttackSoFar = 0;
            moveIndex++;
            if (moveIndex >= movePositions.Length)
            {
                moveIndex = 0;
            }
        }

        if (frozenTimer <= 0)
        {
            Vector3 moveTo = movePositions[moveIndex];
            transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);
        }

        // Movement using rigidbody
        //Vector3 movementVector = (moveTo - bossRigidbody.position).normalized;
        //bossRigidbody.MovePosition(bossRigidbody.position + movementVector * Time.deltaTime * moveSpeed);
    }
    #endregion

    #region Attack Functions
    private IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        for (int i = 0; i < attacksPerPosition; i++)
        {
            UseAttack();
            Debug.Log("Attack Number: " + i);
            yield return new WaitForSeconds(cooldownTime);
        }
        //attacked = true;
    }

    private void UseAttack()
    {
        //GetComponent<Renderer>().material.color = Color.black;
        Debug.Log("This boss is performing an attack now.");
        RaycastHit hit;
        //attackEffect.Play();
        if (Physics.SphereCast(transform.position + attackOffset, 0.5f, Vector3.left, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().TakeDamage(damageDealt);
            }
        }
    }
    #endregion

    #region Health Functions
    public override void Heal(float amount)
    {
        currHealth += amount;
        if (currHealth >= totalHealth)
        {
            currHealth = totalHealth;
        }
        HPBar.value = currHealth / totalHealth;
        Debug.Log("Current health of boss: " + currHealth);
    }

    public override void TakeDamage(float amount)
    {
        currHealth -= amount;
        HPBar.value = currHealth / totalHealth;
        Debug.Log("Current health of boss: " + currHealth);
        if (currHealth <= 0)
        {
            Purify();

            SceneManager.LoadScene("Aftermath");
        }
    }
    #endregion
}
