using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDagger : MonoBehaviour
{
    [SerializeField] public static float DMG = 2;
    float dmgHolder = 2;
    [SerializeField] public static float knockBack = 500; 
    [SerializeField] float rotationSpeed = 1f;
    GameObject DaggersHolder;
    [SerializeField] GameObject Dagger1;
    [SerializeField] GameObject Dagger2;
    [SerializeField] GameObject Dagger3;
    [SerializeField] GameObject Dagger4;

    bool maxDaggersReached = false;
    // Start is called before the first frame update
    void Start()
    {
        DaggersHolder = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // float to center the dagger holder on the real pivot point of the character
        //float y = -0.087f;
        //float x = 0.023f;
        //DaggersHolder.transform.position = new Vector3(player.transform.position.x + x, player.transform.position.y + y);
        DaggersHolder.transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));

        //float astralAngle = DaggersHolder.transform.localRotation.eulerAngles.z;
    }
    public void ResetPrefab()
    {
        transform.localScale = new Vector3(1, 1, 1);
        DMG = dmgHolder;
    }
    public void DmgIncrease(float dmgIncrease)
    {
        DMG += dmgIncrease;
    }
    public void RotationSpeed(float rotationIncrease)
    {
        rotationSpeed *= rotationIncrease;
    }
    public void KnockBackIncrease(float knockbackIncrease)
    {
        knockBack *= knockbackIncrease;
    }
    public void MoreDaggers()
    {
        if(Dagger1.activeSelf == false)
        {
            Dagger1.SetActive(true);
            return;
        }
        else if (Dagger2.activeSelf == false)
        {
            Dagger2.SetActive(true);
            return;
        }
        else if (Dagger3.activeSelf == false)
        {
            Dagger3.SetActive(true);
            return;
        }
        else if (Dagger4.activeSelf == false)
        {
            Dagger4.SetActive(true);
            maxDaggersReached = true;
            return;
        }
    }
}
