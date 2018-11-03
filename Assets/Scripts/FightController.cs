using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour {
    private int turn;

    //Player
    public GameObject player;
    private Player playerScript;

    public Slider playerEnergyBar;
    public Slider playerHealthBar;
    public Text playerHealthNumber;
    public Button lightAttackButton;
    public Button heavyAttackButton;

    //Boss
    public GameObject boss;
    private Boss bossScript;

    public Slider bossHealthBar;
    public Text bossHealthNumber;

    void Start () {
        turn = 0;
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();

        //Buttons
        lightAttackButton.onClick.AddListener(LightAttack);
        heavyAttackButton.onClick.AddListener(HeavyAttack);

        RefreshUI();
    }
	
	
	void Update () {
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        Debug.Log(turn%2);
        if (bossScript.health > 0 && playerScript.health > 0)
        {
            if (turn % 2 == 0)
            {
                //Player's turn
                if (playerScript.moves > 0 && playerScript.energy > 0)
                {
                    if(playerScript.energy > 2)
                        lightAttackButton.interactable = true;
                    else lightAttackButton.interactable = false;
                    if (playerScript.energy > 6)
                        heavyAttackButton.interactable = true;
                    else heavyAttackButton.interactable = false;
                }
                else
                {
                    lightAttackButton.interactable = false;
                    heavyAttackButton.interactable = false;
                    Debug.Log("Boss Turn");
                    turn++;
                }
            }
            else
            {
                int randomChoice = Random.Range(0,2); 
                switch (randomChoice)
                {
                    case 0:
                        playerScript.health -= 30;
                        Debug.Log("Boss ataca");
                        break;
                    case 1:
                        bossScript.health += 100;
                        Debug.Log("Boss se cura");
                        break;
                }
                playerScript.moves = 3;
                playerScript.energy = playerScript.maxEnergy;
                turn++;
            }
        }
        else
        {
            lightAttackButton.interactable = false;
            heavyAttackButton.interactable = false;
            if (bossScript.health > 0)
            {
                //Boss gana
                Debug.Log("Boss gana");
            }
            else
            {
                //Player gana
                Debug.Log("Player gana");
            }
        }

        RefreshUI();
    }

    void LightAttack()
    {
        playerScript.energy -= 3;
        bossScript.health -= 42;
        playerScript.moves--;
    }

    void HeavyAttack()
    {
        playerScript.energy -= 7;
        bossScript.health -= 96;
        playerScript.moves--;
    }

    void RefreshUI()
    {
        //Energia del player
        playerEnergyBar.value = 10 - playerScript.energy;
        //Vida del player
        playerHealthNumber.text = playerScript.health.ToString() + '/' + playerScript.maxHealth.ToString();
        playerHealthBar.value = 1 - (float)playerScript.health / (float)playerScript.maxHealth;

        //Vida del boss
        bossHealthNumber.text = bossScript.health.ToString() + '/' + bossScript.maxHealth.ToString();
        bossHealthBar.value = (float)bossScript.health / (float)bossScript.maxHealth;
    }
}

