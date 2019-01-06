using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpellButton : MonoBehaviour {

    private bool used;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PressUseButton()
    {
        for (int i = 0; i < SpellsShop.spellShopScript.spellItems.Length; i++)
        {
            if (SpellsShop.spellShopScript.spellItems[i].spellName == "Grief" && SpellsShop.spellShopScript.spellItems[i].buyed == true)
            {
                InventoryBattle.inventario.inventoryActive = !InventoryBattle.inventario.inventoryActive;
                FightController.fightController.GriefSpell();

                SpellsShop.spellShopScript.spellItems[i].buyed = false;

                break;
            }
            else if (SpellsShop.spellShopScript.spellItems[i].spellName == "Rage" && SpellsShop.spellShopScript.spellItems[i].buyed == true)
            {
                InventoryBattle.inventario.inventoryActive = !InventoryBattle.inventario.inventoryActive;
                FightController.fightController.RageSpell();

                SpellsShop.spellShopScript.spellItems[i].buyed = false;

                break;
            }
            else if (SpellsShop.spellShopScript.spellItems[i].spellName == "Terror" && SpellsShop.spellShopScript.spellItems[i].buyed == true)
            {
                InventoryBattle.inventario.inventoryActive = !InventoryBattle.inventario.inventoryActive;
                FightController.fightController.TerrorSpell();

                SpellsShop.spellShopScript.spellItems[i].buyed = false;

                break;
            }
        }
    }
}
