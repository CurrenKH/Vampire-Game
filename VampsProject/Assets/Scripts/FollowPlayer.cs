using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //[SerializeField] PlayerController player;
    [SerializeField] float speed = 1;
    Rigidbody2D rb2d;
    bool isHittingPlayer = false;
    [SerializeField] public float stunnedCD = 1;
    public static float stunnedCDHolder;
    public static float stunnedCDHolderSetter;
    bool isStunned = false;
    SpriteRenderer sprite;
    Vector3 stunnedSize;
    Vector3 normalSize;


    // Start is called before the first frame update
    void Start()
    {
        stunnedCDHolder = stunnedCD;
        stunnedCDHolderSetter = stunnedCDHolder;
        stunnedSize = transform.localScale * 0.5f;
        normalSize = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHittingPlayer && isStunned == false)
        {
            Debug.Log("Stunnning Moving: "+ isStunned);
            GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = (tempPlayer.transform.position - gameObject.transform.position).normalized * speed;
            rb2d.velocity = direction; //(direction, ForceMode2D.Impulse);
            //rb2d.AddForce(direction); very cool dynamic follow player
        }
        else
        {
            Vector2 direction = new Vector2(0, 0);
            rb2d.velocity = direction;
            Debug.Log("Stunnning NOT Moving: " + isStunned);
        }

        if (isStunned) stunnedCD -= Time.deltaTime;

        if (stunnedCD <= 0)
        {
            isStunned = false; sprite.color = Color.white;
            gameObject.transform.localScale = normalSize;
            gameObject.GetComponent<Animator>().enabled = true;
            stunnedCD = stunnedCDHolder;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") isHittingPlayer = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") isHittingPlayer = false;
    }
    public static void ResetCD()
    {
        stunnedCDHolder = stunnedCDHolderSetter;
    }
    public void Stunned()
    {
        isStunned = true;
        // make blue white ish
        //sprite.color = new Color(69,72,255,255);
        Debug.Log("Stunnning");
        sprite.color = Color.black;
        gameObject.transform.localScale = stunnedSize;
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
