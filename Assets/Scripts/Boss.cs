﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    //Estadísticas
    public Stats stats;

    //Estados
    public State[] states;

    //Recursos
    public int maxHealth;
    public int health;

    // Use this for initialization
    void Start () {
        stats.vitality = 20;
        stats.strenght = 15;
        stats.endurance = 20;
        stats.power = 20;
        stats.vigor = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
