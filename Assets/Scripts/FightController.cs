using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightController : MonoBehaviour {
    enum Buff { NULL }
    enum Debuff { NULL }

    private int turn;

    //GAMEOBJECTS
    public GameObject armorEffect;

    //PLAYER
    public GameObject player;
    private Player playerScript;
    private Buff[] playerBuff;
    private Debuff[] playerDebuff;
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

    //BOSS ANIMATIONS
    private Animator bossAnimator;

    //PLAYER ANIMATIONS
    private Animator playerAnimator;

    //CAMERAS
    public Camera mainCamera;
    public Camera frontalPlayerCamera;
    public Camera frontalBossCamera;

    private bool endedMove = true;
    private bool bossEndedMove = true;

    void Start () {
        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        bossAnimator = boss.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();

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
        frontalBossCamera.enabled = false;

        //Buffs & Debuffs
        playerBuff = new Buff[2];
        playerBuff[0] = Buff.NULL;
        playerBuff[1] = Buff.NULL;
        playerDebuff = new Debuff[2];
        playerDebuff[0] = Debuff.NULL;
        playerDebuff[1] = Debuff.NULL;

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
                if (bossEndedMove)
                {
                    //Player's turn
                    if (playerScript.moves == 3 && playerScript.armor > 0)//si tenia armadura equipada se retira ya que solo dura 1 turno
                    {
                        playerScript.armor = 0;
                        RefreshUI();
                    } 
                    if(playerScript.armor == 0)
                        armorEffect.SetActive(false);

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
            }
            else
            {
                if (endedMove)
                {
                    //Boss's turn
                    if (bossScript.health < bossScript.maxHealth) //Si tiene vida reducida tiee posibilidad de tirar curas
                    {
                        int randomChoice = Random.Range(0, 3); //Selector de actuación en el turno
                        switch (randomChoice)
                        {
                            case 0:
                                BossMeleeAtack();
                                Debug.Log("Melee Atack");
                                break;
                            case 1:
                                BossMagicSpell();
                                Debug.Log("Magic Atack");
                                break;
                            case 2:
                                BossHealing();
                                Debug.Log("Healing");
                                break;
                        }
                    }
                    else
                    {
                        int randomChoice = Random.Range(0, 2); //Selector de actuación en el turno
                        switch (randomChoice)
                        {
                            case 0:
                                BossMeleeAtack();
                                Debug.Log("Melee Atack");
                                break;
                            case 1:
                                BossMagicSpell();
                                Debug.Log("Magic Atack");
                                break;
                        }
                    }

                    playerScript.moves = 3;
                    playerScript.energy = playerScript.maxEnergy;
                    turn++;
                }
            }
        }
        else
        {
            HideActions();
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

        playerScript.energy -= 3;
        playerScript.moves--;
        
        if (Random.Range(0, 20) == 1) //critico
        {
            int damage = Random.Range(playerScript.stats.strenght * 12 - 3, playerScript.stats.strenght * 12 + 3);
            StartCoroutine(LightAttackWaiter(damage));
            AddCombatText();
            combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
            combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
        }
        else //ataque normal
        {
            int damage = Random.Range(playerScript.stats.strenght * 6 - 3, playerScript.stats.strenght * 6 + 3);
            StartCoroutine(LightAttackWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator LightAttackWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        bossAnimator.SetTrigger("HeadHit");
        yield return new WaitForSecondsRealtime(2);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void HeavyAttack()
    {
        HideActions();

        playerScript.energy -= 7;
        playerScript.moves--;

        if (Random.Range(0, 7) == 1) //critico
        {
            int damage = Random.Range(playerScript.stats.strenght * 32 - 3, playerScript.stats.strenght * 32 + 3);
            StartCoroutine(HeavyAttackWaiter(damage));
            AddCombatText();
            combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
            combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
        }
        else //ataque normal
        {
            int damage = Random.Range(playerScript.stats.strenght * 16 - 3, playerScript.stats.strenght * 16 + 3);
            StartCoroutine(HeavyAttackWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator HeavyAttackWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        bossAnimator.SetTrigger("HeadHit");
        yield return new WaitForSecondsRealtime(2);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void BasicHeal()
    {
        HideActions();

        playerScript.energy -= 3;
        playerScript.moves--;

        int healing = playerScript.stats.vigor * 7;
        if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
        StartCoroutine(BasicHealWaiter(healing));

        AddCombatText();
        combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator BasicHealWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        for (int i = d; i > 0; i--)
        {
            playerScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void BasicSpell()
    {
        HideActions();

        playerScript.energy -= 3;
        playerScript.moves--;

        int damage = playerScript.stats.power * 4;
        StartCoroutine(BasicSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator BasicSpellWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        bossAnimator.SetTrigger("HeadHit");
        yield return new WaitForSecondsRealtime(2);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void Guard()
    {
        HideActions();

        playerScript.energy -= 4;
        playerScript.moves--;

        int armored = playerScript.stats.endurance * 3;
        StartCoroutine(GuardWaiter(armored));
        AddCombatText();
        combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator GuardWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        playerScript.blockChance += 5;
        for (int i = d; i > 0; i--)
        {
            playerScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        armorEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(1); //Tiempo de espera de la animación        
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void SpiritBlast()
    {
        HideActions();

        playerScript.energy -= 10;
        playerScript.moves--;

        int damage = 200;
        StartCoroutine(SpiritBlastWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used SPIRIT BLAST: " + damage.ToString() + " damge to the Boss";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator SpiritBlastWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        //todo TRIGER animacion de recibir daño
        //todo yield return new  WaitForSecondsRealtime(tiempo de esa animacion);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }



    // Boss Actions
    void BossMeleeAtack()
    {
        HideActions();
        bossAnimator.SetTrigger("MeleeAnim");

        if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
        {
            AddCombatText();
            combatDialogue[0].text = "Attack Blocked";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(PlayerBlocked());
        }
        else
        {
            int damage = Random.Range(bossScript.stats.strenght * 2 - 3, bossScript.stats.strenght * 2 + 3);
            StartCoroutine(BossMeleeAtackWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }
    IEnumerator PlayerBlocked()
    {
        bossEndedMove = false;
        playerScript.blockChance = 0;
        //Animacion de bloqueo con cambios de camara y demas
        yield return new WaitForSecondsRealtime(2);
        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    IEnumerator BossMeleeAtackWaiter(int d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        yield return new WaitForSecondsRealtime(2);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

       
        if(playerScript.blockChance >= Random.Range(0, 99))
        {

        }
        else
        if (playerScript.armor > 0)
        {
            if(d <= playerScript.armor)
            {
                for (int i = d; i > 0; i--)
                {
                    playerScript.armor--;
                    RefreshUI();
                    yield return 0;
                    yield return new WaitForSeconds(0);
                }
            }
            else
            {
                d -= playerScript.armor;
                for (int i = playerScript.armor; i > 0; i--)
                {
                    playerScript.armor--;
                    RefreshUI();
                    yield return 0;
                    yield return new WaitForSeconds(0);
                }
                for (int i = d; i > 0; i--)
                {
                    playerScript.health--;
                    RefreshUI();
                    yield return 0;
                    yield return new WaitForSeconds(0);
                }
            }
        }
        else
        {
            for (int i = d; i > 0; i--)
            {
                playerScript.health--;
                RefreshUI();
                yield return 0;
                yield return new WaitForSeconds(0);
            }
        }        
        
        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void BossHealing()
    {
        HideActions();
        bossAnimator.SetTrigger("HealAnim");

        int healing = bossScript.stats.vigor * 2;
        StartCoroutine(BossHealingWaiter(healing));
        AddCombatText();
        combatDialogue[0].text = "Boss healed himself for " + healing.ToString() + " HP";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator BossHealingWaiter(int d)
    {
        bossEndedMove = false;
        for (int i = d; i > 0; i--)
        {
            bossScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void BossMagicSpell()
    {
        HideActions();
        bossAnimator.SetTrigger("SpellCasting");

        int damage = bossScript.stats.power * 3;
        StartCoroutine(BossMagicSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Boss dealt " + damage.ToString() + " magic damage to you";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator BossMagicSpellWaiter(int d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(0.5f); //Tiempo de espera de la animación
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        for (int i = d; i > 0; i--)
        {
            playerScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

        bossEndedMove = true;
        ShowActions();
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
        bossHealthBar.value = (float)bossScript.health / (float)bossScript.maxHealth;
    }


    //Corutina de espera
    //IEnumerator Waiter()
    //{
    //    frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
    //    yield return new WaitForSecondsRealtime(3);
    //    bossAnimator.ResetTrigger("HealAnimation");
    //    bossAnimator.ResetTrigger("");
    //    bossAnimator.ResetTrigger("");
    //    bossAnimator.SetTrigger("");
    //    frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
    //    ShowActions();
    //    RefreshUI();
    //}

    //IEnumerator BossHealthBarAnimation(int d, bool isHeal)
    //{
    //    if (!isHeal)
    //        yield return new WaitForSecondsRealtime(3);
    //    for (int i = d; i > 0; i--)
    //    {
    //        if(isHeal)
    //            bossScript.health++;
    //        else
    //            bossScript.health--;
    //        RefreshUI();
    //        yield return 0;
    //        yield return new WaitForSeconds(0);
    //    }
    //}
    
    //IEnumerator PlayerHealthBarAnimation(int d, bool isHeal)
    //{
    //    if(!isHeal)
    //        yield return new WaitForSecondsRealtime(3);
    //    for (int i = d; i > 0; i--)
    //    {
    //        if (isHeal)
    //            playerScript.health++;
    //        else
    //            playerScript.health--;
    //        RefreshUI();
    //        yield return 0;
    //        yield return new WaitForSeconds(0);
    //    }
    //}
}