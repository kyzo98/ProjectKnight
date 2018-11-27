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
    public Text playerArmorNumber;
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
    public Text bossArmorNumber;

    void Start () {
        turn = 0;
        playerScript = player.GetComponent<Player>();
        playerScript.stats.strenght = 5;
        playerScript.stats.vitality = 5;
        playerScript.stats.endurance = 5;
        playerScript.stats.vigor = 5;
        playerScript.stats.power = 5;
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
                if (playerScript.moves > 0 && playerScript.energy > 2)
                {
                    //todo PASAR A SUS PROPIOS SCRIPTS
                    if (playerScript.energy > 6)
                        heavyAttackButton.interactable = true;
                    else heavyAttackButton.interactable = false;                        
                    if (playerScript.health >= playerScript.maxHealth) //caso de vida maxima igual a vida actual
                    {
                        basicHealButton.interactable = false;
                    }
                    else
                    {
                        basicHealButton.interactable = true;
                    }
                    }
                else
                {
                    playerScript.spiritBlast += playerScript.energy;
                    Debug.Log(playerScript.spiritBlast);
                    Debug.Log("Boss Turn");
                    turn++;
                }
            }
            else
            {
                //Boss's turn
                int randomChoice = Random.Range(0,5); //Selector de actuación en el turno
                switch (randomChoice)
                {
                    case 0:
                        BossLowHealing();
                        break;
                    case 1:
                        BossLightAttack();
                        break;
                    case 2:
                        BossHeavyAttack();
                        break;
                    case 3:
                        BossLowMagic();
                        break;
                    case 4:
                        BossGuard();
                        break;
                    case 5:
                        BossSpiritBlast();
                        break;
                }
                playerScript.moves = 3;
                playerScript.energy = playerScript.maxEnergy;
                if (playerScript.armor > 0) playerScript.armor = 0;
                if (bossScript.armor > 0) bossScript.armor = 0;
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
        if (Random.Range(0, 20) == 1)
            bossScript.health -= playerScript.stats.strenght * 12; //crtikal
        else
            bossScript.health -= playerScript.stats.strenght * 6; //normal light attack

        playerScript.moves--;
        RefreshUI();
        Debug.Log(playerScript.stats.strenght);
    }

    void HeavyAttack()
    {
        playerScript.energy -= 7;
        if(Random.Range(0,7) == 1)
            bossScript.health -= playerScript.stats.strenght * 32; //crtikal
        else
            bossScript.health -= playerScript.stats.strenght * 16; //normal heavy attack

        playerScript.moves--;
        RefreshUI();
    }

    void LowHealing()
    {
        if(playerScript.health >= playerScript.maxHealth) //caso de vida maxima igual a vida actual
        {
            
        }
        else
        {
            playerScript.energy -= 3;
            playerScript.health += playerScript.stats.vigor * 7; //normal healing
            playerScript.moves--;
            if (playerScript.health >= playerScript.maxHealth) playerScript.health = playerScript.maxHealth; //exceso de curación
        }
        RefreshUI();
    }

    void LowMagic()
    {
        playerScript.energy -= 3;
        bossScript.health -= playerScript.stats.power * 4; //normal magic attack
        playerScript.moves--;
        RefreshUI();
    }

    void Guard()
    {
        playerScript.energy -= 4;
        playerScript.armor += playerScript.stats.endurance * 10; //adding armor
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
        //bossScript.energy -= 1;
        playerScript.health -= playerScript.stats.strenght; //normal light attack
        bossScript.moves--;
        RefreshUI();
    }

    void BossHeavyAttack()
    {
        //bossScript.energy -= 4;
        if (Random.Range(0, 50) == 1)
        {
            playerScript.health -= bossScript.stats.strenght * 4; //crtikal
        }
        else
        {
            playerScript.health -= bossScript.stats.strenght * 2; //normal heavy attack
        }
        playerScript.moves--;
        RefreshUI();
    }

    void BossLowHealing()
    {
        //bossScript.energy -= 1;
        bossScript.health += bossScript.stats.vigor; //normal healing
        bossScript.moves--;
        if (bossScript.health >= bossScript.maxHealth) bossScript.health = bossScript.maxHealth; //exceso de curación
        RefreshUI();
    }

    void BossLowMagic()
    {
        //bossScript.energy -= 1;
        playerScript.health -= playerScript.stats.power; //normal magic attack
        bossScript.moves--;
        RefreshUI();
    }

    void BossGuard()
    {
        //bossScript.energy -= 1;
        bossScript.armor += bossScript.stats.endurance; //adding armor
        bossScript.moves--;
        RefreshUI();
    }

    void BossSpiritBlast()
    {
        playerScript.health -= 200; //damage
        bossScript.moves--;
        bossScript.spiritBlast -= 10;
        RefreshUI();
    }

    void RefreshUI()
    {
        //Energia del player
        actionPointsText.text = playerScript.energy.ToString();
        //Vida del player
        playerHealthNumber.text = playerScript.health.ToString() + '/' + playerScript.maxHealth.ToString();
        playerArmorNumber.text = playerScript.armor.ToString();
        playerHealthBar.value = (float)playerScript.health / (float)playerScript.maxHealth;

        //Vida del boss
        bossHealthNumber.text = bossScript.health.ToString() + '/' + bossScript.maxHealth.ToString();
        bossArmorNumber.text = bossScript.armor.ToString();
        bossHealthBar.value = (float)bossScript.health / (float)bossScript.maxHealth;
    }
}

