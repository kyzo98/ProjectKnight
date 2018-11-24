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

public struct Hability
{
    string name;
    float percentage;
};

public struct State
{
    string name;
    int turnsLeft;
};

public class Player : MonoBehaviour {

    //Estadísticas
    public Stats stats;

    //Habilidades
    public Hability[] habilities;

    //Estados
    public State[] states;

    //Recursos
    public int maxHealth;
    public int health;
    public int armor;
    public int coins;
    public int maxEnergy;
    public int energy;
    public int moves;
    public int spiritBlast;

	void Start () {
        stats.vitality = 20;
        stats.strenght = 20;
        stats.endurance = 20;
        stats.power = 20;
        stats.vigor = 20;
	}
	
	void Update () {
		
	}
}
