﻿using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        stats.vitality = 20;
        stats.strenght = 15;
        stats.endurance = 20;
        stats.power = 20;
        stats.vigor = 20;
        stats.charge = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
