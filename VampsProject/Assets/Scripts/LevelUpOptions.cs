using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelUpOptions : MonoBehaviour
{
    public static bool MaxLevel = false;
    int slashLVL = 1;
    int lightningLVL = 1;
    int daggerLVL = 1;

    string daggerlvlHolder = "";
    string daggerUpgradetextHolder = "unlock dagger";

    string lightninglvlHolder = "";
    string lightningUpgradetextHolder = "+0.5 dmg";

    string slashlvlHolder = "";
    string slashUpgradetextHolder = "+5 dmg";

    [SerializeField] GameObject LevelUpMenu;

    [SerializeField] Button slashButton; // 1
    [SerializeField] TextMeshProUGUI slashText ;
    [SerializeField] Button lightningButton; // 2
    [SerializeField] TextMeshProUGUI lightningText;
    [SerializeField] Button daggerButton; // 3
    [SerializeField] TextMeshProUGUI daggerText;

    [SerializeField] GameObject slashPrefab;
    playerProjectileAttack slashScript;

    [SerializeField] GameObject slashPrefabBlack;
    playerProjectileAttack slashScriptBlack;

    [SerializeField] GameObject lightningPrefab;
    LightningAttack lightningScript;

    SpinningDagger daggersScript;


    // Start is called before the first frame update
    void Start()
    {
        slashlvlHolder = slashLVL.ToString();
        lightninglvlHolder = lightningLVL.ToString();
        daggerlvlHolder = daggerLVL.ToString();

        slashScript = slashPrefab.GetComponent<playerProjectileAttack>();
        slashScriptBlack = slashPrefabBlack.GetComponent<playerProjectileAttack>();

        lightningScript = lightningPrefab.GetComponent<LightningAttack>();

        daggersScript = GameObject.FindGameObjectWithTag("Daggers").GetComponent<SpinningDagger>();
        ResetAttacks();
    }

    // Update is called once per frame
    void Update()
    {
        if(lightningLVL == 7 && daggerLVL == 7 && slashLVL == 6)
        {
            MaxLevel = true;
        }
    }

    public void LevelUp()
    {

        Time.timeScale = 0;
        LevelUpMenu.SetActive(true);

        setSlashAttackButton();
        setLightningAttackButton();
        setDaggerAttackButton();
    }
    public void ResetAttacks()
    {
        slashScript.ResetPrefab();
        slashScriptBlack.ResetPrefab();

        daggersScript.ResetPrefab();
        lightningScript.ResetPrefab();


        slashLVL = 1;
        lightningLVL = 1;
        daggerLVL = 1;
        daggerUpgradetextHolder = "unlock dagger";
        lightningUpgradetextHolder = "+0.5 dmg";
        slashUpgradetextHolder = "+5 dmg";

        MaxLevel = false;
        GameObject.FindGameObjectWithTag("leveluptext").GetComponent<TMP_Text>().text = "1";

    }
    public void setSlashAttackButton()
    {
        slashButton.onClick.RemoveAllListeners();
        slashText.text = "lvl "+ slashlvlHolder +": " + slashUpgradetextHolder;
        slashButton.onClick.AddListener(SlashLvlUpgrade);
    }
    public void setLightningAttackButton()
    {
        lightningButton.onClick.RemoveAllListeners();
        lightningText.text = "lvl " + lightninglvlHolder + ": " + lightningUpgradetextHolder;
        Debug.Log("BING"+"lvl " + lightninglvlHolder + ": " + lightningUpgradetextHolder);
        lightningButton.onClick.AddListener(LightningLvlUpgrade);
    }
    public void setDaggerAttackButton()
    {
        daggerButton.onClick.RemoveAllListeners();
        daggerText.text = "lvl " + daggerlvlHolder + ": " + daggerUpgradetextHolder;
        daggerButton.onClick.AddListener(DaggerLvlUpgrade);
    }
    public void DaggerLvlUpgrade()
    {
        daggerLVL++;
        daggerlvlHolder = daggerLVL.ToString();

        if (daggerLVL == 2)
        {
            daggersScript.MoreDaggers();
            daggerUpgradetextHolder = "+3 dmg";
        }
        else if (daggerLVL == 3)
        {
            daggersScript.DmgIncrease(3f);
            daggerUpgradetextHolder = "unlock dagger";
        }
        else if (daggerLVL == 4)
        {
            daggersScript.MoreDaggers();
            daggerUpgradetextHolder = "30% faster";
        }
        else if (daggerLVL == 5)
        {
            daggersScript.RotationSpeed(1.3f);
            daggerUpgradetextHolder = "unlock dagger";
        }
        else if (daggerLVL == 6)
        {
            daggersScript.MoreDaggers();
            daggerUpgradetextHolder = "+30% Knockback";
        }
        else if (daggerLVL == 7)
        {
            daggersScript.KnockBackIncrease(1.3f);
            daggerButton.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
        LevelUpMenu.SetActive(false);
        ExpPickup.isLeveling = false;
    }
    public void LightningLvlUpgrade()
    {
        lightningLVL++;
        lightninglvlHolder = lightningLVL.ToString();

        if (lightningLVL == 2)
        {
            lightningScript.DmgIncrease(0.5f);
            lightningUpgradetextHolder = "+30% Size";
        }
        else if (lightningLVL == 3)
        {
            lightningScript.BiggerAttack(1.3f);
            lightningUpgradetextHolder = "+1 dmg";
        }
        else if (lightningLVL == 4)
        {
            lightningScript.DmgIncrease(0.5f);
            lightningUpgradetextHolder = "+30% Size";
        }
        else if (lightningLVL == 5)
        {
            lightningScript.BiggerAttack(1.3f);
            lightningUpgradetextHolder = "+1 dmg";
        }
        else if (lightningLVL == 6)
        {
            lightningScript.DmgIncrease(0.5f);
            lightningUpgradetextHolder = "+1 Stun Time";
        }
        else if (lightningLVL == 7)
        {
            lightningScript.StunIncrease(1);
            lightningButton.gameObject.SetActive(false);
            // 1st upgrade
        }
        Time.timeScale = 1;
        LevelUpMenu.SetActive(false);
        ExpPickup.isLeveling = false;
    }
    public void SlashLvlUpgrade()
    {
        slashLVL++;
        slashlvlHolder = slashLVL.ToString();

        Debug.Log("slashLVL: " + slashLVL);
        if (slashLVL == 2)
        {
            slashScript.DmgIncrease(5f);
            slashScriptBlack.DmgIncrease(5f);
            slashUpgradetextHolder = "+20% size";
        }
        else if (slashLVL == 3)
        {
            slashScript.BiggerAttack(1.2f);
            slashScriptBlack.BiggerAttack(1.2f);
            slashUpgradetextHolder = "+5 dmg";
        }
        else if (slashLVL == 4)
        {
            slashScript.DmgIncrease(5f);
            slashScriptBlack.DmgIncrease(5f);
            slashUpgradetextHolder = "+30% size";
        }
        else if (slashLVL == 5)
        {
            slashScript.BiggerAttack(1.3f);
            slashScriptBlack.BiggerAttack(1.3f);
            slashUpgradetextHolder = "Evolve";
        }
        else if (slashLVL == 6)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.IsSlashEvolved = true;
            slashButton.gameObject.SetActive(false);
        }

        Time.timeScale = 1;
        LevelUpMenu.SetActive(false);
        ExpPickup.isLeveling = false;
    }
}
