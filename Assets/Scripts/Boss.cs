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
    //Estadísticas
    public BossStats stats;

    //Estados
    public State[] states;

    //Recursos
    public int maxHealth;
    public int health;
    public int armor;

    // Use this for initialization
    void Start () {
        stats.vitality = 20;
        stats.strenght = 15;
        stats.endurance = 20;
        stats.power = 20;
        stats.vigor = 20;
        stats.charge = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
