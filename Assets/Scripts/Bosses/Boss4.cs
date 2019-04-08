using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BossStats4
{
    public float vitality;
    public float strenght;
    public float endurance;
    public float power;
    public float vigor;
    public bool charge;
};

public class Boss4 : MonoBehaviour {

    public BossStats3 stats;

    //Recursos
    public float maxHealth;
    public float health;
    public int armor;

    //BOSS BASE STATS MULTIPLIERS
    public int vitalityMultiplier = 50;
    public int strenghtMultiplier = 20;
    public int enduranceMultiplier = 25;
    public int powerMultiplier = 20;
    public int vigorMultiplier = 7;

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
