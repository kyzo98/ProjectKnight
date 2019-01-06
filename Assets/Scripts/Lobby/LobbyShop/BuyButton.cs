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
        for (int i = 0; i < SpellsShop.spellShopScript.spellItems.Length; i++)
        {
            if(SpellsShop.spellShopScript.spellItems[i].spellPrice <= Player.playerScript.coins && SpellsShop.spellShopScript.spellItems[i].buyed == false)
            {
                SpellsShop.spellShopScript.spellItems[i].buyed = true;
                Player.playerScript.coins -= SpellsShop.spellShopScript.spellItems[i].spellPrice;

                Inventario.inventario.AddSpell(SpellsShop.spellShopScript.spellItems[i]);

                return;
            }
        }
    }
}