using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightController : MonoBehaviour {

    public static FightController fightController;

    enum BuffType { NULL }
    enum DebuffType { NULL }
    struct Buff
    {
        public BuffType type;
        public int remainingTurns;
    }
    struct Debuff
    {
        public DebuffType type;
        public int remainingTurns;
    }
    //Effects the bosses will make to the player
    public enum Effects { NULL, GRIEF, NUMB, PARALISIS};
    public Effects actualEffectPlayer;

    private int turn;
    private int nAttack;
    private int nAttack2;
    private int nAttack3;

    //GAMEOBJECTS
    public GameObject armorEffect;
    public GameObject healEffect;
    public GameObject magicSpell;
    public GameObject pauseMenu;

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
    //PLAYER ANIMATIONS
    private Animator playerAnimator;

    //BOSS
    public GameObject boss;
    private Boss bossScript;
    //UI BOSS
    public Slider bossHealthBar;
    public Text bossHealthNumber;
    public Text bossArmorNumber;
    //POPUP TEXT UI BOSS
    public GameObject popupText;
    //POPUP TEXT UI PLAYER
    public GameObject popupTextPlayer;
    //BOSS ANIMATIONS
    //Animator bossAnimator;
    //PARTICLE ANIMATIONS
    //public Animation particleAnimator;
    //CAMERAS
    public Camera mainCamera;
    public Camera frontalPlayerCamera;
    public Camera frontalBossCamera;

    private bool endedMove = true;
    private bool bossEndedMove = true;

    void Start () {
        fightController = this;
        actualEffectPlayer = Effects.NULL;

        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        //bossAnimator = boss.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        //particleAnimator = magicSpell.GetComponent<Animation>();

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
        playerBuff[0].type = BuffType.NULL;
        playerBuff[0].remainingTurns = 0;
        playerBuff[1].type = BuffType.NULL;
        playerBuff[1].remainingTurns = 0;
        playerDebuff = new Debuff[2];
        playerDebuff[0].type = DebuffType.NULL;
        playerDebuff[0].remainingTurns = 0;
        playerDebuff[1].type = DebuffType.NULL;
        playerDebuff[1].remainingTurns = 0;

        ShowActions();
        RefreshUI();
    }
	
	
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();

        //Repartidor de turnos
        if (bossScript.health > 0 && playerScript.health > 0)
        {
            if (turn % 2 == 0)
            {
                if (bossEndedMove)
                {
                    if (actualEffectPlayer == Effects.GRIEF)
                    {
                        ApplyGrief();
                        if(endedMove == true)
                        {
                            actualEffectPlayer = Effects.NULL;
                        }
                    }
                    else if (actualEffectPlayer == Effects.NUMB)
                    {
                        ApplyNumb();
                        if(endedMove == true)
                        {
                            actualEffectPlayer = Effects.NULL;
                        }
                    }
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
                    actualEffectPlayer = Effects.NULL;
                    //Boss's turn

                    if(bossScript.health >= 700)
                    {
                        nAttack++;
                        Debug.Log(nAttack);
                        switch (nAttack)
                        {
                            case 1:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 2:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 3:
                                GuardBoss();
                                Debug.Log("Boss used Guard.");
                                //nAttack++;
                                break;
                            case 4:
                                Heal();
                                Debug.Log("Boss healed himself.");
                                //nAttack++;
                                break;
                            case 5:
                                GuardBoss();
                                Debug.Log("Boss used Guard.");
                                //nAttack++;
                                break;
                            case 6:
                                Attack();
                                Debug.Log("Boss used normal Attack.");
                                //nAttack++;
                                break;
                            case 7:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 8:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 9:
                                Heal();
                                Debug.Log("Boss healed himself.");
                                //nAttack++;
                                break;
                            case 10:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 11:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 12:
                                GuardBoss();
                                Debug.Log("Boss used Guard.");
                                //nAttack++;
                                break;
                            case 13:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 14:
                                AttackPlus();
                                Debug.Log("Boss used attack plus.");
                                //nAttack++;
                                break;
                            case 15:
                                Heal();
                                Debug.Log("Boss healed himself.");
                                //nAttack++;
                                break;
                            case 16:
                                GuardBoss();
                                Debug.Log("Boss used Guard.");
                                //nAttack++;
                                break;
                            case 17:
                                Attack();
                                Debug.Log("Boss used attack.");
                                //nAttack++;
                                break;
                            case 18:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                nAttack = 0;
                                break;
                        }
                    }
                    else if(bossScript.health >= 250 && bossScript.health <= 700)
                    {
                        nAttack2++;
                        Debug.Log(nAttack2);
                        switch (nAttack2)
                        {
                            case 1:
                                AttackPlus();
                                Debug.Log("Boss used attack plus.");
                                //nAttack++;
                                break;
                            case 2:
                                EffectAttack();
                                Debug.Log("Boss used effect attack.");
                                //nAttack++;
                                break;
                            case 3:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 4:
                                HealPlus();
                                Debug.Log("Boss used healed himself plus.");
                                //nAttack++;
                                break;
                            case 5:
                                GuardBoss();
                                Debug.Log("Boss used guard.");
                                //nAttack++;
                                break;
                            case 6:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 7:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 8:
                                EffectAttack();
                                Debug.Log("Boss used effect attack");
                                //nAttack++;
                                break;
                            case 9:
                                GuardBoss();
                                Debug.Log("Boss used guard");
                                //nAttack++;
                                break;
                            case 10:
                                HealPlus();
                                Debug.Log("boss used heal plus.");
                                //nAttack++;
                                break;
                            case 11:
                                AttackPlus();
                                Debug.Log("boss used attack plus.");
                                //nAttack++;
                                break;
                            case 12:
                                GuardBoss();
                                Debug.Log("Boss used guard");
                                //nAttack++;
                                break;
                            case 13:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 14:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 15:
                                GuardBoss();
                                Debug.Log("Boss used guard.");
                                //nAttack++;
                                break;
                            case 16:
                                Heal();
                                Debug.Log("Boss healed himself");
                                //nAttack++;
                                break;
                            case 17:
                                EffectAttack();
                                Debug.Log("Boss used effect attack.");
                                //nAttack++;
                                break;
                            case 18:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                nAttack2 = 0;
                                break;
                        }
                    }
                    else if(bossScript.health >= 0 && bossScript.health <= 250)
                    {
                        nAttack3++;
                        Debug.Log(nAttack3);
                        switch (nAttack3)
                        {
                            case 1:
                                ChargeAttack();
                                Debug.Log("Boss charged his big attack.");
                                //nAttack++;
                                break;
                            case 2:
                                SpecialAttack();
                                Debug.Log("Boss used special attack. UUU that hurts.");
                                //nAttack++;
                                break;
                            case 3:
                                GuardBoss();
                                Debug.Log("Boss used guard.");
                                //nAttack++;
                                break;
                            case 4:
                                Attack();
                                Debug.Log("Boss used normal attack.");
                                //nAttack++;
                                break;
                            case 5:
                                GuardBoss();
                                Debug.Log("Boss used guard.");
                                //nAttack++;
                                break;
                            case 6:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 7:
                                EffectAttack();
                                Debug.Log("boss used effect attack.");
                                //nAttack++;
                                break;
                            case 8:
                                GuardBoss();
                                Debug.Log("Boss used guard.");
                                //nAttack++;
                                break;
                            case 9:
                                Attack();
                                Debug.Log("Boss used attack.");
                                //nAttack++;
                                break;
                            case 10:
                                AttackPlus();
                                Debug.Log("Boss used attack plus");
                                //nAttack++;
                                break;
                            case 11:
                                HealPlus();
                                Debug.Log("Boss used heal plus.");
                                //nAttack++;
                                break;
                            case 12:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                //nAttack++;
                                break;
                            case 13:
                                Attack();
                                Debug.Log("Boss used attack.");
                                //nAttack++;
                                break;
                            case 14:
                                ChargeAttack();
                                Debug.Log("Boss charged his big attack. Prepare to die bitch.");
                                //nAttack++;
                                break;
                            case 15:
                                SpecialAttack();
                                Debug.Log("boss used his special attack. UUUU hurts a lot.");
                                //nAttack++;
                                break;
                            case 16:
                                Heal();
                                Debug.Log("Boss healed himself.");
                                //nAttack++;
                                break;
                            case 17:
                                EffectAttack();
                                Debug.Log("Boss used effect attack");
                                //nAttack++;
                                break;
                            case 18:
                                bossScript.health += 1;
                                Debug.Log("Boss healed 1 HP");
                                RefreshUI();
                                ShowActions();
                                nAttack3 = 0;
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
                playerScript.health = 0;
                //Boss gana
                Debug.Log("Boss gana");
            }
            else
            {
                bossScript.health = 0;
                //Player gana
                Debug.Log("Player gana");
            }
        }
    }
    
    //UI Info
    public void ShowActions()
    {
        actionPanel.SetActive(true);

        //Coste 10
        if (playerScript.energy > 9)
            if (playerScript.spiritBlast >= 5) //valor de acumulación de spirit blast
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

    public void HideActions()
    {
        actionPanel.SetActive(false);
    }

    public void AddCombatText()
    {
        combatDialogue[6].text = combatDialogue[5].text;
        combatDialogue[5].text = combatDialogue[4].text;
        combatDialogue[4].text = combatDialogue[3].text;
        combatDialogue[3].text = combatDialogue[2].text;
        combatDialogue[2].text = combatDialogue[1].text;
        combatDialogue[1].text = combatDialogue[0].text;
    }

    public void ShowPopupText(float damage, Color color)
    {
        Vector3 newPosition = new Vector3(9.76f, 5.21f, -2.34f);
        Quaternion newRotation = Quaternion.Euler(0, 90, 0);
        GameObject popupClone = Instantiate(popupText, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();
    }

    public void ShowPopupTextPlayer(float damage, Color color)
    {
        Vector3 newPosition = new Vector3(-10.52f, 2.87f, -0.15f);
        Quaternion newRotation = Quaternion.Euler(0, 280, 0);
        GameObject popupClone = Instantiate(popupTextPlayer, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();
        
    }

    //Character Actions
    void LightAttack()
    {
        HideActions();

        if(playerScript.moves == 3)
        {
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
        else if(playerScript.moves == 2)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if (Random.value > 0.3)
            {
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
            else
            {
                Debug.Log("Light Attack failed.");
                playerScript.energy -= 3;
                playerScript.moves--;
                AddCombatText();
                combatDialogue[0].text = "Light Attack failed";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    ShowActions();
                }
            }
        }
        else if(playerScript.moves == 1)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if (Random.value > 0.5)
            {
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
            else
            {
                Debug.Log("Light Attack failed.");
                playerScript.energy -= 3;
                playerScript.moves--;
                AddCombatText();
                combatDialogue[0].text = "Light Attack failed";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    ShowActions();
                }
            }
        }
        if(actualEffectPlayer == Effects.PARALISIS)
        {
            if(Random.value > 0.1)
            {
                if (playerScript.moves == 3)
                {
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
                else if (playerScript.moves == 2)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.3)
                    {
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
                    else
                    {
                        Debug.Log("Light Attack failed.");
                        playerScript.energy -= 3;
                        playerScript.moves--;
                        AddCombatText();
                        combatDialogue[0].text = "Light Attack failed";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            ShowActions();
                        }
                    }
                }
                else if (playerScript.moves == 1)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.5)
                    {
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
                    else
                    {
                        Debug.Log("Light Attack failed.");
                        playerScript.energy -= 3;
                        playerScript.moves--;
                        AddCombatText();
                        combatDialogue[0].text = "Light Attack failed";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            ShowActions();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Your player is paralized");
                playerScript.energy -= 3;
                playerScript.moves--;
                AddCombatText();
                combatDialogue[0].text = "Your player is paralized. He can't move";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if (playerScript.moves > 0 && playerScript.energy > 2)
                {
                    ShowActions();
                }
            }
        }
    }

    IEnumerator LightAttackWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        //ShowPopupText(d);
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

        if(actualEffectPlayer == Effects.PARALISIS)
        {
            if(Random.value > 0.4)
            {
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
            else
            {
                Debug.Log("Your player is paralized");
                playerScript.energy -= 7;
                playerScript.moves--;
                AddCombatText();
                combatDialogue[0].text = "Your player is paralized. He can't move";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if (playerScript.moves > 0 && playerScript.energy > 2)
                {
                    ShowActions();
                }
            }
        }
    }

    IEnumerator HeavyAttackWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        //ShowPopupText(d);
        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void BasicHeal()
    {
        HideActions();

        if(playerScript.moves == 3)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            int healing = playerScript.stats.vigor * 7;
            if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
            StartCoroutine(BasicHealWaiter(healing));

            AddCombatText();
            combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else if(playerScript.moves == 2)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if(Random.value > 0.8)
            {
                int healing = playerScript.stats.vigor * 7;
                if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                StartCoroutine(BasicHealWaiter(healing));

                AddCombatText();
                combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed heal");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed heal";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }
        else if(playerScript.moves == 1)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if(Random.value > 0.6)
            {
                int healing = playerScript.stats.vigor * 7;
                if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                StartCoroutine(BasicHealWaiter(healing));

                AddCombatText();
                combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed heal");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed heal";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }

        if(actualEffectPlayer == Effects.PARALISIS)
        {
            if(Random.value > 0.4)
            {
                if (playerScript.moves == 3)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    int healing = playerScript.stats.vigor * 7;
                    if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                    StartCoroutine(BasicHealWaiter(healing));

                    AddCombatText();
                    combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
                else if (playerScript.moves == 2)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.8)
                    {
                        int healing = playerScript.stats.vigor * 7;
                        if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                        StartCoroutine(BasicHealWaiter(healing));

                        AddCombatText();
                        combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            Debug.Log("Player failed heal");
                            playerScript.energy -= 3;
                            playerScript.moves--;
                            AddCombatText();
                            combatDialogue[0].text = "Player failed heal";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
                else if (playerScript.moves == 1)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.6)
                    {
                        int healing = playerScript.stats.vigor * 7;
                        if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                        StartCoroutine(BasicHealWaiter(healing));

                        AddCombatText();
                        combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            Debug.Log("Player failed heal");
                            playerScript.energy -= 3;
                            playerScript.moves--;
                            AddCombatText();
                            combatDialogue[0].text = "Player failed heal";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
            }
            else
            {
                if (playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Your player is paralized");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Your player is paralized. He can't move";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }
    }

    IEnumerator BasicHealWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        healEffect.SetActive(true);
        for (int i = d; i > 0; i--)
        {
            playerScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        ShowPopupTextPlayer(d, Color.green);
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación
        healEffect.SetActive(false);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void BasicSpell()
    {
        HideActions();

        if(playerScript.moves == 3)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            int damage = playerScript.stats.power * 4;
            StartCoroutine(BasicSpellWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else if(playerScript.moves == 2)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if(Random.value > 0.7)
            {
                int damage = playerScript.stats.power * 4;
                StartCoroutine(BasicSpellWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed basic spell");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed basic spell";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }
        else if(playerScript.moves == 1)
        {
            playerScript.energy -= 3;
            playerScript.moves--;

            if(Random.value > 0.5)
            {
                int damage = playerScript.stats.power * 4;
                StartCoroutine(BasicSpellWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed basic spell");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed basic spell";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }

        if(actualEffectPlayer == Effects.PARALISIS)
        {
            if(Random.value > 0.4)
            {
                if (playerScript.moves == 3)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    int damage = playerScript.stats.power * 4;
                    StartCoroutine(BasicSpellWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
                else if (playerScript.moves == 2)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.7)
                    {
                        int damage = playerScript.stats.power * 4;
                        StartCoroutine(BasicSpellWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            Debug.Log("Player failed basic spell");
                            playerScript.energy -= 3;
                            playerScript.moves--;
                            AddCombatText();
                            combatDialogue[0].text = "Player failed basic spell";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
                else if (playerScript.moves == 1)
                {
                    playerScript.energy -= 3;
                    playerScript.moves--;

                    if (Random.value > 0.5)
                    {
                        int damage = playerScript.stats.power * 4;
                        StartCoroutine(BasicSpellWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            Debug.Log("Player failed basic spell");
                            playerScript.energy -= 3;
                            playerScript.moves--;
                            AddCombatText();
                            combatDialogue[0].text = "Player failed basic spell";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
            }
            else
            {
                if (playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed basic spell");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed basic spell";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }
    }

    IEnumerator BasicSpellWaiter(int d)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        Vector3 particlePos = new Vector3(-9.56f, 1.23f, -0.25f);
        //particleAnimator.Play();
        GameObject particle = Instantiate(magicSpell, particlePos, Quaternion.identity);
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        //ShowPopupText(d);
        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    void Guard()
    {
        HideActions();

        if(playerScript.moves == 3)
        {
            playerScript.energy -= 4;
            playerScript.moves--;

            int armored = playerScript.stats.endurance * 3;
            StartCoroutine(GuardWaiter(armored));
            AddCombatText();
            combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else if(playerScript.moves == 2)
        {
            playerScript.energy -= 4;
            playerScript.moves--;

            if(Random.value > 0.75)
            {
                int armored = playerScript.stats.endurance * 3;
                StartCoroutine(GuardWaiter(armored));
                AddCombatText();
                combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed protecting");
                    playerScript.energy -= 4;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed protecting himself";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }

        if(actualEffectPlayer == Effects.PARALISIS)
        {
            if(Random.value > 0.4)
            {
                if (playerScript.moves == 3)
                {
                    playerScript.energy -= 4;
                    playerScript.moves--;

                    int armored = playerScript.stats.endurance * 3;
                    StartCoroutine(GuardWaiter(armored));
                    AddCombatText();
                    combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
                else if (playerScript.moves == 2)
                {
                    playerScript.energy -= 4;
                    playerScript.moves--;

                    if (Random.value > 0.75)
                    {
                        int armored = playerScript.stats.endurance * 3;
                        StartCoroutine(GuardWaiter(armored));
                        AddCombatText();
                        combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                    else
                    {
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            Debug.Log("Player failed protecting");
                            playerScript.energy -= 4;
                            playerScript.moves--;
                            AddCombatText();
                            combatDialogue[0].text = "Player failed protecting himself";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
            }
            else
            {
                if(playerScript.moves > 0 && playerScript.energy > 2)
                {
                    Debug.Log("Player failed protecting");
                    playerScript.energy -= 4;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "Player failed protecting himself";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }
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
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación        
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
        playerScript.spiritBlast = 0;
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
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
        for (int i = d; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        //ShowPopupText(d);
        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
    }

    //Spells
    public void TerrorSpell()
    {
        Debug.Log("Used Terror");

        playerScript.moves--;

        int damage = 100;
        StartCoroutine(TerrorSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Terror and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator TerrorSpellWaiter(int damage)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(damage, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        for(int i = damage; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        endedMove = true;
        if(playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();
    }

    public void RageSpell()
    {
        Debug.Log("Used Rage");

        playerScript.moves--;

        int damage = 200;
        StartCoroutine(RageSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Rage and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator RageSpellWaiter(int damage)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(damage, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        for (int i = damage; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();
    }

    public void GriefSpell()
    {
        Debug.Log("Used Grief");

        playerScript.moves--;

        int damage = 50;
        StartCoroutine(GriefSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Grief and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator GriefSpellWaiter(int damage)
    {
        endedMove = false;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.SetTrigger("HeadHit");
        ShowPopupText(damage, Color.red);
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        for (int i = damage; i > 0; i--)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        endedMove = true;
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();
    }



    // Boss Actions
    void Attack()
    {
        HideActions();
        //bossAnimator.SetTrigger("MeleeAnim");

        if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
        {
            AddCombatText();
            combatDialogue[0].text = "Attack Blocked";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(PlayerBlocked());
        }
        else
        {
            float damage = bossScript.stats.strenght + 10;
            StartCoroutine(BasicAtackWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator BasicAtackWaiter(float d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

        if (playerScript.blockChance >= Random.Range(0, 99))
        {

        }
        else
        if (playerScript.armor > 0)
        {
            if (d <= playerScript.armor)
            {
                for (float i = d; i > 0; i--)
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
                for (float i = d; i > 0; i--)
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
            for (float i = d; i > 0; i--)
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

    void AttackPlus()
    {
        HideActions();
        //bossAnimator.SetTrigger("MeleeAnim");

        if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
        {
            AddCombatText();
            combatDialogue[0].text = "Attack Blocked";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(PlayerBlocked());
        }
        else
        {
            float damage = bossScript.stats.strenght + 25;
            StartCoroutine(AttackPlusWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator AttackPlusWaiter(float d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

        if (playerScript.blockChance >= Random.Range(0, 99))
        {

        }
        else
        if (playerScript.armor > 0)
        {
            if (d <= playerScript.armor)
            {
                for (float i = d; i > 0; i--)
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
                for (float i = d; i > 0; i--)
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
            for (float i = d; i > 0; i--)
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

    void EffectAttack()
    {
        HideActions();
        //Animacion del boss acorde a esta accion.
        float dmg = bossScript.stats.strenght;
        StartCoroutine(EffectAttackWaiter(dmg));
        AddCombatText();
        combatDialogue[0].text = "Boss dealt " + dmg.ToString() + " damage.";
        combatDialogue[0].color = new Color(1, 1, 1, 1);

        int random = Random.Range(1, 100);
        if(random <= 33)
        {
            actualEffectPlayer = Effects.GRIEF;
            AddCombatText();
            combatDialogue[0].text = "Griffyndor";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else if(random > 33 && random <= 66)
        {
            actualEffectPlayer = Effects.PARALISIS;
            AddCombatText();
            combatDialogue[0].text = "You have been paralized. Sometimes you'll fail";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else
        {
            actualEffectPlayer = Effects.NUMB;
            AddCombatText();
            combatDialogue[0].text = "You're numb";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }

        HideActions();
    }

    IEnumerator EffectAttackWaiter(float d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

        if(playerScript.armor > 0)
        {
            if(d >= playerScript.armor)
            {
                for(float i = d; d > 0; i--)
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
                for(float i = playerScript.armor; i > 0; i--)
                {
                    playerScript.armor--;
                    RefreshUI();
                    yield return 0;
                    yield return new WaitForSeconds(0);
                }
                for(float i = d; i > 0; i--)
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
            for(float i = d; i > 0; i--)
            {
                playerScript.health--;
                RefreshUI();
                yield return 0;
                yield return new WaitForSeconds(0);
            }
        }

        if(Random.value <= 0.3)
        {
            actualEffectPlayer = Effects.GRIEF;
        }
        else
        {
            actualEffectPlayer = Effects.NULL;
        }

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void ChargeAttack()
    {
        HideActions();
        StartCoroutine(ChargeWaiter());
        AddCombatText();
        combatDialogue[0].text = "Boss charged his Special Attack.";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator ChargeWaiter()
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        if(bossScript.stats.charge == false)
        {
            bossScript.stats.charge = true;
        }

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void SpecialAttack()
    {
        float dmg = bossScript.stats.strenght * 4;
        if(bossScript.stats.charge == true)
        {
            StartCoroutine(SpecialAttackWaiter(dmg));
        }
    }

    IEnumerator SpecialAttackWaiter(float d)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.SetTrigger("HitReaction");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

        if(playerScript.armor > 0)
        {
            if (d <= playerScript.armor)
            {
                for (int i = 0; i > 0; i--)
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
                for (float i = d; i > 0; i--)
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
            for (float i = d; i > 0; i--)
            {
                playerScript.health--;
                RefreshUI();
                yield return 0;
                yield return new WaitForSeconds(0);
            }
        }

        bossScript.stats.charge = false;
        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void GuardBoss()
    {
        HideActions();

        float armor = bossScript.armor + 50;
        StartCoroutine(GuardBossWaiter(armor));
        AddCombatText();
        combatDialogue[0].text = "Boss has " + armor.ToString() + " armor now.";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator GuardBossWaiter(float h)
    {
        bossEndedMove = false;
        frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //Aumento en la cantidad de block del boss
        for (float i = h; i > 0; i--)
        {
            bossScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(3);
        frontalBossCamera.enabled = !frontalBossCamera.enabled;

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void Heal()
    {
        HideActions();
        //bossAnimator.SetTrigger("HealAnim");

        float heal = bossScript.stats.vigor * 2.5f;
        StartCoroutine(HealWaiter(heal));
        AddCombatText();
        combatDialogue[0].text = "Boss healed for " + heal.ToString() + " HP";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator HealWaiter(float h)
    {
        bossEndedMove = false;
        for(float i = h; i > 0; i--)
        {
            bossScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        ShowPopupText(h, Color.green);
        yield return new WaitForSecondsRealtime(3);

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void HealPlus()
    {
        HideActions();
        //bossAnimator.SetTrigger("HealAnim");

        float heal = bossScript.stats.vigor * 3.5f;
        StartCoroutine(HealPlusWaiter(heal));
    }

    IEnumerator HealPlusWaiter(float h)
    {
        bossEndedMove = false;
        for (float i = h; i > 0; i--)
        {
            bossScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        ShowPopupText(h, Color.green);
        yield return new WaitForSecondsRealtime(3);

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }
    //Efectos que tendran los estados en el player
    void ApplyGrief() //Similar a estar envenenado, el efecto aumenta la disminucion de vida por turno.
    {
        if(playerScript.moves == 3)
        {
            playerScript.health -= playerScript.maxHealth / 16; //resta 1/16 del total de la vida
            RefreshUI();
        }
        else if(playerScript.moves == 2)
        {
            playerScript.health -= (playerScript.maxHealth * 2) / 16; //resta 2/16 del total de la vida
            RefreshUI();
        }
        else if(playerScript.moves == 1)
        {
            playerScript.health -= (playerScript.maxHealth * 3) / 16; //resta 3/16 del total de la vida
        }
    }

    void ApplyNumb() //El player no se puede defender
    {
        guardButton.interactable = false;
    }

    void ApplyParalisis() //El player tiene un porcentaje de no poder realizar acciones, y disminuyen los action points
    {

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