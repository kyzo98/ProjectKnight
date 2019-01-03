using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LobbyShop : MonoBehaviour {

    public static LobbyShop store;
    public GameObject lobbyShop;
    public GameObject itemHolderPrefab;
    public Transform grid; //necesitamos esta variable para poder instanciar los holders como hijos del grid.
    //VARIABLES
    Player playerScript;
    public int space = 10;

    public OrbItems[] orbItems;
    Inventario inventario;

    void Start()
    {
        store = this;
        FillShop();
    }

    public void OpenStore()
    {
        lobbyShop.SetActive(true);
    }

    public void CloseStore()
    {
        lobbyShop.SetActive(false);
    }

    public void FillShop()
    {
        for(int i = 0; i < orbItems.Length; i++)
        {
            GameObject instHolder = Instantiate(itemHolderPrefab, grid);
            ItemHolder holderScript = instHolder.GetComponent<ItemHolder>();

            holderScript.itemName.text = orbItems[i].orbName;
            holderScript.itemPrice.text = orbItems[i].orbPrice.ToString();

            if(orbItems[i].buyed == true)
            {
                holderScript.itemImg.sprite = orbItems[i].buyedOrbImg;
            }
            else
            {
                holderScript.itemImg.sprite = orbItems[i].unbuyedOrbImg;
            }
        }
    }
}
