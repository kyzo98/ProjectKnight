using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightController : MonoBehaviour {
    private int turn;

    //PLAYER
    public GameObject player;
    private Player playerScript;
    //UI PLAYER
    public Text actionPointsText;
    public Slider playerHealthBar;
    public Slider spiritBlastCounter;
    public Text playerHealthNumber;
    public Text playerArmorNumber;
    public Button lightAttackButton;
    public Button heavyAttackButton;
    public Button basicHealButton;
    public Button basicSpellButton;
    public Button guardButton;
    public Button spiritBlastButton;
    public GameObject actionPanel;
    public Text[] combatDialogue;

    //BOSS
    public GameObject boss;
    private Boss bossScript;
    //UI BOSS
    public Slider bossHealthBar;
    public Text bossHealthNumber;
    public Text bossArmorNumber;

    //CAMERAS
    public Camera mainCamera;
    public Camera frontalPlayerCamera;

    void Start () {
        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();      

        //Buttons
        lightAttackButton.onClick.AddListener(LightAttack);
        heavyAttackButton.onClick.AddListener(HeavyAttack);
        basicHealButton.onClick.AddListener(BasicHeal);
        basicSpellButton.onClick.AddListener(BasicSpell);
        guardButton.onClick.AddListener(Guard);
        spiritBlastButton.onClick.AddListener(SpiritBlast);

        //Cameras
        mainCamera.enabled = true;
        frontalPlayerCamera.enabled = false;

        ShowActions();
        RefreshUI();
    }
	
	
	void Update () {
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();

        //Repartidor de turnos
        if (bossScript.health > 0 && playerScript.health > 0)
        {
            if (turn % 2 == 0)
            {
                //Player's turn
                if (playerScript.moves > 0 && playerScript.energy > 2) //todo Mayor que dos porque ninguna habilidad cuesta menos de 3 actualmente
                {
                    //ShowActions();                    
                }
                else
                {
                    HideActions();
                    playerScript.spiritBlast += playerScript.energy;
                    //Debug.Log(playerScript.spiritBlast);
                    //Debug.Log("Boss Turn");
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
    }



    //UI Info
    void ShowActions()
    {
        actionPanel.SetActive(true);

        //Coste 10
        if (playerScript.energy > 9)
            if (playerScript.spiritBlast >= 10) //valor de acumulación de spirit blast
            {
                spiritBlastButton.interactable = true;
            }
            else spiritBlastButton.interactable = false;

        //Coste 7
        if (playerScript.energy > 6)
            heavyAttackButton.interactable = true;
        else heavyAttackButton.interactable = false;

        //Coste 4
        if (playerScript.energy > 3)
            guardButton.interactable = true;
        else guardButton.interactable = false;

        //Coste 3
        if (playerScript.energy > 2)
        {
            lightAttackButton.interactable = true;

            //Curación cuando la vida es máxima
            if (playerScript.health >= playerScript.maxHealth)
                basicHealButton.interactable = false;
            else basicHealButton.interactable = true;

            basicSpellButton.interactable = true;
        }
        else
        {
            lightAttackButton.interactable = false;
            basicHealButton.interactable = false;
            basicSpellButton.interactable = false;
        }
    }

    void HideActions()
    {
        actionPanel.SetActive(false);
    }

    void AddCombatText()
    {
        combatDialogue[6].text = combatDialogue[5].text;
        combatDialogue[5].text = combatDialogue[4].text;
        combatDialogue[4].text = combatDialogue[3].text;
        combatDialogue[3].text = combatDialogue[2].text;
        combatDialogue[2].text = combatDialogue[1].text;
        combatDialogue[1].text = combatDialogue[0].text;
    }



    //Character Actions
    void LightAttack()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 3;
        playerScript.moves--;

        if (Random.Range(0, 20) == 1) //critico
        {
            int damage = Random.Range(playerScript.stats.strenght * 12 - 3, playerScript.stats.strenght * 12 + 3);
            StartCoroutine(BossHealthBarAnimation(damage, false));
            AddCombatText();
            combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
            combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
        }
        else //ataque normal
        {
            int damage = Random.Range(playerScript.stats.strenght * 6 - 3, playerScript.stats.strenght * 6 + 3);
            StartCoroutine(BossHealthBarAnimation(damage, false));
            AddCombatText();
            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    void HeavyAttack()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 7;
        playerScript.moves--;

        if (Random.Range(0, 7) == 1) //critico
        {
            int damage = Random.Range(playerScript.stats.strenght * 32 - 3, playerScript.stats.strenght * 32 + 3);
            StartCoroutine(BossHealthBarAnimation(damage, false));
            AddCombatText();
            combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
            combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
        }
        else //ataque normal
        {
            int damage = Random.Range(playerScript.stats.strenght * 16 - 3, playerScript.stats.strenght * 16 + 3);
            StartCoroutine(BossHealthBarAnimation(damage, false));
            AddCombatText();
            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    void BasicHeal()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 3;
        playerScript.moves--;

        int healing = playerScript.stats.vigor * 7;
        if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
        StartCoroutine(PlayerHealthBarAnimation(healing, true));

        AddCombatText();
        combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    void BasicSpell()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 3;
        playerScript.moves--;

        int damage = playerScript.stats.power * 4;
        StartCoroutine(BossHealthBarAnimation(damage, false));
        AddCombatText();
        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    void Guard()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 4;
        playerScript.moves--;

        int armored = playerScript.stats.endurance * 12;
        playerScript.armor += armored;
        AddCombatText();
        combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    void SpiritBlast()
    {
        HideActions();
        StartCoroutine(Waiter());

        playerScript.energy -= 10;
        playerScript.moves--;

        int damage = 200;
        StartCoroutine(BossHealthBarAnimation(damage, false));
        AddCombatText();
        combatDialogue[0].text = "Player used SPIRIT BLAST: " + damage.ToString() + " damge to the Boss";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
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
        spiritBlastCounter.value = playerScript.spiritBlast;

        //Vida del player
        playerHealthNumber.text = playerScript.health.ToString() + '/' + playerScript.maxHealth.ToString();
        playerArmorNumber.text = playerScript.armor.ToString();
        playerHealthBar.value = (float)playerScript.health / (float)playerScript.maxHealth;

        //Vida del boss
        bossHealthNumber.text = bossScript.health.ToString() + '/' + bossScript.maxHealth.ToString();
        bossArmorNumber.text = bossScript.armor.ToString();
        bossHealthBar.value = (float)bossScript.health / (float)bossScript.maxHealth;
    }

    //Corutina de espera
    IEnumerator Waiter()
    {
        mainCamera.enabled = !mainCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        mainCamera.enabled = !mainCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        ShowActions();
        RefreshUI();
    }

    IEnumerator BossHealthBarAnimation(int d, bool isHeal)
    {
        if (!isHeal)
            yield return new WaitForSecondsRealtime(3);
        for (int i = d; i > 0; i--)
        {
            if(isHeal)
                bossScript.health++;
            else
                bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator PlayerHealthBarAnimation(int d, bool isHeal)
    {
        if(!isHeal)
            yield return new WaitForSecondsRealtime(3);
        for (int i = d; i > 0; i--)
        {
            if (isHeal)
                playerScript.health++;
            else
                playerScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
    }
}