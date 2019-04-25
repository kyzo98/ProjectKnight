using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stats
{
    public int vitality;
    public int strenght;
    public int endurance;
    public int power;
    public int vigor;
};

public class Player : MonoBehaviour {

    public static Player playerScript;

    //Estadísticas
    public Stats stats;

    //Recursos
    public float maxHealth;
    public float health;
    public int armor;
    public int coins;
    public int maxEnergy;
    public int energy;
    public int moves;
    public int spiritBlast;
    public int blockChance;

    //BASE STATS MULTIPLIER
    public int vitalityMultiplier = 50;
    public float enduranceMultiplier = 12.5f;
    public int strenghtMultiplier = 20;
    public int vigorMultiplier = 7;
    public int powerMultiplier = 20;

	void Awake () {
        coins = PlayerPrefs.GetInt("COINS");
        playerScript = this;

        //BASE STATS
        stats.vitality = 5;
        stats.strenght = 5;
        stats.endurance = 5;
        stats.power = 5;
        stats.vigor = 5;
        blockChance = 0;

        //STATS
        maxHealth = stats.vitality * vitalityMultiplier;
        health = maxHealth;
    }
	
	void Update () {
        
    }
}
