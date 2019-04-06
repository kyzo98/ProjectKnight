using System.Collections;
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

    public GameObject inventory;

    public bool inventoryActive;
    public bool inventoryFilled;

    void Start()
    {
        //GETTING ACCESS TO PLAYER SCRIPT
        this.GetComponent<Player>();

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

        //ORBS INITIALIZATION AND SAVE
        orbs.quantity = PlayerPrefs.GetInt("ORBS");
    }

    void Update()
    {
        //if (Input.GetKeyDown("i"))
        //{
        //    inventoryActive = !inventoryActive;
        //}

        //if(inventoryActive == true)
        //{
        //    OpenInventory();
        //    //Debug.Log("inventory active");
        //}
        //else
        //{
        //    CloseInventory();
        //    //Debug.Log("Inventory unactive");
        //}
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
    }

    public void CloseInventory()
    {
        inventory.SetActive(false);
    }

    //FUNCTIONS TO ADD SORROWS
    public void AddRage()
    {
        sorrows.rage += 1;
        PlayerPrefs.SetInt("Rage", sorrows.rage);
    }

    public void AddTerror()
    {
        sorrows.terror += 1;
        PlayerPrefs.SetInt("Terror", sorrows.terror);
    }

    public void AddGrief()
    {
        sorrows.grief += 1;
        PlayerPrefs.SetInt("Grief", sorrows.grief);
    }

    //FUNCTIONS TO ADD DRIVES
    public void AddCourage()
    {
        drives.courage += 1;
        PlayerPrefs.SetInt("Courage", drives.courage);
    }

    public void AddFocus()
    {
        drives.focus += 1;
        PlayerPrefs.SetInt("Focus", drives.focus);
    }

    public void AddWill()
    {
        drives.will += 1;
        PlayerPrefs.SetInt("Will", drives.will);
    }

    public void AddRemembrance()
    {
        drives.remembrance += 1;
        PlayerPrefs.SetInt("Remembrance", drives.remembrance);
    }

    public void AddSpiritualHealing()
    {
        drives.spiritualHealing += 1;
        PlayerPrefs.SetInt("SpiritualHealing", drives.spiritualHealing);
    }

    public void AddClarity()
    {
        drives.clarity += 1;
        PlayerPrefs.SetInt("Clarity", drives.clarity);
    }

    public void AddGrace()
    {
        drives.grace += 1;
        PlayerPrefs.SetInt("Grace", drives.grace);
    }

    //FUNCTIONS FOR ORBS
    public void AddVitality()
    {
        if (orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.vitality += 1;
        }
    }

    public void AddStrenght()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.strenght += 1;
        }
    }

    public void AddEndurance()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.endurance += 1;
        }
    }

    public void AddPower()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.power += 1;
        }
    }

    public void AddVigor()
    {
        if(orbs.quantity > 0)
        {
            orbs.quantity -= 1;
            PlayerPrefs.SetInt("ORBS", orbs.quantity);
            playerScript.stats.vigor += 1;
        }
    }
}
