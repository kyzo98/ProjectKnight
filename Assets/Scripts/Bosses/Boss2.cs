using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BossStats2
{
    public float vitality;
    public float strenght;
    public float endurance;
    public float power;
    public float vigor;
    public bool charge;
};

public class Boss2 : MonoBehaviour {

    public BossStats2 stats;

    //Recursos
    public float maxHealth;
    public float health;
    public int armor;

    //BOSS BASE STATS MULTIPLIERS
    public float vitalityMultiplier = 50;
    public float strenghtMultiplier = 20;
    public float enduranceMultiplier = 12.5f;
    public float powerMultiplier = 20;
    public float vigorMultiplier = 7;

    // Use this for initialization
    void Start()
    {
        //BASE STATS
        stats.vitality = 40;
        stats.strenght = 6;
        stats.endurance = 8;
        stats.power = 8;
        stats.vigor = 12;
        stats.charge = false;

        //STATS
        maxHealth = stats.vitality * vitalityMultiplier;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
