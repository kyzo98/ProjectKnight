﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public struct Sorrows {
    public int rage;
    public int terror;
    public int grief;
};

public struct Drives {
    public int courage;
    public int focus;
    public int will;
    public int remembrance;
    public int spiritualHealing;
    public int clarity;
    public int grace;
};

public struct Orbs
{
    public int quantity;
};

public class Inventario : MonoBehaviour {

    //PLAYER
    public GameObject player;
    private Player playerScript;

    //ACCES TO SORROWS, DRIVES AND ORBS STRUCTS
    public Sorrows sorrows;
    public Drives drives;
    public Orbs orbs;

    //public GameObject inventory;

    public bool inventoryActive;
    public bool inventoryFilled;

    //UI
    //Sliders
    public Slider vitalitySlider;
    public Slider powerSlider;
    public Slider strenghtSlider;
    public Slider enduranceSlider;
    public Slider vigorSlider;
    //Stats Buttons
    public Button sumVitalityButton;
    public Button sumPowerButton;
    public Button sumStrenghtButton;
    public Button sumEnduranceButton;
    public Button sumVigorButton;
    //Sorrows buttons
    public Button buttonRage;
    public Button buttonTerror;
    public Button buttonGrief;
    //Drives buttons
    public Button buttonCourage;
    public Button buttonFocus;
    public Button buttonWill;
    public Button buttonRemembrance;
    public Button buttonSpiritualHealing;
    public Button buttonClarity;
    public Button buttonGrace;
    //Orbs quantity
    public Text orbsQuantityUIText;
    //Drives & Sorrows quantity
    public Text rageQuantity;
    public Text terrorQuantity;
    public Text griefQuantity;
    public Text courageQuantity;
    public Text focusQuantity;
    public Text willQuantity;
    //Money quantity
    public Text moneyQuantity;


    void Start()
    {

        //GETTING ACCESS TO PLAYER SCRIPT
        playerScript = this.GetComponent<Player>();

        //SORROWS INITIALIZATION AND SAVE
        sorrows.rage = PlayerPrefs.GetInt("Rage");
        sorrows.terror = PlayerPrefs.GetInt("Terror");
        sorrows.grief = PlayerPrefs.GetInt("Grief");

        //DRIVES INITIALIZATION AND SAVE
        drives.courage = PlayerPrefs.GetInt("Courage");
        drives.focus = PlayerPrefs.GetInt("Focus");
        drives.will = PlayerPrefs.GetInt("Will");
        drives.remembrance = PlayerPrefs.GetInt("Remembrance");
        drives.spiritualHealing = PlayerPrefs.GetInt("SpiritualHealing");
        drives.clarity = PlayerPrefs.GetInt("Clarity");
        drives.grace = PlayerPrefs.GetInt("Grace");

        //GETTING STATS VALUE TO SAVE THEM
        playerScript.stats.vitality = PlayerPrefs.GetInt("Vitality");
        playerScript.stats.vitality = PlayerPrefs.GetInt("Strenght");
        playerScript.stats.endurance = PlayerPrefs.GetInt("Endurance");
        playerScript.stats.power = PlayerPrefs.GetInt("Power");
        playerScript.stats.vigor = PlayerPrefs.GetInt("Vigor");

        //ORBS INITIALIZATION AND SAVE
        orbs.quantity = PlayerPrefs.GetInt("ORBS");
        playerScript.coins = PlayerPrefs.GetInt("COINS");

        //UI INITIALIZATION
        //Sliders
        RefreshUI();

        //Stats Buttons
        sumVitalityButton.onClick.AddListener(AddVitality);
        sumPowerButton.onClick.AddListener(AddPower);
        sumStrenghtButton.onClick.AddListener(AddStrenght);
        sumEnduranceButton.onClick.AddListener(AddEndurance);
        sumVigorButton.onClick.AddListener(AddVigor);

        //Drives buttons
        buttonRage.onClick.AddListener(AddRage);
        buttonTerror.onClick.AddListener(AddTerror);
        buttonGrief.onClick.AddListener(AddGrief);

        //Sorrows buttons
        buttonCourage.onClick.AddListener(AddCourage);
        buttonFocus.onClick.AddListener(AddFocus);
        buttonWill.onClick.AddListener(AddWill);
        buttonRemembrance.onClick.AddListener(AddRemembrance);
        buttonSpiritualHealing.onClick.AddListener(AddSpiritualHealing);
        buttonClarity.onClick.AddListener(AddClarity);
        buttonGrace.onClick.AddListener(AddGrace);
}

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        orbsQuantityUIText.text = orbs.quantity.ToString();

        vitalitySlider.value = playerScript.stats.vitality;
        powerSlider.value = playerScript.stats.power;
        strenghtSlider.value = playerScript.stats.strenght;
        enduranceSlider.value = playerScript.stats.endurance;
        vigorSlider.value = playerScript.stats.vigor;
        //Drives and Sorrows
        rageQuantity.text = sorrows.rage.ToString();
        terrorQuantity.text = sorrows.terror.ToString();
        griefQuantity.text = sorrows.grief.ToString();
        courageQuantity.text = drives.courage.ToString();
        focusQuantity.text = drives.focus.ToString();
        willQuantity.text = drives.will.ToString();
        //Money
        moneyQuantity.text = playerScript.coins.ToString();
    }

    public void AddRage()
    {
        sorrows.rage += 1;
        PlayerPrefs.SetInt("Rage", sorrows.rage);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddTerror()
    {
        sorrows.terror += 1;
        PlayerPrefs.SetInt("Terror", sorrows.terror);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddGrief()
    {
        sorrows.grief += 1;
        PlayerPrefs.SetInt("Grief", sorrows.grief);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    //FUNCTIONS TO ADD DRIVES
    public void AddCourage()
    {
        drives.courage += 1;
        PlayerPrefs.SetInt("Courage", drives.courage);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddFocus()
    {
        drives.focus += 1;
        PlayerPrefs.SetInt("Focus", drives.focus);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddWill()
    {
        drives.will += 1;
        PlayerPrefs.SetInt("Will", drives.will);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddRemembrance()
    {
        drives.remembrance += 1;
        PlayerPrefs.SetInt("Remembrance", drives.remembrance);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddSpiritualHealing()
    {
        drives.spiritualHealing += 1;
        PlayerPrefs.SetInt("SpiritualHealing", drives.spiritualHealing);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddClarity()
    {
        drives.clarity += 1;
        PlayerPrefs.SetInt("Clarity", drives.clarity);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    public void AddGrace()
    {
        drives.grace += 1;
        PlayerPrefs.SetInt("Grace", drives.grace);
        playerScript.coins -= 50;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        RefreshUI();
    }

    //FUNCTIONS FOR ORBS
    public void AddVitality()
    {
        if (orbs.quantity > 0)
        {
            Debug.Log("Player vitality afte adding more is: " + playerScript.stats.vitality.ToString());
            Debug.Log("Player health after adding vitality is: " + playerScript.maxHealth.ToString());
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.vitality += 1;
            PlayerPrefs.SetInt("Vitality", playerScript.stats.vitality);
            Debug.Log("Player vitality now is: " + playerScript.stats.vitality.ToString());
            Debug.Log("Player health now is: " + playerScript.maxHealth.ToString());
            RefreshUI();
        }
    }

    public void AddStrenght()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.strenght += 1;
            PlayerPrefs.SetInt("Strenght", playerScript.stats.strenght);
            strenghtSlider.value = playerScript.stats.strenght;
            RefreshUI();
        }
    }

    public void AddEndurance()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.endurance += 1;
            PlayerPrefs.SetInt("Endurance", playerScript.stats.endurance);
            enduranceSlider.value = playerScript.stats.endurance;
            RefreshUI();
        }
    }

    public void AddPower()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.power += 1;
            PlayerPrefs.SetInt("Power", playerScript.stats.power);
            powerSlider.value = playerScript.stats.power;
            RefreshUI();
        }
    }

    public void AddVigor()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.vigor += 1;
            PlayerPrefs.SetInt("Vigor", playerScript.stats.vigor);
            vigorSlider.value = playerScript.stats.power;
            RefreshUI();
        }
    }
}
