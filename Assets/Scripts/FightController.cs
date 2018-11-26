using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour {
    private int turn;

    //Player
    public GameObject player;
    private Player playerScript;
    public Text actionPointsText;
    public Slider playerHealthBar;
    public Text playerHealthNumber;
    public Button lightAttackButton;
    public Button heavyAttackButton;
    public Button basicHealButton;
    public Button basicMagicButton;
    public Button guardButton;
    public Button spiritBlastButton;

    //Boss
    public GameObject boss;
    private Boss bossScript;

    public Slider bossHealthBar;
    public Text bossHealthNumber;

    void Start () {
        turn = 0;
        playerScript = player.GetComponent<Player>();
        playerScript.stats.strenght = 20;
        playerScript.stats.vitality = 20;
        playerScript.stats.endurance = 20;
        playerScript.stats.vigor = 20;
        playerScript.stats.power = 20;
        bossScript = boss.GetComponent<Boss>();

        //Buttons
        lightAttackButton.onClick.AddListener(LightAttack);
        heavyAttackButton.onClick.AddListener(HeavyAttack);
        basicHealButton.onClick.AddListener(LowHealing);
        basicMagicButton.onClick.AddListener(LowMagic);
        guardButton.onClick.AddListener(Guard);
        spiritBlastButton.onClick.AddListener(SpiritBlast);

        RefreshUI();
    }
	
	
	void Update () {
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        //Debug.Log(turn%2);
        if (bossScript.health > 0 && playerScript.health > 0)
        {
            if (turn % 2 == 0)
            {
                //Player's turn
                if (playerScript.moves > 0 && playerScript.energy > 0)
                {
                    //todo PASAR A SUS PROPIOS SCRIPTS
                    if(playerScript.energy > 2)
                        lightAttackButton.interactable = true;
                    else lightAttackButton.interactable = false;
                    if (playerScript.energy > 6)
                        heavyAttackButton.interactable = true;
                    else heavyAttackButton.interactable = false;
                }
                else
                {
                    playerScript.spiritBlast += playerScript.energy;
                    Debug.Log(playerScript.spiritBlast);
                    lightAttackButton.interactable = false;
                    heavyAttackButton.interactable = false;
                    Debug.Log("Boss Turn");
                    turn++;
                }
            }
            else
            {
                //Boss's turn
                int randomChoice = Random.Range(0,2); //Selector de actuación en el turno
                switch (randomChoice)
                {
                    case 0:
                        if (playerScript.armor > 0)
                        {
                            playerScript.armor -= bossScript.stats.strenght;
                            if(playerScript.armor < 0)
                                playerScript.health += playerScript.armor;
                        }
                            
                        else
                            playerScript.health -= bossScript.stats.strenght;
                        Debug.Log("Boss ataca");
                        break;
                    case 1:
                        bossScript.health += 100;
                        if (bossScript.health >= bossScript.maxHealth) bossScript.health = bossScript.maxHealth;
                        Debug.Log("Boss se cura");
                        break;
                }
                playerScript.moves = 3;
                playerScript.energy = playerScript.maxEnergy;
                if (playerScript.armor > 0) playerScript.armor = 0;
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

    //Character Actions
    void LightAttack()
    {
        playerScript.energy -= 3;
        bossScript.health -= playerScript.stats.strenght; //normal light attack
        playerScript.moves--;
        RefreshUI();
        Debug.Log(playerScript.stats.strenght);
    }

    void HeavyAttack()
    {
        playerScript.energy -= 7;
        if(Random.Range(0,50) == 1)
            bossScript.health -= playerScript.stats.strenght * 4; //crtikal
        else
            bossScript.health -= playerScript.stats.strenght * 2; //normal heavy attack
        playerScript.moves--;
        RefreshUI();
    }

    void LowHealing()
    {
        if(playerScript.health >= playerScript.maxHealth) //caso de vida maxima igual a vida actual
        {
            //boton deshabilitado
        }
        else
        {
            playerScript.energy -= 3;
            playerScript.health += playerScript.stats.vigor; //normal healing
            playerScript.moves--;
            if (playerScript.health >= playerScript.maxHealth) playerScript.health = playerScript.maxHealth; //exceso de curación
        }
        RefreshUI();
    }

    void LowMagic()
    {
        playerScript.energy -= 3;
        bossScript.health -= playerScript.stats.power; //normal magic attack
        playerScript.moves--;
        RefreshUI();
    }

    void Guard()
    {
        playerScript.energy -= 3;
        playerScript.armor += playerScript.stats.endurance; //adding armor
        playerScript.moves--;
        RefreshUI();
    }

    void SpiritBlast()
    {
        if(playerScript.energy == 10)
        {
            if (playerScript.spiritBlast >= 10)
            {
                playerScript.energy -= 10;
                bossScript.health -= 200; //damage
                playerScript.moves--;
                playerScript.spiritBlast -= 10;
            }
            else
            {
                //boton desactivado
            }
        }
        else
        {
            //boton desactivado
        }
        RefreshUI();
    }

    // Boss Actions
    void BossLightAttack()
    {
        bossScript.energy -= 3;
        playerScript.health -= playerScript.stats.strenght; //normal light attack
        bossScript.moves--;
        RefreshUI();
    }

    void BossHeavyAttack()
    {
        bossScript.energy -= 7;
        if (Random.Range(0, 50) == 1)
            playerScript.health -= bossScript.stats.strenght * 4; //crtikal
        else
            playerScript.health -= bossScript.stats.strenght * 2; //normal heavy attack
        playerScript.moves--;
        RefreshUI();
    }

    void BossLowHealing()
    {
        if (bossScript.health >= bossScript.maxHealth) //caso de vida maxima igual a vida actual
        {
            //boton deshabilitado
        }
        else
        {
            bossScript.energy -= 3;
            bossScript.health += bossScript.stats.vigor; //normal healing
            bossScript.moves--;
            if (bossScript.health >= bossScript.maxHealth) bossScript.health = bossScript.maxHealth; //exceso de curación
        }
        RefreshUI();
    }

    void BossLowMagic()
    {
        bossScript.energy -= 3;
        playerScript.health -= playerScript.stats.power; //normal magic attack
        bossScript.moves--;
    }

    void BossGuard()
    {
        bossScript.energy -= 3;
        bossScript.armor += bossScript.stats.endurance; //adding armor
        bossScript.moves--;
    }

    void BossSpiritBlast()
    {
        if (bossScript.energy == 10)
        {
            if (bossScript.spiritBlast >= 10)
            {
                playerScript.energy -= 10;
                bossScript.health -= 200; //damage
                playerScript.moves--;
                playerScript.spiritBlast -= 10;
            }
            else
            {
                //boton desactivado
            }
        }
        else
        {
            //boton desactivado
        }
    }

    void RefreshUI()
    {
        //Energia del player
        actionPointsText.text = playerScript.energy.ToString();
        //Vida del player
        playerHealthNumber.text = playerScript.health.ToString() + '/' + playerScript.maxHealth.ToString();
        playerHealthBar.value = (float)playerScript.health / (float)playerScript.maxHealth;

        //Vida del boss
        bossHealthNumber.text = bossScript.health.ToString() + '/' + bossScript.maxHealth.ToString();
        bossHealthBar.value = (float)bossScript.health / (float)bossScript.maxHealth;
    }
}

