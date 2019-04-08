using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BossStats
{
    public float vitality;
    public float strenght;
    public float endurance;
    public float power;
    public float vigor;
    public bool charge;
};

public class Boss : MonoBehaviour {

    public static Boss bossScript;
    //Estadísticas
    public BossStats stats;

    //Recursos
    public float maxHealth;
    public float health;
    public float armor;

    //BOSS BASE STATS MULTIPLIERS
    public float vitalityMultiplier = 50;
    public float strenghtMultiplier = 20;
    public float enduranceMultiplier = 12.5f;
    public float powerMultiplier = 20;
    public float vigorMultiplier = 7;

    // Use this for initialization
    void Start () {
        //BASE STATS
        stats.vitality = 20;
        stats.strenght = 3;
        stats.endurance = 5;
        stats.power = 4;
        stats.vigor = 10;
        stats.charge = false;

        //STATS
        maxHealth = stats.vitality * vitalityMultiplier;
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
