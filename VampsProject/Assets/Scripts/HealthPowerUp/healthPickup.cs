using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/HealthBuff")]

public class healthPickup : HealthBuff
{

    float AddHealth = 10f;


    public override void Apply(GameObject target)
    {
        Debug.Log("Initial " + target.GetComponent<PlayerController>().playerHealth);

        if (target.GetComponent<PlayerController>().playerHealthSlider.value < target.GetComponent<PlayerController>().playerHealthSlider.maxValue)
        {
            target.GetComponent<PlayerController>().playerHealth += AddHealth;

            float currentHealth = target.GetComponent<PlayerController>().playerHealth;
            Debug.Log("Current " + target.GetComponent<PlayerController>().playerHealth);

            target.GetComponent<PlayerController>().playerHealthSlider.value = currentHealth;

        }
        else
        {
            Debug.Log("Test");
        }
       

       
    }
}
