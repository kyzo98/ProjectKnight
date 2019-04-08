using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BossStats3
{
    public float vitality;
    public float strenght;
    public float endurance;
    public float power;
    public float vigor;
    public bool charge;
};

public class Boss3 : MonoBehaviour
{

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
        stats.vitality = 60;
        stats.strenght = 9;
        stats.endurance = 9;
        stats.power = 14;
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
