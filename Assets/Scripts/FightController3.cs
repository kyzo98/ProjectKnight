﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightController3 : MonoBehaviour
{

    public enum StateType3 { NULL, GRIEF, PARALISIS, NUMB };

    public struct States3
    {
        public StateType3 name3;
        public int turnsLeft3;
    }

    enum BuffStateType { NULL }
    enum DebuffStateType { NULL }
    struct Buff
    {
        public BuffStateType StateType;
        public int remainingTurns;
    }
    struct Debuff
    {
        public DebuffStateType StateType;
        public int remainingTurns;
    }
    public struct Orbs
    {
        public int vitality;
        public int strenght;
        public int endurance;
        public int power;
        public int vigor;
    }
    Orbs orbs;

    public struct Sorrows
    {
        public int rage;
        public int terror;
        public int grief;
    };
    Sorrows sorrows;
    public Button buttonSorrows;
    public Button buttonRage;
    public Button buttonTerror;
    public Button buttonGrief;

    public struct Drives
    {
        public int courage;
        public int focus;
        public int will;
        public int remembrance;
        public int spiritualHealing;
        public int clarity;
        public int grace;
    };
    Drives drives;
    public Button buttonDrives;
    public Button buttonCourage;
    public Button buttonFocus;
    public Button buttonWill;
    public Button buttonRemembrance;
    public Button buttonSpiritualHealing;
    public Button buttonClarity;
    public Button buttonGrace;

    private int turn;
    private int nAttack;
    private int nAttack2;
    private int nAttack3;
    private int EffectTurnCounter = 0;
    private int griefLifeDivider = 1;
    //Velocidad de movimiento del player cada vez que se acerca al boss para atacar
    float speed = 10.0f;

    //GAMEOBJECTS
    //public GameObject armorEffect;
    //public GameObject healEffect;
    public GameObject armorEffect;
    public GameObject magicSpell;
    public GameObject pauseMenu;
    public GameObject exitGameMenu;
    public GameObject backMenuMenu;
    public GameObject optionsMenuMenu;
    bool gamePaused = false;

    //AUDIO
    public GameObject backgroundMusic;
    AudioSource[] backgroundAudio;

    //PLAYER
    public GameObject player;
    Vector3 playerInitPos;
    private Player playerScript;
    private Buff[] playerBuff;
    private Debuff[] playerDebuff;
    public States3[] state;
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
    public States3[] bossStates3;
    //UI BOSS
    public Slider bossHealthBar;
    public Text bossHealthNumber;
    public Text bossArmorNumber;
    //POPUP TEXT UI BOSS
    public GameObject popupText;
    //POPUP TEXT UI PLAYER
    public GameObject popupTextPlayer;
    //BOSS ANIMATIONS
    Animator bossAnimator;

    //PARTICLES
    public GameObject lightStrikeHolder;
    public ParticleSystem lightStrikeParticleSystem;
    public GameObject healParticle;
    public GameObject despairParticleHolder;
    public ParticleSystem despairParticleSystem;
    public GameObject terrorParticleHolder;
    public ParticleSystem terrorParticleSystem;
    public GameObject griefParticleHolder;
    public ParticleSystem griefParticleSystem;
    public GameObject rageParticleHolder;
    public ParticleSystem rageParticleSystem;
    public GameObject animaBlastParticleHolder;
    public ParticleSystem animaBlastParticleSystem;
    public ParticleSystem hitParticle;
    public GameObject hitHolder;
    public GameObject courageParticle;
    public GameObject focusParticle;
    public GameObject willParticle;

    //Sounds
    private AudioSource audioSource;
    public AudioClip lightStrikeAudio;
    public AudioClip heavyStrikeAudio;
    public AudioClip guardAudio;
    public AudioClip despairAudio;
    public AudioClip healAudio;
    public AudioClip sorrow1Audio;
    public AudioClip sorrow2Audio;

    public AudioClip HitSpellAudio;
    public AudioClip HitStrikeAudio;

    //CAMERAS
    public Camera mainCamera;
    //public Camera frontalPlayerCamera;
    //public Camera frontalBossCamera;
    //public Camera heavyAttackCam;

    private bool endedMove = true;
    private bool bossEndedMove = true;
    //Succes rates bools
    bool usedLightAttack1;
    bool usedLightAttack2;
    bool usedBasicHeal1;
    bool usedBasicHeal2;
    bool usedBasicSpell1;
    bool usedBasicSpell2;
    bool usedGuard;

    // Use this for initialization
    void Start()
    {
        //GETTING QUANTITY OF ORBS
        orbs.vitality = PlayerPrefs.GetInt("VITALITY_ORB");
        orbs.strenght = PlayerPrefs.GetInt("STRENGHT_ORB");
        orbs.endurance = PlayerPrefs.GetInt("ENDURANCE_ORB");
        orbs.power = PlayerPrefs.GetInt("POWER_ORB");
        orbs.vigor = PlayerPrefs.GetInt("VIGOR_ORB");

        //GETTING QUANTITY OF SORROWS
        sorrows.rage = PlayerPrefs.GetInt("Rage");
        sorrows.terror = PlayerPrefs.GetInt("Terror");
        sorrows.grief = PlayerPrefs.GetInt("Grief");

        //GETTING QUANTITY OF DRIVES
        drives.courage = PlayerPrefs.GetInt("Courage");
        drives.focus = PlayerPrefs.GetInt("Focus");
        drives.will = PlayerPrefs.GetInt("Will");
        drives.remembrance = PlayerPrefs.GetInt("Remembrance");
        drives.spiritualHealing = PlayerPrefs.GetInt("SpiritualHealing");
        drives.clarity = PlayerPrefs.GetInt("Clarity");
        drives.grace = PlayerPrefs.GetInt("Grace");

        backgroundAudio = backgroundMusic.GetComponents<AudioSource>();
        audioSource = this.GetComponent<AudioSource>();

        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss>();
        bossAnimator = boss.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();

        hitParticle.Stop();

        playerInitPos = player.transform.position;

        //Buttons
        lightAttackButton.onClick.AddListener(LightAttack);
        heavyAttackButton.onClick.AddListener(HeavyAttack);
        basicHealButton.onClick.AddListener(BasicHeal);
        basicSpellButton.onClick.AddListener(BasicSpell);
        guardButton.onClick.AddListener(Guard);
        spiritBlastButton.onClick.AddListener(SpiritBlast);
        buttonTerror.onClick.AddListener(TerrorSpell);
        buttonRage.onClick.AddListener(RageSpell);
        buttonGrief.onClick.AddListener(GriefSpell);

        //Cameras
        mainCamera.enabled = true;

        //Buffs & Debuffs
        playerBuff = new Buff[2];
        playerBuff[0].StateType = BuffStateType.NULL;
        playerBuff[0].remainingTurns = 0;
        playerBuff[1].StateType = BuffStateType.NULL;
        playerBuff[1].remainingTurns = 0;
        playerDebuff = new Debuff[2];
        playerDebuff[0].StateType = DebuffStateType.NULL;
        playerDebuff[0].remainingTurns = 0;
        playerDebuff[1].StateType = DebuffStateType.NULL;
        playerDebuff[1].remainingTurns = 0;

        //INITIALIZING PLAYER STATES
        state = new States3[3];
        state[0].name3 = StateType3.NULL;
        state[0].turnsLeft3 = 0;
        state[1].name3 = StateType3.NULL;
        state[1].turnsLeft3 = 0;
        state[2].name3 = StateType3.NULL;
        state[2].turnsLeft3 = 0;

        //INITIALIZING BOSS STATES
        bossStates3 = new States3[3];
        bossStates3[0].name3 = StateType3.NULL;
        bossStates3[0].turnsLeft3 = 0;
        bossStates3[1].name3 = StateType3.NULL;
        bossStates3[1].turnsLeft3 = 0;
        bossStates3[2].name3 = StateType3.NULL;
        bossStates3[2].turnsLeft3 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!gamePaused)
                PauseGame();
            else
                UnPauseGame();
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
                    //When the player is grief he loses some life.
                    if (state[0].name3 == StateType3.GRIEF || state[1].name3 == StateType3.GRIEF || state[2].name3 == StateType3.GRIEF)
                    {
                        float damage = playerScript.maxHealth * (1 / 16);
                        AddCombatText();
                        combatDialogue[0].text = "Boss lost " + damage.ToString() + " due to Grief";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        StartCoroutine(GriefLifePlayer(damage));
                        griefLifeDivider += 1;
                    }

                    //Player's turn
                    if (playerScript.moves == 3 && playerScript.armor > 0)//si tenia armadura equipada se retira ya que solo dura 1 turno
                    {
                        playerScript.armor = 0;
                        RefreshUI();
                    }
                    if (playerScript.armor == 0)
                        //armorEffect.SetActive(false); ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    Debug.Log("player ended move");
                    RestartSuccesBools();
                    //DECREASE TURNS LEFT OF THE PLAYER STATES
                    if (state[0].name3 == StateType3.GRIEF || state[0].name3 == StateType3.NUMB || state[0].name3 == StateType3.PARALISIS && state[0].turnsLeft3 > 0)
                    {
                        state[0].turnsLeft3 -= 1;
                        if (state[0].turnsLeft3 == 0)
                        {
                            if (state[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            state[0].name3 = StateType3.NULL;
                        }
                    }

                    if (state[1].name3 == StateType3.GRIEF || state[1].name3 == StateType3.NUMB || state[1].name3 == StateType3.PARALISIS && state[1].turnsLeft3 > 0)
                    {
                        state[1].turnsLeft3 -= 1;
                        if (state[1].turnsLeft3 == 0)
                        {
                            if (state[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            state[1].name3 = StateType3.NULL;
                        }
                    }

                    if (state[2].name3 == StateType3.GRIEF || state[2].name3 == StateType3.NUMB || state[2].name3 == StateType3.PARALISIS && state[2].turnsLeft3 > 0)
                    {
                        state[2].turnsLeft3 -= 1;
                        if (state[2].turnsLeft3 == 0)
                        {
                            if (state[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            state[2].name3 = StateType3.NULL;
                        }
                    }

                    //DECREASE TURNS LEFT OF THE BOSS STATES
                    if (bossStates3[0].name3 == StateType3.GRIEF || bossStates3[0].name3 == StateType3.NUMB || bossStates3[0].name3 == StateType3.PARALISIS && bossStates3[0].turnsLeft3 > 0)
                    {
                        bossStates3[0].turnsLeft3 -= 1;
                        if (bossStates3[0].turnsLeft3 == 0)
                        {
                            if (bossStates3[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossStates3[0].name3 = StateType3.NULL;
                        }
                    }

                    if (bossStates3[1].name3 == StateType3.GRIEF || bossStates3[1].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.PARALISIS && bossStates3[1].turnsLeft3 > 0)
                    {
                        bossStates3[1].turnsLeft3 -= 1;
                        if (bossStates3[1].turnsLeft3 == 0)
                        {
                            if (bossStates3[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossStates3[1].name3 = StateType3.NULL;
                        }
                    }

                    if (bossStates3[2].name3 == StateType3.GRIEF || bossStates3[2].name3 == StateType3.NUMB || bossStates3[2].name3 == StateType3.PARALISIS && bossStates3[2].turnsLeft3 > 0)
                    {
                        bossStates3[2].turnsLeft3 -= 1;
                        if (bossStates3[2].turnsLeft3 == 0)
                        {
                            if (bossStates3[2].name3 == StateType3.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossStates3[2].name3 = StateType3.NULL;
                        }
                    }

                    //Boss's turn

                    if (bossScript.health >= 700)
                    {
                        //When the boss is grief he losses some life
                        if (bossStates3[0].name3 == StateType3.GRIEF || bossStates3[1].name3 == StateType3.GRIEF || bossStates3[2].name3 == StateType3.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (1 / 16);
                            AddCombatText();
                            combatDialogue[0].text = "Boss lost " + damage.ToString() + " due to Grief";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            StartCoroutine(GriefLifeBoss(damage));
                            griefLifeDivider += 1;
                        }
                        nAttack++;
                        Debug.Log(nAttack);
                        switch (nAttack)
                        {
                            case 1:
                                Attack();
                                Debug.Log("Boss used effect attack.");
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
                    else if (bossScript.health >= 250 && bossScript.health <= 700)
                    {
                        //When the boss is grief he losses some life
                        if (bossStates3[0].name3 == StateType3.GRIEF || bossStates3[1].name3 == StateType3.GRIEF || bossStates3[2].name3 == StateType3.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (1 / 16);
                            AddCombatText();
                            combatDialogue[0].text = "Boss lost " + damage.ToString() + " due to Grief";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            StartCoroutine(GriefLifeBoss(damage));
                            griefLifeDivider += 1;
                        }
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
                    else if (bossScript.health >= 0 && bossScript.health <= 250)
                    {
                        //When the boss is grief he losses some life
                        if (bossStates3[0].name3 == StateType3.GRIEF || bossStates3[1].name3 == StateType3.GRIEF || bossStates3[2].name3 == StateType3.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (1 / 16);
                            AddCombatText();
                            combatDialogue[0].text = "Boss lost " + damage.ToString() + " due to Grief";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            StartCoroutine(GriefLifeBoss(damage));
                            griefLifeDivider += 1;
                        }
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

                    for (int i = 0; i < 3; i++)
                    {
                        Debug.Log(state[i].name3);
                        Debug.Log(state[i].turnsLeft3);
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
                SceneManager.LoadScene("Narrator", LoadSceneMode.Single);
            }
        }
    }

    public void ShowActions()
    {
        actionPanel.SetActive(true);

        if ((drives.clarity + drives.courage + drives.focus + drives.grace + drives.remembrance + drives.spiritualHealing + drives.will) > 0) buttonDrives.interactable = true;
        else buttonDrives.interactable = false;
        if ((sorrows.grief + sorrows.rage + sorrows.terror) > 0) buttonSorrows.interactable = true;
        else buttonSorrows.interactable = false;

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

        //Coste 5
        if (playerScript.energy > 4)
        {
            if (sorrows.grief > 0) buttonGrief.interactable = true;
            else buttonGrief.interactable = false;
            if (sorrows.terror > 0) buttonTerror.interactable = true;
            else buttonTerror.interactable = false;
            if (sorrows.rage > 0) buttonRage.interactable = true;
            else buttonRage.interactable = false;
        }
        else
        {
            buttonGrief.interactable = false;
            buttonRage.interactable = false;
            buttonTerror.interactable = false;
        }

        //Coste 4
        if (playerScript.energy > 3)
        {
            guardButton.interactable = true;

            if (drives.clarity > 0) buttonClarity.interactable = true;
            else buttonClarity.interactable = false;
            if (drives.courage > 0) buttonCourage.interactable = true;
            else buttonCourage.interactable = false;
            if (drives.focus > 0) buttonFocus.interactable = true;
            else buttonFocus.interactable = false;
            if (drives.grace > 0) buttonGrace.interactable = true;
            else buttonGrace.interactable = false;
            if (drives.remembrance > 0) buttonRemembrance.interactable = true;
            else buttonRemembrance.interactable = false;
            if (drives.spiritualHealing > 0) buttonSpiritualHealing.interactable = true;
            else buttonSpiritualHealing.interactable = false;
            if (drives.will > 0) buttonWill.interactable = true;
            else buttonWill.interactable = false;
        }
        else
        {
            guardButton.interactable = false;

            buttonClarity.interactable = false;
            buttonCourage.interactable = false;
            buttonFocus.interactable = false;
            buttonGrace.interactable = false;
            buttonRemembrance.interactable = false;
            buttonSpiritualHealing.interactable = false;
            buttonWill.interactable = false;
        }

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

        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.NUMB)
            {
                basicHealButton.interactable = false;
            }
        }
    }

    public void HideActions()
    {
        actionPanel.SetActive(false);
    }

    public void RestartSuccesBools()
    {
        Debug.Log("Restarted bools");
        usedLightAttack1 = false;
        usedLightAttack2 = false;
        usedBasicHeal1 = false;
        usedBasicHeal2 = false;
        usedBasicSpell1 = false;
        usedBasicSpell2 = false;
        usedGuard = false;
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
        Vector3 newPosition = new Vector3(-2.915f, 5.27f, 1.55f);
        Quaternion newRotation = Quaternion.Euler(0, 90, 0);
        GameObject popupClone = Instantiate(popupText, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();
    }

    public void ShowPopupTextPlayer(float damage, Color color)
    {
        Vector3 newPosition = new Vector3(-9.9f, 1.43f, 1.24f);
        Quaternion newRotation = Quaternion.Euler(0, 280, 0);
        GameObject popupClone = Instantiate(popupTextPlayer, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();
    }

    public void ShowFailText(Color color)
    {
        Vector3 newPosition = new Vector3(-2.915f, 5.27f, 1.55f);
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject failClone = Instantiate(popupText, newPosition, newRotation);
        failClone.GetComponent<TextMesh>().color = color;
        failClone.GetComponent<TextMesh>().text = "MISS";
    }

    public void ShowFailTextPlayer(Color color)
    {
        Vector3 newPosition = new Vector3(-9.9f, 1.43f, 1.24f);
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject failClone = Instantiate(popupText, newPosition, newRotation);
        failClone.GetComponent<TextMesh>().color = color;
        failClone.GetComponent<TextMesh>().text = "MISS";
    }

    void LightAttack()
    {
        HideActions();

        if (state[0].name3 != StateType3.PARALISIS || state[1].name3 != StateType3.PARALISIS || state[2].name3 != StateType3.PARALISIS)
        {
            if (usedLightAttack1 == false && usedLightAttack2 == false)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value <= 0.05) //critico
                {
                    int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                    StartCoroutine(LightAttackWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                    combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                    usedLightAttack1 = true;
                }
                else //ataque normal
                {
                    int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                    StartCoroutine(LightAttackWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    usedLightAttack1 = true;
                }
            }
            else if (usedLightAttack1 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.3)
                {
                    if (Random.value <= 0.05) //critico
                    {
                        int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                        StartCoroutine(LightAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                        combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                        usedLightAttack2 = true;
                    }
                    else //ataque normal
                    {
                        int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                        StartCoroutine(LightAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        usedLightAttack2 = true;
                    }
                }
                else
                {
                    Debug.Log("Light Attack failed.");
                    ShowFailText(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Light Attack failed";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    usedLightAttack2 = true;
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
            else if (usedLightAttack2 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.5)
                {
                    if (Random.value <= 0.05) //critico
                    {
                        int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                        StartCoroutine(LightAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                        combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                    }
                    else //ataque normal
                    {
                        int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                        StartCoroutine(LightAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                }
                else
                {
                    Debug.Log("Light Attack failed.");
                    ShowFailText(Color.red);
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

        //Aplicación de parálisis
        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.PARALISIS)
            {
                if (Random.value > 0.1)
                {
                    if (usedLightAttack1 == false && usedLightAttack2 == false)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value <= 0.05) //critico
                        {
                            int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                            StartCoroutine(LightAttackWaiter(damage));
                            AddCombatText();
                            combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                            combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                            usedLightAttack1 = true;
                        }
                        else //ataque normal
                        {
                            int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                            StartCoroutine(LightAttackWaiter(damage));
                            AddCombatText();
                            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            usedLightAttack1 = true;
                        }
                    }
                    else if (usedLightAttack1 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.3)
                        {
                            if (Random.value <= 0.05) //critico
                            {
                                int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                                StartCoroutine(LightAttackWaiter(damage));
                                AddCombatText();
                                combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                                combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                                usedLightAttack2 = true;
                            }
                            else //ataque normal
                            {
                                int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                                StartCoroutine(LightAttackWaiter(damage));
                                AddCombatText();
                                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                                combatDialogue[0].color = new Color(1, 1, 1, 1);
                                usedLightAttack2 = true;
                            }
                        }
                        else
                        {
                            Debug.Log("Light Attack failed.");
                            ShowFailText(Color.red);
                            AddCombatText();
                            combatDialogue[0].text = "Light Attack failed";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            usedLightAttack2 = true;
                            if (playerScript.moves > 0 && playerScript.energy > 2)
                            {
                                ShowActions();
                            }
                        }
                    }
                    else if (usedLightAttack2 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.5)
                        {
                            if (Random.value <= 0.05) //critico
                            {
                                int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                                StartCoroutine(LightAttackWaiter(damage));
                                AddCombatText();
                                combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                                combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                            }
                            else //ataque normal
                            {
                                int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 30) / 100) + 3));
                                StartCoroutine(LightAttackWaiter(damage));
                                AddCombatText();
                                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                                combatDialogue[0].color = new Color(1, 1, 1, 1);
                            }
                        }
                        else
                        {
                            Debug.Log("Light Attack failed.");
                            ShowFailText(Color.red);
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
                    Debug.Log("You can't attack your player is paralized");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "You can't attack, your player is paralized";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }
    }

    IEnumerator ThrowLightStrike(GameObject particleHolder, ParticleSystem particleSystem, int damage)
    {
        float speed = 20.0f;
        Vector3 initialPos = new Vector3(-9.537f, 0.585f, 1.318f);
        Vector3 finalPos = new Vector3(-3.329f, 1.931f, 1.346f);
        float timeToReachTarget = Vector3.Distance(initialPos, finalPos) / speed;

        float time = 0.0f;
        playerAnimator.Play("LightAttack");
        yield return new WaitForSecondsRealtime(1.87f);
        GameObject lightStrikeClone = Instantiate(particleHolder, initialPos, Quaternion.Euler(new Vector3(1.904f, 1.303f, 14.395f)));
        particleSystem.Play();
        audioSource.clip = lightStrikeAudio;
        audioSource.Play();
        while (time < 1)
        {
            time += Time.deltaTime / timeToReachTarget;
            lightStrikeClone.transform.position = Vector3.Lerp(initialPos, finalPos, time);

            yield return null;
        }
        Destroy(lightStrikeClone);
        GameObject hitClone = Instantiate(hitHolder, finalPos, Quaternion.identity);
        audioSource.clip = HitStrikeAudio;
        audioSource.Play();
        hitParticle.Play();
        bossAnimator.Play("Damage");
        ShowPopupText(damage, Color.red);
    }

    IEnumerator LightAttackWaiter(int d)
    {
        endedMove = false;
        //heavyAttackCam.enabled = !heavyAttackCam.enabled; //Cambio de camara (cámara específica de la animación)
        StartCoroutine(ThrowLightStrike(lightStrikeHolder, lightStrikeParticleSystem, d));
        player.transform.rotation = Quaternion.Euler(0f, 170.944f, 0f);
        yield return new WaitForSecondsRealtime(3f); //Tiempo de espera de la animación
        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        //heavyAttackCam.enabled = !heavyAttackCam.enabled;
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

        if (state[0].name3 != StateType3.PARALISIS || state[1].name3 != StateType3.PARALISIS || state[2].name3 != StateType3.PARALISIS)
        {
            if (Random.value <= 0.15) //critico
            {
                int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 80) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 80) / 100) + 3));
                StartCoroutine(HeavyAttackWaiter(damage));
                AddCombatText();
                combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
            }
            else //ataque normal
            {
                int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 80) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 80) / 100) + 3));
                StartCoroutine(HeavyAttackWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }

        //Aplicacion del efecto de paralisis
        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.PARALISIS)
            {
                if (Random.value > 0.4)
                {
                    if (Random.value <= 0.15) //critico
                    {
                        int damage = Random.Range(((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 80) / 100) - 3), ((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 2) * 80) / 100) + 3));
                        StartCoroutine(HeavyAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].color = new Color(1, 0.086f, 0.258f, 1);
                        combatDialogue[0].text = "CRITICAL! Player and dealt " + damage.ToString() + " damge to the Boss";
                    }
                    else //ataque normal
                    {
                        int damage = Random.Range((((playerScript.stats.strenght * playerScript.strenghtMultiplier * 80) / 100) - 3), (((playerScript.stats.strenght * playerScript.strenghtMultiplier * 80) / 100) + 3));
                        StartCoroutine(HeavyAttackWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damge to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                    }
                }
                else
                {
                    Debug.Log("You failed to use heavy attack, due to paralisis.");
                    ShowFailText(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "You failed to use heavy attack, due to paralisis.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
            }
        }
    }

    IEnumerator HeavyAttackWaiter(int d)
    {
        endedMove = false;
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        //yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        Vector3 enemyPosition = new Vector3(-3.409f, -0.309f, 1.289f);
        playerAnimator.Play("Run");
        while (MoveToPosition(enemyPosition))
        {
            yield return null;
        } //adapt animations and moving times for the attack.
        //heavyAttackCam.enabled = !heavyAttackCam.enabled;
        playerAnimator.Play("HeavyAttack");
        yield return new WaitForSecondsRealtime(0.7f);
        audioSource.clip = heavyStrikeAudio;
        audioSource.Play();
        bossAnimator.Play("Damage");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(1.6f);
        player.transform.rotation = Quaternion.Euler(0, -100, 0);
        playerAnimator.Play("RunBack");
        //heavyAttackCam.enabled = !heavyAttackCam;
        //Make it return to the starting position
        Vector3 originalPosition = playerInitPos;
        yield return new WaitForSecondsRealtime(0.5f);
        while (MoveToPosition(originalPosition)) { yield return null; } //The player moves near the enemy to kick him.
        playerAnimator.Play("Idle");
        player.transform.rotation = Quaternion.Euler(0, 81.5f, 0);
        //frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
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

        if (state[0].name3 != StateType3.PARALISIS || state[1].name3 != StateType3.PARALISIS || state[2].name3 != StateType3.PARALISIS)
        {
            if (usedBasicHeal1 == false && usedBasicHeal2 == false)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                StartCoroutine(BasicHealWaiter(healing));

                AddCombatText();
                combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                usedBasicHeal1 = true;
            }
            else if (usedBasicHeal1 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.8)
                {
                    float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                    if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                    StartCoroutine(BasicHealWaiter(healing));

                    AddCombatText();
                    combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    usedBasicHeal2 = true;
                }
                else
                {
                    Debug.Log("Player failed heal");
                    ShowFailTextPlayer(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Player failed heal";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    usedBasicHeal2 = true;
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
            else if (usedBasicHeal2 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.6)
                {
                    float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                    if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                    StartCoroutine(BasicHealWaiter(healing));

                    AddCombatText();
                    combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Debug.Log("Player failed heal");
                    ShowFailTextPlayer(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Player failed heal";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.PARALISIS)
            {
                if (Random.value > 0.4)
                {
                    if (usedBasicHeal1 == false && usedBasicHeal2 == false)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                        if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                        StartCoroutine(BasicHealWaiter(healing));

                        AddCombatText();
                        combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        usedBasicHeal1 = true;
                    }
                    else if (usedBasicHeal1 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.8)
                        {
                            float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                            if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                            StartCoroutine(BasicHealWaiter(healing));

                            AddCombatText();
                            combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            usedBasicHeal2 = true;
                        }
                        else
                        {
                            Debug.Log("Player failed heal");
                            ShowFailTextPlayer(Color.red);
                            AddCombatText();
                            combatDialogue[0].text = "Player failed heal";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            usedBasicHeal2 = true;
                            if (playerScript.moves > 0 && playerScript.energy > 2)
                            {
                                ShowActions();
                            }
                        }
                    }
                    else if (usedBasicHeal2 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.6)
                        {
                            float healing = playerScript.stats.vigor * playerScript.vigorMultiplier;
                            if (playerScript.health + healing > playerScript.maxHealth) healing -= playerScript.health + healing - playerScript.maxHealth; //exceso de curación
                            StartCoroutine(BasicHealWaiter(healing));

                            AddCombatText();
                            combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                        }
                        else
                        {
                            Debug.Log("Player failed heal");
                            ShowFailTextPlayer(Color.red);
                            AddCombatText();
                            combatDialogue[0].text = "Player failed heal";
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
                    Debug.Log("Failed to heal due to paralisis.");
                    ShowFailTextPlayer(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Failed to heal due to paralisis.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }
    }

    IEnumerator BasicHealWaiter(float d)
    {
        endedMove = false;
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        playerAnimator.Play("Heal");
        healParticle.SetActive(true);
        audioSource.clip = healAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        ShowPopupTextPlayer(d, Color.green);
        yield return new WaitForSecondsRealtime(1);
        ParticleSystem ps = healParticle.GetComponent<ParticleSystem>();
        ps.Stop();
        yield return new WaitForSecondsRealtime(1.5f);
        healParticle.SetActive(false);
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
        for (float i = d; i > 0; i--)
        {
            playerScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(2); //Tiempo de espera de la animación
        //healEffect.SetActive(false);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
    }

    void BasicSpell() //It's the despair one
    {
        HideActions();

        if (state[0].name3 != StateType3.PARALISIS || state[1].name3 != StateType3.PARALISIS || state[2].name3 != StateType3.PARALISIS)
        {
            if (usedBasicSpell1 == false && usedBasicSpell2 == false)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
                StartCoroutine(BasicSpellWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                usedBasicSpell1 = true;
            }
            else if (usedBasicSpell1 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.7)
                {
                    int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
                    StartCoroutine(BasicSpellWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    usedBasicSpell2 = true;
                }
                else
                {
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        Debug.Log("Player failed basic spell");
                        AddCombatText();
                        combatDialogue[0].text = "Player failed basic spell";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            ShowActions();
                        }
                        usedBasicSpell2 = true;
                    }
                }
            }
            else if (usedBasicSpell2 == true)
            {
                playerScript.energy -= 3;
                playerScript.moves--;

                if (Random.value > 0.5)
                {
                    int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
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
                        AddCombatText();
                        combatDialogue[0].text = "Player failed basic spell";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        if (playerScript.moves > 0 && playerScript.energy > 2)
                        {
                            ShowActions();
                        }
                    }
                }
            }
        }

        //Aplicación de Paralisis
        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.PARALISIS)
            {
                if (Random.value > 0.4)
                {
                    if (usedBasicSpell1 == false && usedBasicSpell2 == false)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
                        StartCoroutine(BasicSpellWaiter(damage));
                        AddCombatText();
                        combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        usedBasicSpell1 = true;
                    }
                    else if (usedBasicSpell1 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.7)
                        {
                            int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
                            StartCoroutine(BasicSpellWaiter(damage));
                            AddCombatText();
                            combatDialogue[0].text = "Player dealt " + damage.ToString() + " damage to the Boss";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            usedBasicSpell2 = true;
                        }
                        else
                        {
                            if (playerScript.moves > 0 && playerScript.energy > 2)
                            {
                                Debug.Log("Player failed basic spell");
                                ShowFailText(Color.red);
                                AddCombatText();
                                combatDialogue[0].text = "Player failed basic spell";
                                combatDialogue[0].color = new Color(1, 1, 1, 1);
                                if (playerScript.moves > 0 && playerScript.energy > 2)
                                {
                                    ShowActions();
                                }
                                usedBasicSpell2 = true;
                            }
                        }
                    }
                    else if (usedBasicSpell2 == true)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.value > 0.5)
                        {
                            int damage = (((playerScript.stats.power * playerScript.powerMultiplier) * 20) / 100);
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
                                ShowFailText(Color.red);
                                AddCombatText();
                                combatDialogue[0].text = "Player failed basic spell";
                                combatDialogue[0].color = new Color(1, 1, 1, 1);
                                if (playerScript.moves > 0 && playerScript.energy > 2)
                                {
                                    ShowActions();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Failed to use spell due to paralisis");
                    ShowFailText(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Failed to use spell due to paralisis.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }
    }

    IEnumerator ThrowProjectile(GameObject particleHolder, ParticleSystem particleSystem, int damage, AudioClip auxClip, AudioClip auxClipHit)
    {
        float speed = 20.0f;
        Vector3 initialPos = new Vector3(-7.52f, 0.65f, 1.32f);
        Vector3 finalPos = new Vector3(-3.329f, 1.931f, 1.346f);
        GameObject despairClone = Instantiate(particleHolder, initialPos, Quaternion.Euler(new Vector3(1.904f, 1.303f, 14.395f)));
        float timeToReachTarget = Vector3.Distance(initialPos, finalPos) / speed;

        float time = 0.0f;
        particleSystem.Play();
        audioSource.clip = auxClip;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(0.9f);
        while (time < 1)
        {
            time += Time.deltaTime / timeToReachTarget;
            despairClone.transform.position = Vector3.Lerp(initialPos, finalPos, time);

            yield return null;
        }
        Destroy(despairClone);
        GameObject hitClone = Instantiate(hitHolder, finalPos, Quaternion.identity);
        hitParticle.Play();
        audioSource.clip = auxClipHit;
        audioSource.Play();
        bossAnimator.Play("Damage");
        ShowPopupText(damage, Color.red);
    }

    IEnumerator BasicSpellWaiter(int d)
    {
        endedMove = false;
        //heavyAttackCam.enabled = !heavyAttackCam.enabled; //Cambio de camara (cámara específica de la animación)
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.6f);
        StartCoroutine(ThrowProjectile(despairParticleHolder, despairParticleSystem, d, despairAudio, HitSpellAudio));
        yield return new WaitForSecondsRealtime(3f); //Tiempo de espera de la animación
        //heavyAttackCam.enabled = !heavyAttackCam.enabled;
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

        if (state[0].name3 != StateType3.PARALISIS || state[1].name3 != StateType3.PARALISIS || state[2].name3 != StateType3.PARALISIS)
        {
            if (usedGuard == false)
            {
                playerScript.energy -= 4;
                playerScript.moves--;

                int armored = playerScript.stats.endurance * 3;
                StartCoroutine(GuardWaiter(armored));
                AddCombatText();
                combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                usedGuard = true;
            }
            else if (usedGuard == true)
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
                    Debug.Log("Player failed protecting");
                    ShowFailText(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Player failed protecting himself";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }
        }

        //Aplicacion de paralisis
        for (int i = 0; i < 3; i++)
        {
            if (state[i].name3 == StateType3.PARALISIS)
            {
                if (Random.value > 0.4)
                {
                    if (usedGuard == false)
                    {
                        playerScript.energy -= 4;
                        playerScript.moves--;

                        int armored = playerScript.stats.endurance * 3;
                        StartCoroutine(GuardWaiter(armored));
                        AddCombatText();
                        combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        usedGuard = true;
                    }
                    else if (usedGuard == true)
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
                            Debug.Log("Player failed protecting");
                            ShowFailText(Color.red);
                            AddCombatText();
                            combatDialogue[0].text = "Player failed protecting himself";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            ShowActions();
                        }
                    }
                }
                else
                {
                    Debug.Log("Failed to protect due to paralisis");
                    ShowFailText(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Player failed to prtect due to paralisi.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    ShowActions();
                }
            }

            else if (state[i].name3 == StateType3.GRIEF)
            {
                if (Random.value > 0.7)
                {
                    state[i].name3 = StateType3.NULL;
                    state[i].turnsLeft3 = 0;
                    AddCombatText();
                    combatDialogue[0].text = "GRIEF Effect disapear.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
            }
        }
    }

    IEnumerator GuardWaiter(int d)
    {
        audioSource.clip = guardAudio;
        audioSource.Play();
        endedMove = false;
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        playerScript.blockChance += 5;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
        for (int i = d; i > 0; i--)
        {
            playerScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        armorEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación        
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara a normal

        endedMove = true;
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
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled; //Cambio de camara (cámara específica de la animación)
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        StartCoroutine(ThrowProjectile(animaBlastParticleHolder, animaBlastParticleSystem, d, sorrow2Audio, HitSpellAudio));
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        //frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //bossAnimator.Play("Damage");
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
        //frontalBossCamera.enabled = !frontalBossCamera.enabled; //Cambio de camara a normal
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

    public void TerrorSpell()
    {
        HideActions();
        Debug.Log("Used Terror");

        playerScript.moves--;
        playerScript.energy -= 5;

        int damage = playerScript.stats.power * playerScript.powerMultiplier;
        StartCoroutine(TerrorSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Terror and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator TerrorSpellWaiter(int damage)
    {
        HideActions();
        endedMove = false;
        playerAnimator.Play("Despair");
        StartCoroutine(ThrowProjectile(terrorParticleHolder, terrorParticleSystem, damage, sorrow2Audio, HitSpellAudio));
        yield return new WaitForSecondsRealtime(3);
        ShowPopupText(damage, Color.red);

        if(Random.value > 0.4)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossStates3[0].name3 == StateType3.NULL)
            {
                bossStates3[0].name3 = StateType3.PARALISIS;
                bossStates3[0].turnsLeft3 = 3;
            }
            else if (bossStates3[0].name3 == StateType3.NUMB || bossStates3[0].name3 == StateType3.GRIEF)
            {
                if (bossStates3[1].name3 == StateType3.NULL)
                {
                    bossStates3[1].name3 = StateType3.PARALISIS;
                    bossStates3[1].turnsLeft3 = 3;
                }
                else if (bossStates3[1].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.GRIEF)
                {
                    if (bossStates3[2].name3 == StateType3.NULL)
                    {
                        bossStates3[2].name3 = StateType3.PARALISIS;
                        bossStates3[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (bossStates3[0].name3 == StateType3.PARALISIS)
            {
                bossStates3[0].turnsLeft3 += 2;
                if (bossStates3[0].turnsLeft3 > 5)
                {
                    bossStates3[0].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[1].name3 == StateType3.PARALISIS)
            {
                bossStates3[1].turnsLeft3 += 2;
                if (bossStates3[1].turnsLeft3 > 5)
                {
                    bossStates3[1].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[2].name3 == StateType3.PARALISIS)
            {
                bossStates3[2].turnsLeft3 += 2;
                if (bossStates3[2].turnsLeft3 > 5)
                {
                    bossStates3[2].turnsLeft3 = 5;
                }
            }
        }

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

    public void RageSpell()
    {
        HideActions();
        Debug.Log("Used Rage");

        playerScript.moves--;
        playerScript.energy -= 5;

        int damage = (playerScript.stats.power * playerScript.powerMultiplier) * 2;
        StartCoroutine(RageSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Rage and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator RageSpellWaiter(int damage)
    {
        HideActions();
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ThrowProjectile(rageParticleHolder, rageParticleSystem, damage, sorrow1Audio, HitSpellAudio));
        yield return new WaitForSecondsRealtime(3);

        if(Random.value > 0.4)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossStates3[0].name3 == StateType3.NULL)
            {
                bossStates3[0].name3 = StateType3.NUMB;
                bossStates3[0].turnsLeft3 = 3;
            }
            else if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[0].name3 == StateType3.GRIEF)
            {
                if (bossStates3[1].name3 == StateType3.NULL)
                {
                    bossStates3[1].name3 = StateType3.NUMB;
                    bossStates3[1].turnsLeft3 = 3;
                }
                else if (bossStates3[1].name3 == StateType3.GRIEF || bossStates3[1].name3 == StateType3.PARALISIS)
                {
                    if (bossStates3[2].name3 == StateType3.NULL)
                    {
                        bossStates3[2].name3 = StateType3.NUMB;
                        bossStates3[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (bossStates3[0].name3 == StateType3.NUMB)
            {
                bossStates3[0].turnsLeft3 += 2;
                if (bossStates3[0].turnsLeft3 > 5)
                {
                    bossStates3[0].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[1].name3 == StateType3.NUMB)
            {
                bossStates3[1].turnsLeft3 += 2;
                if (bossStates3[1].turnsLeft3 > 5)
                {
                    bossStates3[1].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[2].name3 == StateType3.NUMB)
            {
                bossStates3[2].turnsLeft3 += 2;
                if (bossStates3[1].turnsLeft3 > 5)
                {
                    bossStates3[1].turnsLeft3 = 5;
                }
            }
        }
        

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
        HideActions();
        Debug.Log("Used Grief");

        playerScript.moves--;
        playerScript.energy -= 5;

        int damage = (playerScript.stats.power * playerScript.powerMultiplier) / 2;
        StartCoroutine(GriefSpellWaiter(damage));
        AddCombatText();
        combatDialogue[0].text = "Player used Grief and dealt" + damage.ToString();
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator GriefSpellWaiter(int damage)
    {
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ThrowProjectile(griefParticleHolder, griefParticleSystem, damage, sorrow2Audio, HitSpellAudio));
        yield return new WaitForSecondsRealtime(3);

        if(Random.value > 0.4)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossStates3[0].name3 == StateType3.NULL)
            {
                bossStates3[0].name3 = StateType3.GRIEF;
                bossStates3[0].turnsLeft3 = 3;
            }
            else if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[0].name3 == StateType3.NUMB)
            {
                if (bossStates3[1].name3 == StateType3.NULL)
                {
                    bossStates3[1].name3 = StateType3.GRIEF;
                    bossStates3[1].turnsLeft3 = 3;
                }
                else if (bossStates3[1].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.PARALISIS)
                {
                    if (bossStates3[2].name3 == StateType3.NULL)
                    {
                        bossStates3[2].name3 = StateType3.GRIEF;
                        bossStates3[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (bossStates3[0].name3 == StateType3.GRIEF)
            {
                bossStates3[0].turnsLeft3 += 2;
                if (bossStates3[0].turnsLeft3 > 5)
                {
                    bossStates3[0].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[1].name3 == StateType3.GRIEF)
            {
                bossStates3[1].turnsLeft3 += 2;
                if (bossStates3[1].turnsLeft3 > 5)
                {
                    bossStates3[1].turnsLeft3 = 5;
                }
            }
            else if (bossStates3[2].name3 == StateType3.GRIEF)
            {
                bossStates3[2].turnsLeft3 += 2;
                if (bossStates3[1].turnsLeft3 > 5)
                {
                    bossStates3[1].turnsLeft3 = 5;
                }
            }
        }

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

    public void CourageDrive()
    {
        HideActions();

        playerScript.moves--;
        playerScript.energy -= 4;

        if (state[0].name3 == StateType3.PARALISIS)
        {
            state[0].name3 = StateType3.NULL;
            state[0].turnsLeft3 = 0;
        }
        else if (state[1].name3 == StateType3.PARALISIS)
        {
            state[1].name3 = StateType3.NULL;
            state[1].turnsLeft3 = 0;
        }
        else if (state[2].name3 == StateType3.PARALISIS)
        {
            state[2].name3 = StateType3.NULL;
            state[2].turnsLeft3 = 0;
        }
        StartCoroutine(CourageDriveWaiter());
    }

    IEnumerator CourageDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        courageParticle.SetActive(true);
        //audio source clip
        //audio source play
        yield return new WaitForSecondsRealtime(0.5f);
        //popup text = curado de paralisis
        yield return new WaitForSecondsRealtime(1f);
        ParticleSystem ps = courageParticle.GetComponent<ParticleSystem>();
        ps.Stop();
        yield return new WaitForSecondsRealtime(1.5f);
        courageParticle.SetActive(false);
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();

        endedMove = true;
    }

    public void FocusDrive()
    {
        HideActions();

        playerScript.moves--;
        playerScript.energy -= 4;

        if (state[0].name3 == StateType3.NUMB)
        {
            state[0].name3 = StateType3.NULL;
            state[0].turnsLeft3 = 0;
        }
        else if (state[1].name3 == StateType3.NUMB)
        {
            state[1].name3 = StateType3.NULL;
            state[1].turnsLeft3 = 0;
        }
        else if (state[2].name3 == StateType3.NUMB)
        {
            state[2].name3 = StateType3.NULL;
            state[2].turnsLeft3 = 0;
        }
        StartCoroutine(FocusDriveWaiter());
    }

    IEnumerator FocusDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        focusParticle.SetActive(true);
        //audio source clip
        //audio source play
        yield return new WaitForSecondsRealtime(0.5f);
        //popup text = curado de paralisis
        yield return new WaitForSecondsRealtime(1f);
        ParticleSystem ps = focusParticle.GetComponent<ParticleSystem>();
        ps.Stop();
        yield return new WaitForSecondsRealtime(1.5f);
        focusParticle.SetActive(false);
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();

        endedMove = true;
    }

    public void WillDrive()
    {
        HideActions();

        playerScript.moves--;
        playerScript.energy -= 4;

        if (state[0].name3 == StateType3.GRIEF)
        {
            state[0].name3 = StateType3.NULL;
            state[0].turnsLeft3 = 0;
        }
        else if (state[1].name3 == StateType3.GRIEF)
        {
            state[1].name3 = StateType3.NULL;
            state[1].turnsLeft3 = 0;
        }
        else if (state[2].name3 == StateType3.GRIEF)
        {
            state[2].name3 = StateType3.NULL;
            state[2].turnsLeft3 = 0;
        }
        StartCoroutine(WillDriveWaiter());
    }

    IEnumerator WillDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        willParticle.SetActive(true);
        //audio source clip
        //audio source play
        yield return new WaitForSecondsRealtime(0.5f);
        //popup text = curado de paralisis
        yield return new WaitForSecondsRealtime(1f);
        ParticleSystem ps = willParticle.GetComponent<ParticleSystem>();
        ps.Stop();
        yield return new WaitForSecondsRealtime(1.5f);
        willParticle.SetActive(false);
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();

        endedMove = true;
    }

    private bool MoveToPosition(Vector3 enemy)
    {
        float animSpeed = 10.0f;
        return enemy != (player.transform.position = Vector3.MoveTowards(player.transform.position, enemy, animSpeed * Time.deltaTime));
    }

    // Boss Actions
    void Attack()
    {
        HideActions();
        if(bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if(Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed the attack due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Attack");
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
        }
        else
        {
            bossAnimator.Play("Attack");
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
    }

    IEnumerator BasicAtackWaiter(float d)
    {
        bossEndedMove = false;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        playerAnimator.Play("Damage");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);

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
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed the attack+ due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Attack");
                if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
                {
                    AddCombatText();
                    combatDialogue[0].text = "Attack Blocked";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(PlayerBlocked());
                }
                else
                {
                    bossAnimator.SetTrigger("Attack+");
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
            }
        }
        else
        {
            bossAnimator.SetTrigger("Attack+");
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
    }

    IEnumerator AttackPlusWaiter(float d)
    {
        bossEndedMove = false;
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        playerAnimator.Play("Damage");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);

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
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed the attack+ due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("EffectAttack");
                float dmg = bossScript.stats.strenght;
                StartCoroutine(EffectAttackWaiter(dmg));
                AddCombatText();
                combatDialogue[0].text = "Boss dealt " + dmg.ToString() + " damage.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            bossAnimator.Play("EffectAttack");
            float dmg = bossScript.stats.strenght;
            StartCoroutine(EffectAttackWaiter(dmg));
            AddCombatText();
            combatDialogue[0].text = "Boss dealt " + dmg.ToString() + " damage.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator EffectAttackWaiter(float d)
    {
        bossEndedMove = false;
        //frontalBossCamera.enabled = !frontalBossCamera.enabled;
        yield return new WaitForSecondsRealtime(3);
        //frontalBossCamera.enabled = !frontalBossCamera.enabled;
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;
        playerAnimator.Play("Damage");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

        int randomPercentage = Random.Range(0, 100);

        if (randomPercentage <= 33)
        {
            AddCombatText();
            combatDialogue[0].text = "Player is griefed.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (state[0].name3 == StateType3.NULL)
            {
                AddCombatText();
                combatDialogue[0].text = "Grief is now the first element of the array.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                state[0].name3 = StateType3.GRIEF;
                state[0].turnsLeft3 = 3;
            }
            else if (state[0].name3 == StateType3.NUMB || state[0].name3 == StateType3.PARALISIS)
            {
                if (state[1].name3 == StateType3.NULL)
                {
                    state[1].name3 = StateType3.GRIEF;
                    state[1].turnsLeft3 = 3;
                }
                else if (state[1].name3 == StateType3.NUMB || state[1].name3 == StateType3.PARALISIS)
                {
                    if (state[2].name3 == StateType3.NULL)
                    {
                        state[2].name3 = StateType3.GRIEF;
                        state[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (state[0].name3 == StateType3.GRIEF)
            {
                state[0].turnsLeft3 += 2;
                if (state[0].turnsLeft3 > 5)
                {
                    state[0].turnsLeft3 = 5;
                }
            }
            else if (state[1].name3 == StateType3.GRIEF)
            {
                state[1].turnsLeft3 += 2;
                if (state[1].turnsLeft3 > 5)
                {
                    state[1].turnsLeft3 = 5;
                }
            }
            else if (state[2].name3 == StateType3.GRIEF)
            {
                state[2].turnsLeft3 += 2;
                if (state[2].turnsLeft3 > 5)
                {
                    state[2].turnsLeft3 = 5;
                }
            }
        }
        else if (randomPercentage > 33 && randomPercentage <= 66)
        {
            AddCombatText();
            combatDialogue[0].text = "Player is numb.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (state[0].name3 == StateType3.NULL)
            {
                AddCombatText();
                combatDialogue[0].text = "Numb is now the first element of the array.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                state[0].name3 = StateType3.NUMB;
                state[0].turnsLeft3 = 3;
            }
            else if (state[0].name3 == StateType3.GRIEF || state[0].name3 == StateType3.PARALISIS)
            {
                if (state[1].name3 == StateType3.NULL)
                {
                    state[1].name3 = StateType3.NUMB;
                    state[1].turnsLeft3 = 3;
                }
                else if (state[1].name3 == StateType3.GRIEF || state[1].name3 == StateType3.PARALISIS)
                {
                    if (state[2].name3 == StateType3.NULL)
                    {
                        state[2].name3 = StateType3.NUMB;
                        state[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (state[0].name3 == StateType3.NUMB)
            {
                state[0].turnsLeft3 += 2;
                if (state[0].turnsLeft3 > 5)
                {
                    state[0].turnsLeft3 = 5;
                }
            }
            else if (state[1].name3 == StateType3.NUMB)
            {
                state[1].turnsLeft3 += 2;
                if (state[1].turnsLeft3 > 5)
                {
                    state[1].turnsLeft3 = 5;
                }
            }
            else if (state[2].name3 == StateType3.NUMB)
            {
                state[2].turnsLeft3 += 2;
                if (state[2].turnsLeft3 > 5)
                {
                    state[2].turnsLeft3 = 5;
                }
            }
        }
        else
        {
            AddCombatText();
            combatDialogue[0].text = "Player is paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (state[0].name3 == StateType3.NULL)
            {
                AddCombatText();
                combatDialogue[0].text = "Paralisis is now the first element of the array.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                state[0].name3 = StateType3.PARALISIS;
                state[0].turnsLeft3 = 3;
            }
            else if (state[0].name3 == StateType3.NUMB || state[0].name3 == StateType3.GRIEF)
            {
                if (state[1].name3 == StateType3.NULL)
                {
                    state[1].name3 = StateType3.PARALISIS;
                    state[1].turnsLeft3 = 3;
                }
                else if (state[1].name3 == StateType3.NUMB || state[1].name3 == StateType3.GRIEF)
                {
                    if (state[2].name3 == StateType3.NULL)
                    {
                        state[2].name3 = StateType3.PARALISIS;
                        state[2].turnsLeft3 = 3;
                    }
                }
            }
            else if (state[0].name3 == StateType3.PARALISIS)
            {
                state[0].turnsLeft3 += 2;
                if (state[0].turnsLeft3 > 5)
                {
                    state[0].turnsLeft3 = 5;
                }
            }
            else if (state[1].name3 == StateType3.PARALISIS)
            {
                state[1].turnsLeft3 += 2;
                if (state[1].turnsLeft3 > 5)
                {
                    state[1].turnsLeft3 = 5;
                }
            }
            else if (state[2].name3 == StateType3.PARALISIS)
            {
                state[2].turnsLeft3 += 2;
                if (state[2].turnsLeft3 > 5)
                {
                    state[2].turnsLeft3 = 5;
                }
            }
        }

        if (playerScript.armor > 0)
        {
            if (d >= playerScript.armor)
            {
                for (float i = d; d > 0; i--)
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
                for (float i = playerScript.armor; i > 0; i--)
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

    void ChargeAttack()
    {
        HideActions();

        bossAnimator.Play("Charge");
        StartCoroutine(ChargeWaiter());
        AddCombatText();
        combatDialogue[0].text = "Boss charged his Special Attack.";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator ChargeWaiter()
    {
        bossEndedMove = false;
        yield return new WaitForSecondsRealtime(3);
        if (bossScript.stats.charge == false)
        {
            bossScript.stats.charge = true;
        }

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void SpecialAttack()
    {
        HideActions();
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed the special attack due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Special");
                float dmg = bossScript.stats.strenght * 4;
                if (bossScript.stats.charge == true)
                {
                    StartCoroutine(SpecialAttackWaiter(dmg));
                }
            }
        }
        else
        {
            bossAnimator.Play("Special");
            float dmg = bossScript.stats.strenght * 4;
            if (bossScript.stats.charge == true)
            {
                StartCoroutine(SpecialAttackWaiter(dmg));
            }
        }
    }

    IEnumerator SpecialAttackWaiter(float d)
    {
        bossEndedMove = false;
        yield return new WaitForSecondsRealtime(3);
        playerAnimator.Play("HitReaction");
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);

        if (playerScript.armor > 0)
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
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed guard due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Guard");
                float armor = bossScript.armor + 50;
                StartCoroutine(GuardBossWaiter(armor));
                AddCombatText();
                combatDialogue[0].text = "Boss has " + armor.ToString() + " armor now.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            bossAnimator.Play("Guard");
            float armor = bossScript.armor + 50;
            StartCoroutine(GuardBossWaiter(armor));
            AddCombatText();
            combatDialogue[0].text = "Boss has " + armor.ToString() + " armor now.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator GuardBossWaiter(float h)
    {
        bossEndedMove = false;
        //Aumento en la cantidad de block del boss
        for (float i = h; i > 0; i--)
        {
            bossScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(3);

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void Heal()
    {
        HideActions();
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed heal due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else if (bossStates3[0].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.NUMB || bossStates3[2].name3 == StateType3.NUMB)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed heal because is numb";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Heal");
                float heal = bossScript.stats.vigor * 2.5f;
                StartCoroutine(HealWaiter(heal));
                AddCombatText();
                combatDialogue[0].text = "Boss healed for " + heal.ToString() + " HP";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else if (bossStates3[0].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.NUMB || bossStates3[2].name3 == StateType3.NUMB)
        {
            AddCombatText();
            combatDialogue[0].text = "Boss failed heal because is numb";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else
        {
            bossAnimator.Play("Heal");
            float heal = bossScript.stats.vigor * 2.5f;
            StartCoroutine(HealWaiter(heal));
            AddCombatText();
            combatDialogue[0].text = "Boss healed for " + heal.ToString() + " HP";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator HealWaiter(float h)
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

    void HealPlus()
    {
        HideActions();
        if (bossStates3[0].name3 == StateType3.PARALISIS || bossStates3[1].name3 == StateType3.PARALISIS || bossStates3[2].name3 == StateType3.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "Player failed heal+ due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else if (bossStates3[0].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.NUMB || bossStates3[2].name3 == StateType3.NUMB)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed heal because is numb";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                bossAnimator.Play("Heal+");
                float heal = bossScript.stats.vigor * 3.5f;
                StartCoroutine(HealPlusWaiter(heal));
            }
        }
        else if (bossStates3[0].name3 == StateType3.NUMB || bossStates3[1].name3 == StateType3.NUMB || bossStates3[2].name3 == StateType3.NUMB)
        {
            AddCombatText();
            combatDialogue[0].text = "Boss failed heal+ because is numb";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        else
        {
            bossAnimator.Play("Heal+");
            float heal = bossScript.stats.vigor * 3.5f;
            StartCoroutine(HealPlusWaiter(heal));
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        backgroundAudio[0].Pause();
        backgroundAudio[1].Play();
        gamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        backgroundAudio[0].UnPause();
        exitGameMenu.SetActive(false);
        backMenuMenu.SetActive(false);
        optionsMenuMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

    IEnumerator GriefLifePlayer(float damage)
    {
        bossEndedMove = false;
        playerAnimator.Play("HitReaction");
        for (int i = 0; i < damage; i++)
        {
            playerScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSecondsRealtime(2);
    }

    IEnumerator GriefLifeBoss(float damage)
    {
        AddCombatText();
        combatDialogue[0].text = "Started coroutine";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
        bossEndedMove = false;
        bossAnimator.Play("Damage");
        for (int i = 0; i < damage; i++)
        {
            bossScript.health--;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(2);
    }
}
