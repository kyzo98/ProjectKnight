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

    //BOSS ANIMATIONS
    private Animator bossAnimator;

    //CAMERAS
    public Camera mainCamera;
    public Camera frontalPlayerCamera;

    private bool endedMove = true;
    private bool bossEndedMove = true;

    void Start () {
        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        bossAnimator = boss.GetComponent<Animator>();

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
                if (bossEndedMove)
                {
                    //Player's turn
                    if (playerScript.moves == 3 && playerScript.armor > 0)//si tenia armadura equipada se retira ya que solo dura 1 turno
                    {
                        playerScript.armor = 0;
                        RefreshUI();
                    } 
                    
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
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
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
        for (int i = d; i > 0; i--)
        {
            playerScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
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
        ShowActions();
        RefreshUI();
    }



    // Boss Actions
    void BossMeleeAtack()
    {
        HideActions();
        bossAnimator.SetTrigger("MeleeAnim");

        int damage = Random.Range(bossScript.stats.strenght * 2 - 3, bossScript.stats.strenght * 2 + 3);
        StartCoroutine(BossMeleeAtackWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damge to you";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator BossMeleeAtackWaiter(int d)
    {
        bossEndedMove = false;
        //todo cambiar a camara de boss si queremos frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        //todo TRIGER animacion de recibir daño del player
        //todo yield return new  WaitForSecondsRealtime(tiempo de esa animacion);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; esta va con la primera linea
        if(playerScript.armor > 0)
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
        //todo cambiar a camara de boss si queremos frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        //todo TRIGER animacion de recibir daño del player
        //todo yield return new  WaitForSecondsRealtime(tiempo de esa animacion);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; esta va con la primera linea

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
        //todo cambiar a camara de boss si queremos frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        //todo TRIGER animacion de recibir daño del player
        //todo yield return new  WaitForSecondsRealtime(tiempo de esa animacion);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; esta va con la primera linea
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