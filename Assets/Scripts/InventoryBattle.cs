using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBattle : MonoBehaviour {

    public static InventoryBattle inventario;

    SpellItems[] spellItems;

    int space = 3;

    //public GameObject inventoryHolderPrefab;
    public GameObject spellsHolderPrefab;
    public GameObject inventory;
    //public Transform gridOrbs;
    public Transform gridSpells;

    public bool inventoryActive;

    void Start()
    {
        inventario = this;
        for (int i = 0; i < SpellsShop.spellShopScript.spellItems.Length; i++)
        {
            if (SpellsShop.spellShopScript.spellItems[i].buyed == true)
            {
                GameObject holderClone = Instantiate(spellsHolderPrefab, gridSpells);
                ItemHolder itemHolder = holderClone.GetComponent<ItemHolder>();

                itemHolder.itemName.text = SpellsShop.spellShopScript.spellItems[i].spellName;
                itemHolder.itemImg.sprite = SpellsShop.spellShopScript.spellItems[i].spellImg;
                itemHolder.type.text = SpellsShop.spellShopScript.spellItems[i].spellType.ToString();
                itemHolder.itemPrice.text = null;
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
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

    //GameObject holderClone = Instantiate(spellsHolderPrefab, gridSpells);
    //ItemHolder holdInventarioScript = holderClone.GetComponent<ItemHolder>();

    //holdInventarioScript.itemName.text = spellItems.spellName;
    //    holdInventarioScript.itemImg.sprite = spellItems.spellImg;
    //    holdInventarioScript.type.text = spellItems.spellType.ToString();
    //    holdInventarioScript.itemPrice.text = null;

    //public void AddSpellToBattle(SpellItems spellItems)
    //{
    //    for (int i = 0; i < spells.Length; i++)
    //    {
    //        if (spells[i].buyed == true)
    //        {
    //            GameObject holderClone = Instantiate(spellsHolderPrefab, gridSpells);
    //            ItemHolder itemHolder = holderClone.GetComponent<ItemHolder>();

    //            itemHolder.itemName.text = spellItems.spellName;
    //            itemHolder.itemImg.sprite = spellItems.spellImg;
    //            itemHolder.type.text = spellItems.spellType.ToString();
    //            itemHolder.itemPrice.text = null;
    //        }
    //    }
    //}
}
