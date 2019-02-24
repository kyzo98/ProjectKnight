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
    public int maxHealth;
    public int health;
    public int armor;
    public float coins = 700;
    public int maxEnergy;
    public int energy;
    public int moves;
    public int spiritBlast;
    public int blockChance;

	void Start () {
        playerScript = this;

        stats.vitality = 5;
        stats.strenght = 5;
        stats.endurance = 5;
        stats.power = 5;
        stats.vigor = 5;
        blockChance = 0;

    }
	
	void Update () {
        
    }
}
