using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    float lifeTime = 3;
    float dmg = 0.5f;
    float dmgHolder = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
          //  Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("boom DMG");
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.EnemyHurt(dmg);
            enemy.Stun();
        }
    }
    public void ResetPrefab()
    {
        transform.localScale = new Vector3(1, 1, 1);
        dmg = dmgHolder;
        FollowPlayer.ResetCD();
    }
    public void BiggerAttack(float percentageIncrease)
    {
        transform.localScale *= percentageIncrease;
    }
    public void DmgIncrease(float dmgIncrease)
    {
        dmg += dmgIncrease;
        Debug.Log("DAMAGE" + dmg);
    }
    public void StunIncrease(float stunIncrease)
    {
        FollowPlayer.stunnedCDHolder += stunIncrease;
    }
}
