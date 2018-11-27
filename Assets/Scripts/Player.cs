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

public struct State
{
    string name;
    int turnsLeft;
};

public class Player : MonoBehaviour {

    //Estadísticas
    public Stats stats;

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
        stats.vitality = 5;
        stats.strenght = 5;
        stats.endurance = 5;
        stats.power = 5;
        stats.vigor = 5;
	}
	
	void Update () {
        
    }
}
