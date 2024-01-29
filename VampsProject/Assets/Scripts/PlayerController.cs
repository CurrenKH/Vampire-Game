using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerHealth = 100;
    //public static GameObject playerObject;
    [SerializeField] public Slider playerHealthSlider;
    [SerializeField] Slider playerExpSlider;
    float EXPStart = 400;
    [SerializeField] public float speed = 3f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashLength = 0.5f;
    [SerializeField] float dashCD = 1f;



    [SerializeField] GameObject SlashAttack;
    [SerializeField] GameObject SlashAttackBlack;
    float slashAttackTimeHolder = 2f;
    float slashAttackTimer = 2f;
    bool slashAttacking = false;
    public bool IsSlashEvolved = false;

    [SerializeField] GameObject LigntingAttack;
    float ligntingAttackTimeHolder = 4f;
    // to sperate timing from slash attack delay start of lighting
    float ligntingAttackTimer = 3f;

    //Slider playerHealthSlider;

    // -- Handle input and movement --
    public float inputX = 0;
    public float activeMoveSpeed;
    private float dashCounter;
    private float dashCDCounter;
    float horizontal;
    float vertical;
    Rigidbody2D rb2d;
    //Collider2D collider2D;
    Collider2D collider;
    SpriteRenderer spriteRenderer;
    private Animator m_animator;
    private int m_facingDirection = 1;
    private float m_delayToIdle = 0.0f;
    private Vector2 moveInput;
    public static bool m_isDead;


    bool invincible = false;


    [SerializeField] bool testRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        //playerObject = GetComponent<GameObject>();
        activeMoveSpeed = speed;

        Time.timeScale = 1;
        m_isDead = false;
        //collider2D = GetComponent<Collider2D>();
    }
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealthSlider.maxValue = playerHealth;
        playerHealthSlider.value = playerHealth;
        playerExpSlider.maxValue = EXPStart;
        playerExpSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("m_isDead--- " + m_isDead);
        if (!m_isDead)
        {
            Debug.Log("m_isDead inside update " + m_isDead);

            if (testRunning) IsSlashEvolved = playerProjectileAttack.Evolve(); Debug.Log("slash" + IsSlashEvolved);
            if (!testRunning) Debug.Log("slash" + IsSlashEvolved);
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Dashing();


            if (!invincible)
            {
                collider.enabled = true;
                spriteRenderer.color = new Color(255, 255, 255);
            }
            else
            {
                collider.enabled = false;
                spriteRenderer.color = new Color(0, 0, 0);
            }

            // trigger attack animation with good timing
            if (slashAttackTimer <= 0.3f && slashAttacking == false)
            {
                m_animator.SetTrigger("Attack");
                slashAttacking = true;
            }
            if (slashAttackTimer <= 0)
            {
                PlayerSlashAttack();
                slashAttackTimer = slashAttackTimeHolder;
                slashAttacking = false;
            }
            else
            {
                slashAttackTimer -= Time.deltaTime;
            }

            // lightning attacker
            if (ligntingAttackTimer <= 0)
            {
                PlayerLightningAttack();
                ligntingAttackTimer = ligntingAttackTimeHolder;
            }
            else
            {
                ligntingAttackTimer -= Time.deltaTime;
            }

            //Attack
            if (Input.GetKeyDown("e"))
            {
                m_animator.SetTrigger("Attack");
                //Debug.Log("Attack");
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                //m_delayToIdle = 0.05f;
                m_animator.SetInteger("animState", 1);
                //Debug.Log("Running inputX: " + inputX);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_animator.SetInteger("animState", 0);
                //Debug.Log("Idle");
            }
        }
    }
    void FixedUpdate()
    {

        if(!m_isDead)
        {
            Debug.Log("m_isDead inside fixed update " + m_isDead);

            Vector2 position = rb2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            rb2d.MovePosition(position);

            //  Swap direction of sprite depending on walk direction
            //  Move right
            if (Input.GetKey("d"))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                m_facingDirection = 1;
                inputX = -1;
            }

            //  Move left
            else if (Input.GetKey("a"))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                m_facingDirection = -1;
                inputX = 1;
            }

            //  Moving upwards while sprite is already facing right
            else if (Input.GetKey("w") && m_facingDirection == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;

                inputX = 1;
            }

            //  Moving downwards while sprite is already facing left
            else if (Input.GetKey("s") && m_facingDirection == -1)
            {
                GetComponent<SpriteRenderer>().flipX = true;

                inputX = -1;
            }

            //  Moving upwards while sprite is already facing left
            else if (Input.GetKey("w") && m_facingDirection == -1)
            {
                GetComponent<SpriteRenderer>().flipX = true;

                inputX = -1;
            }

            //  Moving downwards while sprite is already facing right
            else if (Input.GetKey("s") && m_facingDirection == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;

                inputX = 1;
            }
            else
            {
                inputX = 0;
            }
        }
    }

    private void Dashing()
    {

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCDCounter <= 0 && dashCounter <= 0)
            {
                speed = dashSpeed;
                dashCounter = dashLength;
                invincible = true;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                speed = activeMoveSpeed;
                dashCDCounter = dashCD;
                invincible = false;
            }
        }

        if (dashCDCounter > 0)
        {
            dashCDCounter -= Time.deltaTime;
        }
    }
    IEnumerator Delay()
    {
        //Wait for 3 seconds
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("MainMenu");
    }
    public void PlayerHurt(float dmg)
    {
        playerHealth -= dmg;
        Debug.Log(playerHealth);
        playerHealthSlider.value = playerHealth;
        m_animator.SetTrigger("Hurt");

        if (playerHealth <= 0)
        {
            m_isDead = true;
            collider.enabled = false;
            m_animator.SetTrigger("Death");

            StartCoroutine(Delay());
        }
    }
    private void PlayerSlashAttack()
    {
        float attackpositionX = 2.5f;
        float attackpositionY = -0.22f;

        Quaternion rotation = new Quaternion();

        if (spriteRenderer.flipX == true)
        {
            rotation[1] = 180f;
            attackpositionX *= -1;
        }
        //Debug.Log(transform.rotation);
        Instantiate(SlashAttack, new Vector3(transform.position.x + attackpositionX, transform.position.y + attackpositionY, transform.position.z), rotation);
        if (IsSlashEvolved)
        {
            Instantiate(SlashAttackBlack, new Vector3(transform.position.x + attackpositionX + 0.5f, transform.position.y + attackpositionY, transform.position.z), rotation);
        }

    }
    private void PlayerLightningAttack()
    {
        // lightning vector
        Vector3 lightningOffest = new Vector3(0.0189999994f, -0.108999997f, 0);

        Vector3 playerPosition = gameObject.transform.position;

        Vector3 lightningSpawn = playerPosition + lightningOffest;

        //Debug.Log(transform.rotation);
        Instantiate(LigntingAttack, lightningSpawn, Quaternion.identity);
    }

    //
    
}
