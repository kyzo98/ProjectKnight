using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OrbType { VITALITY, ENDURANCE, STRENGHT, VIGOR, POWER };

[System.Serializable]
public class OrbItems {

    public string orbName;
    public OrbType orbType;
    public Sprite unbuyedOrbImg; //image to be the icon of the orb.
    public Sprite buyedOrbImg;
    public float orbPrice;

    public bool buyed;
}
