using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour {

    public static Inventario inventario;

    public GameObject inventoryHolderPrefab;
    public GameObject inventory;
    public Transform grid;

    private int space = 3;

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

    public void AddItem(OrbItems orbItem)
    {
        GameObject holderClone = Instantiate(inventoryHolderPrefab, grid);
        HolderInventario holdInventarioScript = holderClone.GetComponent<HolderInventario>();

        holdInventarioScript.itemName.text = orbItem.orbName;
        holdInventarioScript.itemImg.sprite = orbItem.unbuyedOrbImg;

        //for (int i = 0; i < LobbyShop.store.orbItems.Length; i++)
        //{
        //    holdInventarioScript.itemName.text = LobbyShop.store.orbItems[i].orbName;
        //    holdInventarioScript.itemImg.sprite = LobbyShop.store.orbItems[i].unbuyedOrbImg;
        //}
    }
}
