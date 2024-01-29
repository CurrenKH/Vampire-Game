using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPowerUpPickup : MonoBehaviour
{
    public HealthBuff health;
    [SerializeField] GameObject objToDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            health.Apply(collision.gameObject);
            Debug.Log("Picked up");

            Destroy(objToDestroy);
        }
    }
}
