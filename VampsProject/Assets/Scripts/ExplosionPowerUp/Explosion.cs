using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Powerups/Explosion")]


public class Explosion : exploBuff
{


    public override void explode(GameObject target, GameObject prefab)
    {

        Instantiate(prefab, new Vector3(target.transform.position.x, target.transform.position.y), Quaternion.identity);
    }
}
