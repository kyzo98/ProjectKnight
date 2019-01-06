using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsShop : MonoBehaviour {

    public static SpellsShop spellShopScript;

    public GameObject spellsShop;
    public GameObject spellHolderPrefab;
    public Transform grid;

    public SpellItems[] spellItems;


	// Use this for initialization
	void Start () {
        spellShopScript = this;
        FillShopSpells();
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void FillShopSpells()
    {
        for (int i = 0; i < spellItems.Length; i++)
        {
            GameObject instHolder = Instantiate(spellHolderPrefab, grid);
            ItemHolder holderScript = instHolder.GetComponent<ItemHolder>();

            holderScript.itemName.text = spellItems[i].spellName;
            holderScript.itemPrice.text = spellItems[i].spellPrice.ToString();
            holderScript.description.text = spellItems[i].spellDescription;
            holderScript.itemImg.sprite = spellItems[i].spellImg;
            holderScript.type.text = spellItems[i].spellType.ToString();
        }
    }
}
