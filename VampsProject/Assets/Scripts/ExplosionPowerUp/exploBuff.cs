using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class exploBuff : ScriptableObject
{
    public abstract void explode(GameObject target, GameObject prefab);
}
