using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellType { SPELL, DRIVE};
public enum AttackType { RAGE, TERROR, GRIEF};

[System.Serializable]
public class SpellItems {

    public static SpellItems spellItems;

    public string spellName;
    public SpellType spellType;
    public AttackType attackType;
    public string spellDescription;
    public Sprite spellImg;
    public float spellPrice;

    public bool buyed;

	// Use this for initialization
	void Start () {
	    	
	}

    // Update is called once per frame
    void Update () {
		
	}
}
