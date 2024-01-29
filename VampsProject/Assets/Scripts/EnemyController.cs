using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private float enemyHealth = 10f;
    [SerializeField] private float meleeDMG = 5f;
    [SerializeField] private float ExpWorth = 45;

    
    bool isAttacking = false;
    [SerializeField] float dmgTicks = 1f;
    float timer;
    Rigidbody2D rb2d;
    Collider2D collider;
    PlayerController player;
    private Animator m_animator;
    SpriteRenderer spriteRenderer;

    public float enemyHealthHolder;
    public float enemyExpWorthHolder;


    Vector3 oldPos;
    Quaternion oldRot;


    [SerializeField] GameObject greenGem;
    [SerializeField] GameObject redGem;
    [SerializeField] GameObject purpleGem;
    [SerializeField] GameObject orangeGem;
    [SerializeField] GameObject blueGem;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealthHolder = enemyHealth;
        enemyExpWorthHolder = ExpWorth;
    }

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ENEMY CURRENT HEALTH " + enemyHealthHolder);

        if(Mathf.Abs(timer) > 0)
        {
            timer -= Time.deltaTime;
        }
        if(isAttacking && timer <= 0)
        {
            player.PlayerHurt(meleeDMG);
            timer = dmgTicks;
        }

        if(enemyHealthHolder < 0)
        {
            m_animator.SetTrigger("Death");
            StartCoroutine(Delay());
            EnemyDie();
        }

        //  Enemy position checked for flipping sprite based off left or right movement
        Vector3 movement = oldRot * (transform.position - oldPos);
        if (movement.x > 0)
        {
            // right
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movement.x < 0)
        {
            // left
            GetComponent<SpriteRenderer>().flipX = true;
        }
        oldPos = transform.position;
        oldRot = transform.rotation;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = true;
            m_animator.SetInteger("animState", 1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = false;
            m_animator.SetInteger("animState", 0);
        }
    }

    public void EnemyHurt(float dmg)
    {
        enemyHealthHolder -= dmg;
        m_animator.SetTrigger("Hurt");
    }

    IEnumerator Delay()
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(2);
    }
    public void Stun()
    {
        gameObject.GetComponent<FollowPlayer>().Stunned();
    }

    public void BuffEnemyHealth(float amountIncrease)
    {
        enemyHealthHolder *= amountIncrease;
    }
    void EnemyDie()
    {
        // green 15
        // 1 * 15 == 15
        // red 45
        // 3 * 15 == 45
        // purple 135
        // 9 * 15 == 135
        // orange 405
        // 27 * 15 == 405
        // blue 1215 
        // 81 * 15 == 1215

        // this way we quantify the exp by the basic green value of 15
        ExpWorth /= 15;
        while (ExpWorth >= 81)
        {
            Vector3 trajectory = Random.insideUnitCircle * 5;
            GameObject gem = Instantiate(blueGem, transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            ExpWorth -= 81;
            Debug.Log("EXPworth qty left: "+ ExpWorth);
        }
        while (ExpWorth >= 27)
        {
            Vector3 trajectory = Random.insideUnitCircle * 5;
            GameObject gem = Instantiate(orangeGem, transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            ExpWorth -= 27;
            Debug.Log("EXPworth qty left: " + ExpWorth);

        }
        while (ExpWorth >= 9)
        {
            Vector3 trajectory = Random.insideUnitCircle * 5;
            GameObject gem = Instantiate(purpleGem, transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            ExpWorth -= 9;
            Debug.Log("EXPworth qty left: " + ExpWorth);
        }
        while (ExpWorth >= 3)
        {
            Vector3 trajectory = Random.insideUnitCircle * 5;
            GameObject gem = Instantiate(redGem, transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            ExpWorth -= 3;
            Debug.Log("EXPworth qty left: " + ExpWorth);
        }
        while (ExpWorth >= 1)
        {

            Vector3 trajectory = Random.insideUnitCircle * 5;
            GameObject gem = Instantiate(redGem, transform.position, Quaternion.identity);
            gem.GetComponent<Rigidbody2D>().AddForce(trajectory, ForceMode2D.Impulse);
            ExpWorth -= 1;
            Debug.Log("EXPworth qty left: " + ExpWorth);
        }
        //Instantiate();
        Destroy(gameObject);
    }


}
