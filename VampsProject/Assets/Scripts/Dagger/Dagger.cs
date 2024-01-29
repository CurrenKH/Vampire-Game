using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    // Start is called before the first frame update
    float daggerDMG = SpinningDagger.DMG;
    float knockBack = SpinningDagger.knockBack;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
            Vector3 dir = collision.transform.position - playerPosition;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force.
            // This will push back the player
            Debug.Log("Direction: " + dir + "\n knockback: " + knockBack);
            Rigidbody2D enemyRgbd = collision.gameObject.GetComponent<Rigidbody2D>();
            enemyRgbd.AddForce(-dir * knockBack);
            enemy.EnemyHurt(daggerDMG);

        }
    }
}
