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

public class Inventario : MonoBehaviour {

    public Sorrows sorrows;
    public Drives drives;

    public GameObject inventory;

    public bool inventoryActive;
    public bool inventoryFilled;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryActive = !inventoryActive;
        }

        if(inventoryActive == true)
        {
            OpenInventory();
            //Debug.Log("inventory active");
        }
        else
        {
            CloseInventory();
            //Debug.Log("Inventory unactive");
        }
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
}
