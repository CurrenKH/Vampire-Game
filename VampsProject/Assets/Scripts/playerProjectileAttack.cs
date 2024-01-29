using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectileAttack : MonoBehaviour
{
    [SerializeField] float dmg = 2;
    [SerializeField] float dmgHolder = 2;
    [SerializeField] float lifeTime = 1;
    [SerializeField] public static bool  IsEvolved = false;
    GameObject slashObject;
    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        //slashObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.EnemyHurt(dmg);
        }
    }
    public void ResetPrefab()
    {
        transform.localScale = new Vector3(1, 1, 1);
        dmg = dmgHolder;
    }
    public void BiggerAttack(float percentageIncrease)
    {
        transform.localScale *= percentageIncrease;
    }
    public void DmgIncrease(float dmgIncrease)
    {
        dmg += dmgIncrease;
        Debug.Log("DAMAGE"+dmg);
    }
    public static bool Evolve()
    {
        return true;
    }
}
