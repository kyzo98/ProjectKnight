using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBattle : MonoBehaviour {

    public static InventoryBattle inventario;

    //public GameObject inventoryHolderPrefab;
    public GameObject spellsHolderPrefab;
    public GameObject inventory;
    //public Transform gridOrbs;
    public Transform gridSpells;

    public bool inventoryActive;
    public bool inventoryFilled;

    void Start()
    {
        inventario = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            inventoryActive = !inventoryActive;
        }

        if (inventoryActive == true)
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

    //public void AddItem(OrbItems orbItem)
    //{
    //    GameObject holderClone = Instantiate(inventoryHolderPrefab, gridOrbs);
    //    ItemHolder holdInventarioScript = holderClone.GetComponent<ItemHolder>();

    //    holdInventarioScript.itemName.text = orbItem.orbName;
    //    holdInventarioScript.itemImg.sprite = orbItem.unbuyedOrbImg;
    //    holdInventarioScript.type.text = orbItem.orbType.ToString();
    //}

    public void AddSpellToBattle(SpellItems spellItems)
    {
        GameObject holderClone = Instantiate(spellsHolderPrefab, gridSpells);
        ItemHolder holdInventarioScript = holderClone.GetComponent<ItemHolder>();

        holdInventarioScript.itemName.text = spellItems.spellName;
        holdInventarioScript.itemImg.sprite = spellItems.spellImg;
        holdInventarioScript.type.text = spellItems.spellType.ToString();
        holdInventarioScript.itemPrice.text = null;
    }
}
