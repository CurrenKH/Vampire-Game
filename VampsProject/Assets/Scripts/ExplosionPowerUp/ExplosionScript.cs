using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public exploBuff exploActiveBuff;
    [SerializeField] GameObject objToDestroy;
    [SerializeField] GameObject myPrefab;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          
            exploActiveBuff.explode(collision.gameObject, myPrefab);
          
            Destroy(objToDestroy);
        }
    }

}
