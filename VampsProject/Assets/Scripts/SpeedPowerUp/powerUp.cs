using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public Buffs buffs;


    [SerializeField] GameObject objToDisable;
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }


    IEnumerator speedBuff(Collider2D collision)
    {
        buffs.Apply(collision.gameObject);
        SpriteRenderer asd = objToDisable.GetComponent<SpriteRenderer>();

        asd.enabled = false;
        collider.enabled = false;

        Debug.Log("1s");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("2s");
        
        buffs.ReturnToNormal(collision.gameObject);

        Destroy(objToDisable);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
           
            StartCoroutine(speedBuff(collision));
           
        }
    }
}
