using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]

public class SpeedBuff : Buffs
{

    private float Addspeed = 10f;


    public override void Apply(GameObject target)
    {

        target.GetComponent<PlayerController>().speed += Addspeed;

    }

    public override void ReturnToNormal(GameObject target)
    {


        target.GetComponent<PlayerController>().speed -= Addspeed;


    }

}



