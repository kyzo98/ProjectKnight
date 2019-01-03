using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BuyButton : MonoBehaviour {

    void Start()
    {

    }

    public void BuyItem()
    {
        for (int i = 0; i < LobbyShop.store.orbItems.Length; i++)
        {
            if(LobbyShop.store.orbItems[i].orbPrice <= Player.playerScript.coins && LobbyShop.store.orbItems[i].buyed == false)
            {
                LobbyShop.store.orbItems[i].buyed = true;
                Player.playerScript.coins -= LobbyShop.store.orbItems[i].orbPrice;

                Inventario.inventario.AddItem(LobbyShop.store.orbItems[i]);

                return;
            }
        }
    }
}