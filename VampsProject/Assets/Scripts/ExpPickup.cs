using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExpPickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float EXP_worth = 5;
    float totalEXP;
    public static bool isLeveling;
    Slider ExpSlider;
    void Start()
    {
        ExpSlider = GameObject.FindGameObjectWithTag("EXP Slider").GetComponent<Slider>();
    }
    private void Update()
    {
        if(!isLeveling && totalEXP > 0)
        {
            levelUp();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            totalEXP = levelUp();
        }

    }

    private float levelUp()
    {
        //ExpSlider.value += EXP_worth;
        totalEXP = ExpSlider.value + EXP_worth;
        // enough xp to level up
        if (totalEXP > ExpSlider.maxValue)
        {
            LevelUpOptions levelUpScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelUpOptions>();
            isLeveling = true;
            if (LevelUpOptions.MaxLevel == false)
            {
                if (isLeveling) levelUpScript.LevelUp();
            }

            //WaitForLevelUp();



            TMP_Text levelUptext = GameObject.FindGameObjectWithTag("leveluptext").GetComponent<TMP_Text>();

            // level up and retain extra exp
            float extraEXP = (ExpSlider.value + EXP_worth) - ExpSlider.maxValue;
            ExpSlider.maxValue *= 1.2f;
            // level up menu pop up

            // change level
            int currentLvl = int.Parse(levelUptext.text);


            levelUptext.text = (currentLvl + 1).ToString();
            if (LevelUpOptions.MaxLevel) Destroy(gameObject);
            return extraEXP;

            Debug.Log("extraEXP : " + extraEXP + "\n ExpSlider.maxValue: " + ExpSlider.maxValue);
            while (extraEXP > ExpSlider.maxValue)
            {
                    //StartCoroutine(WaitForLevelUp());
                    Debug.Log("In Do While");

                    if (!isLeveling)
                    {
                        // do nothing to retain exp
                        // level up again and retain extra EXP until there is no extra EXP
                        extraEXP -= ExpSlider.maxValue;
                        ExpSlider.maxValue *= 1.3f;
                        // level up menu pop up
                        levelUpScript.LevelUp();


                        // change level
                        levelUptext.text = (int.Parse(levelUptext.text) + 1).ToString();
                    }
                } 

            ExpSlider.value = extraEXP;
            //ExpSlider.value = 0;
            Debug.Log("ExpSlider.maxValue: " + ExpSlider.maxValue);

        }
        else
        {
            ExpSlider.value = totalEXP;
            Destroy(gameObject);
            return 0;
        }
    }

    IEnumerator WaitForLevelUp()
    {
        yield return new WaitForSeconds(1);
    }
    private bool GetLevelUpMenuState()
    {
        GameObject levelupMenu =  GameObject.FindGameObjectWithTag("LevelUpMenu");
        if(levelupMenu.activeSelf == false)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
}
