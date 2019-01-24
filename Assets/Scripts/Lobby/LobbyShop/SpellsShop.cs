using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsShop : MonoBehaviour {

    public static SpellsShop spellShopScript;

    public GameObject spellsShop;
    public GameObject spellHolderPrefab;
    public Transform grid;

	// Use this for initialization
	void Start () {
        spellShopScript = this;
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
