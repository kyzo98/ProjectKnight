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
        stats.vitality = PlayerPrefs.GetInt("Vitality");
        stats.strenght = PlayerPrefs.GetInt("Strenght");
        stats.endurance = PlayerPrefs.GetInt("Endurance");
        stats.power = PlayerPrefs.GetInt("Power");
        stats.vigor = PlayerPrefs.GetInt("Vigor");
        blockChance = 0;

        //STARTING HEALTH STATS
        maxHealth = stats.vitality * vitalityMultiplier;
        health = 15;
    }
	
	void Update () {
        
    }
}
