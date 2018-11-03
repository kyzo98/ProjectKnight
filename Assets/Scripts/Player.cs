using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stats
{
    int vitality;
    int strenght;
    int endurance;
    int power;
    int vigor;
};
public struct Hability
{
    string name;
    float percentage;
};
public struct Equipment
{
    string name;
    Stats stats;
    Hability hability;
};
public struct State
{
    string name;
    int turnsLeft;
};

public class Player : MonoBehaviour {
    //Estadísticas
    public Stats stats;

    //Equipamiento
    public Equipment[] equipment;

    //Habilidades
    public Hability[] habilities;

    //Estados
    public State[] states;

    //Recursos
    public int maxHealth;
    public int health;
    public int coins;
    public int maxEnergy;
    public int energy;
    public int moves;

	void Start () {

	}
	
	void Update () {
		
	}
}
