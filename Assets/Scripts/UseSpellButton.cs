using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSpellButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void PressUseButton()
    {
        if (SpellItems.spellItems.attackType == AttackType.GRIEF)
        {
            UseGrief();
        }
        else if (SpellItems.spellItems.attackType == AttackType.RAGE)
        {
            UseRage();
        }
        else if (SpellItems.spellItems.attackType == AttackType.TERROR)
        {
            UseTerror();
        }
    }

    public void UseTerror()
    {

    }

    public void UseRage()
    {

    }

    public void UseGrief()
    {

    }
}
