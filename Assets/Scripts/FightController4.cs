﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.Events;

public class FightController4 : MonoBehaviour {

    public enum StateType4 { NULL, GRIEF, PARALISIS, NUMB };

    public struct State4
    {
        public StateType4 name4;
        public int turnsLeft4;
    }

    enum BuffStateType4 { NULL }
    enum DebuffStateType4 { NULL }
    struct Buff
    {
        public BuffStateType4 StateType4;
        public int remainingTurns;
    }
    struct Debuff
    {
        public DebuffStateType4 StateType4;
        public int remainingTurns;
    }

    public struct Orbs
    {
        public int quantity;
    };
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
    private float griefLifeDivider = 1;
    //Velocidad de movimiento del player cada vez que se acerca al boss para atacar
    float speed = 10.0f;

    //GAMEOBJECTS
    public GameObject armorEffect;
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
    public GameObject revolver;
    private Material revolverMaterial;
    Vector3 playerInitPos;
    private Player playerScript;
    private Buff[] playerBuff;
    private Debuff[] playerDebuff;
    public State4[] states4;

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
    private Boss4 bossScript;
    public State4[] bossstates4;

    //UI BOSS
    public Slider bossHealthBar;
    public Text bossHealthNumber;
    public Text bossArmorNumber;

    //UI WHEN YOU WIN
    public GameObject winCanvas;
    public Text orbsText;
    public Text moneyText;
    public Button continueButton;

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
    public GameObject graceParticle;
    //public GameObject heavyAttackHolder;
    //public ParticleSystem heavyAttackParticle;
    //STATE PARTICLE EFFECTS
    public ParticleSystem paralizeEffect;
    public ParticleSystem numbEffect;
    public ParticleSystem griefEffect;

    //BOSS PARTICLE SYSTEMS
    public GameObject specialParticlesHolder;
    public ParticleSystem specialParticles;
    public GameObject chargeParticlesHolder;
    public ParticleSystem chargeParticles;
    public GameObject guardParticlesHolder;
    public ParticleSystem guardParticles;
    public GameObject healParticlesBoss;

    //CAMERAS PARA LAS CINEMATICAS SON TODAS DEL CINEMACHINE; LA VCAM1 ES LA CAMARA POR DEFECTO
    public GameObject CM_vcam1;
    public GameObject CM_vcam2;

    //VARIABLES CAMERA SHAKE
    public CinemachineVirtualCamera mainVCam; //variable de la main virtual camera
    private CinemachineBasicMultiChannelPerlin vCamNoise; //acceso a los ajustos de ruido de la camara (perlin noise).
    float shakeAmplitudeLight = 0.07f;
    float shakeFrequencyLight = 2.5f;

    //PARTICULAS CINEMATICA INICIAL
    public Camera mainCamera;
    public GameObject teleportAura;
    public GameObject teleportOrb;

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

    //Cooldowns Sorrows
    bool griefCooldown;
    bool rageCooldown;
    bool terrorCooldown;
    //Cooldowns Drives
    bool graceCooldown;
    bool willCooldown;
    bool focusCooldown;
    bool courageCooldown;

    //Sounds
    private AudioSource audioSource;
    public AudioClip lightStrikeAudio;
    public AudioClip heavyStrikeAudio;
    public AudioClip guardAudio;
    public AudioClip despairAudio;
    public AudioClip healAudio;
    public AudioClip sorrow1Audio;
    public AudioClip sorrow2Audio;
    //DrivesAudio
    public AudioClip graceAudio;
    public AudioClip willAudio;
    public AudioClip focusAudio;
    public AudioClip courageAudio;
    //BossSounds
    public AudioClip attackplusAudio;
    public AudioClip chargeAudio;
    public AudioClip effectAttackAudio;
    public AudioClip specialAttackAudio;
    public AudioClip bossHealAudio;
    public AudioClip receiveDamageAudio;
    public AudioClip deathAudio;
    //CinematicSounds
    public AudioClip introAudio;
    public AudioClip teleportAudio;

    public AudioClip HitSpellAudio;
    public AudioClip HitStrikeAudio;

    //EXTRA VARIABLES
    float skyboxRotSpeed;

    void Start()
    {
        HideActions();

        //Hides revolver when the battle starts, it only appears when using light attack
        revolverMaterial = revolver.GetComponent<Renderer>().material;
        revolverMaterial.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        //GETTING NOISE PROFILE FOR THE CAMERA SHAKE
        vCamNoise = mainVCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        vCamNoise.m_FrequencyGain = 0; //setting all noise camera elements to 0, to not shake when starting the battle
        vCamNoise.m_AmplitudeGain = 0;

        //GETTING QUANTITY OF ORBS
        orbs.quantity = PlayerPrefs.GetInt("ORBS");

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

        turn = 0; //Turno inicial
        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss4>();
        bossAnimator = boss.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();

        //Particles
        //healParticle.Stop();
        hitParticle.Stop();

        playerInitPos = player.transform.position;

        //Buttons
        lightAttackButton.onClick.AddListener(LightAttack);
        heavyAttackButton.onClick.AddListener(HeavyAttack);
        basicHealButton.onClick.AddListener(BasicHeal);
        basicSpellButton.onClick.AddListener(BasicSpell);
        guardButton.onClick.AddListener(Guard);
        spiritBlastButton.onClick.AddListener(SpiritBlast);
        buttonRage.onClick.AddListener(RageSpell);
        buttonGrief.onClick.AddListener(GriefSpell);
        buttonTerror.onClick.AddListener(TerrorSpell);
        //Cameras
        mainCamera.enabled = true;
        //frontalPlayerCamera.enabled = false;
        //frontalBossCamera.enabled = false;
        //heavyAttackCam.enabled = false;
        //Buffs & Debuffs
        playerBuff = new Buff[2];
        playerBuff[0].StateType4 = BuffStateType4.NULL;
        playerBuff[0].remainingTurns = 0;
        playerBuff[1].StateType4 = BuffStateType4.NULL;
        playerBuff[1].remainingTurns = 0;
        playerDebuff = new Debuff[2];
        playerDebuff[0].StateType4 = DebuffStateType4.NULL;
        playerDebuff[0].remainingTurns = 0;
        playerDebuff[1].StateType4 = DebuffStateType4.NULL;
        playerDebuff[1].remainingTurns = 0;

        //Player states4
        states4 = new State4[3];
        states4[0].name4 = StateType4.NULL;
        states4[0].turnsLeft4 = 0;
        states4[1].name4 = StateType4.NULL;
        states4[1].turnsLeft4 = 0;
        states4[2].name4 = StateType4.NULL;
        states4[2].turnsLeft4 = 0;

        //Boss states4
        bossstates4 = new State4[3];
        bossstates4[0].name4 = StateType4.NULL;
        bossstates4[0].turnsLeft4 = 0;
        bossstates4[1].name4 = StateType4.NULL;
        bossstates4[1].turnsLeft4 = 0;
        bossstates4[2].name4 = StateType4.NULL;
        bossstates4[2].turnsLeft4 = 0;

        RefreshUI();

        audioSource = this.GetComponent<AudioSource>();

        //CONTROL DE LA CINEMATICA INICIAL
        CM_vcam1.SetActive(true);
        CM_vcam2.SetActive(false);
        player.SetActive(false);
        teleportAura.SetActive(false);
        teleportOrb.SetActive(false);

        StartCoroutine(AuraWaiterCinem());
        StartCoroutine(OrbWaiterCinem());
        StartCoroutine(PlayerWaiterCinem());
        StartCoroutine(BossWaiterCinem());

        //Extra variables initialization
        skyboxRotSpeed = 1;
    }

    void Update()
    {
        //Skybox Rotation
        //RenderSettings.skybox.SetFloat("SkyboxRot", Time.deltaTime * skyboxRotSpeed);

        if (Input.GetKeyDown("escape"))
        {
            if (!gamePaused)
                PauseGame();
            else
                UnPauseGame();
        }
        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene("Fight3");
        }
        if (Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene("Lobby");
        }

        playerScript = player.GetComponent<Player>();
        bossScript = boss.GetComponent<Boss4>();

        //Repartidor de turnos
        if (bossScript.health > 0 && playerScript.health > 0)
        {
            if (turn % 2 == 0)
            {
                if (bossEndedMove)
                {
                    //When the player is grief he loses some life.
                    if (states4[0].name4 == StateType4.GRIEF || states4[1].name4 == StateType4.GRIEF || states4[2].name4 == StateType4.GRIEF)
                    {
                        float damage = playerScript.maxHealth * (griefLifeDivider / 16);
                        AddCombatText();
                        combatDialogue[0].text = "Player lost " + damage.ToString() + "due to Grief";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        StartCoroutine(StateEffectFeedback(griefEffect));
                        StartCoroutine(GriefLifePlayer(damage));
                        griefLifeDivider += 1;
                    }
                    //Player's turn
                    if (playerScript.moves == 3 && playerScript.armor > 0)//si tenia armadura equipada se retira ya que solo dura 1 turno
                    {
                        playerScript.armor = 0;
                        RefreshUI();
                    }
                    //if (playerScript.armor == 0)
                    //    //armorEffect.SetActive(false);

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
                    //DECREASE TURNS LEFT OF THE PLAYER states4
                    if ((states4[0].name4 == StateType4.GRIEF || states4[0].name4 == StateType4.NUMB || states4[0].name4 == StateType4.PARALISIS) && states4[0].turnsLeft4 > 0)
                    {
                        states4[0].turnsLeft4 -= 1;
                        if (states4[0].turnsLeft4 == 0)
                        {
                            if (states4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            states4[0].name4 = StateType4.NULL;
                        }
                    }

                    if ((states4[1].name4 == StateType4.GRIEF || states4[1].name4 == StateType4.NUMB || states4[1].name4 == StateType4.PARALISIS) && states4[1].turnsLeft4 > 0)
                    {
                        states4[1].turnsLeft4 -= 1;
                        if (states4[1].turnsLeft4 == 0)
                        {
                            if (states4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            states4[1].name4 = StateType4.NULL;
                        }
                    }

                    if ((states4[2].name4 == StateType4.GRIEF || states4[2].name4 == StateType4.NUMB || states4[2].name4 == StateType4.PARALISIS) && states4[2].turnsLeft4 > 0)
                    {
                        states4[2].turnsLeft4 -= 1;
                        if (states4[2].turnsLeft4 == 0)
                        {
                            if (states4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            states4[2].name4 = StateType4.NULL;
                        }
                    }

                    //DECREASE TURNS LEFT OF THE BOSS states4
                    if ((bossstates4[0].name4 == StateType4.GRIEF || bossstates4[0].name4 == StateType4.NUMB || bossstates4[0].name4 == StateType4.PARALISIS) && bossstates4[0].turnsLeft4 > 0)
                    {
                        bossstates4[0].turnsLeft4 -= 1;
                        if (bossstates4[0].turnsLeft4 == 0)
                        {
                            if (bossstates4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossstates4[0].name4 = StateType4.NULL;
                        }
                    }

                    if ((bossstates4[1].name4 == StateType4.GRIEF || bossstates4[1].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.PARALISIS) && bossstates4[1].turnsLeft4 > 0)
                    {
                        bossstates4[1].turnsLeft4 -= 1;
                        if (bossstates4[1].turnsLeft4 == 0)
                        {
                            if (bossstates4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossstates4[1].name4 = StateType4.NULL;
                        }
                    }

                    if ((bossstates4[2].name4 == StateType4.GRIEF || bossstates4[2].name4 == StateType4.NUMB || bossstates4[2].name4 == StateType4.PARALISIS) && bossstates4[2].turnsLeft4 > 0)
                    {
                        bossstates4[2].turnsLeft4 -= 1;
                        if (bossstates4[2].turnsLeft4 == 0)
                        {
                            if (bossstates4[2].name4 == StateType4.GRIEF)
                            {
                                griefLifeDivider = 1;
                            }
                            bossstates4[2].name4 = StateType4.NULL;
                        }
                    }

                    //Boss's turn

                    if (bossScript.health >= 700)
                    {
                        //When the boss is numb he losses some life
                        if (bossstates4[0].name4 == StateType4.GRIEF || bossstates4[1].name4 == StateType4.GRIEF || bossstates4[2].name4 == StateType4.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (griefLifeDivider / 16);
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
                                Heal();
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
                        //When the boss is numb he losses some life
                        if (bossstates4[0].name4 == StateType4.GRIEF || bossstates4[1].name4 == StateType4.GRIEF || bossstates4[2].name4 == StateType4.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (griefLifeDivider / 16);
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
                        //When the boss is numb he losses some life
                        if (bossstates4[0].name4 == StateType4.GRIEF || bossstates4[1].name4 == StateType4.GRIEF || bossstates4[2].name4 == StateType4.GRIEF)
                        {
                            float damage = bossScript.maxHealth * (griefLifeDivider / 16);
                            AddCombatText();
                            combatDialogue[0].text = "Boss lost " + damage.ToString() + " due to Grief";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                            StartCoroutine(GriefLifeBoss(damage));
                            griefLifeDivider += 2;
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
                        Debug.Log(states4[i].name4);
                        Debug.Log(states4[i].turnsLeft4);
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
            if (bossScript.health > 0) //WHAT IF YOU LOSE THE BATTLE
            {
                playerScript.health = 0;
                //Boss gana
                Debug.Log("Boss gana");
            }
            else
            {
                StartCoroutine(WinScene());
            }
        }
    }

    //UI Info
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
            if (sorrows.grief > 0 && griefCooldown == false) buttonGrief.interactable = true;
            else buttonGrief.interactable = false;
            if (sorrows.terror > 0 && terrorCooldown == false) buttonTerror.interactable = true;
            else buttonTerror.interactable = false;
            if (sorrows.rage > 0 && rageCooldown == false) buttonRage.interactable = true;
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
            if (drives.courage > 0 && courageCooldown == false) buttonCourage.interactable = true;
            else buttonCourage.interactable = false;
            if (drives.focus > 0 && focusCooldown == false) buttonFocus.interactable = true;
            else buttonFocus.interactable = false;
            if (drives.grace > 0 && graceCooldown == false) buttonGrace.interactable = true;
            else buttonGrace.interactable = false;
            if (drives.remembrance > 0) buttonRemembrance.interactable = true;
            else buttonRemembrance.interactable = false;
            if (drives.spiritualHealing > 0) buttonSpiritualHealing.interactable = true;
            else buttonSpiritualHealing.interactable = false;
            if (drives.will > 0 && willCooldown == false) buttonWill.interactable = true;
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
            if (states4[i].name4 == StateType4.NUMB)
            {
                StartCoroutine(StateEffectFeedback(numbEffect));
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
        griefCooldown = false;
        rageCooldown = false;
        terrorCooldown = false;
        graceCooldown = false;
        willCooldown = false;
        focusCooldown = false;
        courageCooldown = false;
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
        Vector3 newPosition = new Vector3(3.91f, 4.89f, -5.43f);
        Quaternion newRotation = Quaternion.Euler(0, 90, 0);
        GameObject popupClone = Instantiate(popupText, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();
    }

    public void ShowPopupTextPlayer(float damage, Color color)
    {
        Vector3 newPosition = new Vector3(-7.13f, 1.98f, -6.83f);
        Quaternion newRotation = Quaternion.Euler(0, 90, 0);
        GameObject popupClone = Instantiate(popupTextPlayer, newPosition, newRotation);
        popupClone.GetComponent<TextMesh>().color = color;
        popupClone.GetComponent<TextMesh>().text = damage.ToString();

    }

    public void ShowFailText(Color color)
    {
        Vector3 newPosition = new Vector3(-7.13f, 1.98f, -6.83f);
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject failClone = Instantiate(popupText, newPosition, newRotation);
        failClone.GetComponent<TextMesh>().color = color;
        failClone.GetComponent<TextMesh>().text = "MISS";
    }

    public void ShowFailTextPlayer(Color color)
    {
        Vector3 newPosition = new Vector3(3.91f, 4.89f, -5.43f);
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
        GameObject failClone = Instantiate(popupText, newPosition, newRotation);
        failClone.GetComponent<TextMesh>().color = color;
        failClone.GetComponent<TextMesh>().text = "MISS";
    }

    //Character Actions
    void LightAttack()
    {
        HideActions();

        if (states4[0].name4 != StateType4.PARALISIS || states4[1].name4 != StateType4.PARALISIS || states4[2].name4 != StateType4.PARALISIS)
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
            if (states4[i].name4 == StateType4.PARALISIS)
            {
                if (Random.value > 0.1)
                {
                    if (usedLightAttack1 == false && usedLightAttack2 == false)
                    {
                        playerScript.energy -= 3;
                        playerScript.moves--;

                        if (Random.Range(0, 20) == 1) //critico
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
                    Debug.Log("You can't attack, your player is paralized");
                    playerScript.energy -= 3;
                    playerScript.moves--;
                    AddCombatText();
                    combatDialogue[0].text = "You can't attack, your player is paralized";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
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
        Vector3 initialPos = new Vector3(-6.207f, 0.49f, -6.569f);
        Vector3 finalPos = new Vector3(0.43f, 2.16f, -5.85f);
        float timeToReachTarget = Vector3.Distance(initialPos, finalPos) / speed;

        float time = 0.0f;
        playerAnimator.Play("LightAttack");
        revolverMaterial.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
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
        revolverMaterial.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        Destroy(lightStrikeClone);
        GameObject hitClone = Instantiate(hitHolder, finalPos, Quaternion.identity);
        audioSource.clip = receiveDamageAudio;
        audioSource.Play();
        bossAnimator.Play("Damage");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        ShowPopupText(damage, Color.red);
    }

    IEnumerator LightAttackWaiter(int d)
    {
        endedMove = false;
        StartCoroutine(ThrowLightStrike(lightStrikeHolder, lightStrikeParticleSystem, d));
        player.transform.rotation = Quaternion.Euler(0f, 170.944f, 0f);
        yield return new WaitForSecondsRealtime(3f); //Tiempo de espera de la animación
        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
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

        if (states4[0].name4 != StateType4.PARALISIS || states4[1].name4 != StateType4.PARALISIS || states4[2].name4 != StateType4.PARALISIS)
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
            if (states4[i].name4 == StateType4.PARALISIS)
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
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
                }
            }
        }
    }

    IEnumerator HeavyAttackWaiter(int d)
    {
        endedMove = false;
        playerAnimator.Play("HeavyAttack");
        yield return new WaitForSecondsRealtime(0.7f);
        audioSource.clip = heavyStrikeAudio;
        audioSource.Play();
        bossAnimator.Play("Damage");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(1.6f);
        player.transform.rotation = Quaternion.Euler(0, 81.5f, 0);
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

        if (states4[0].name4 != StateType4.PARALISIS || states4[1].name4 != StateType4.PARALISIS || states4[2].name4 != StateType4.PARALISIS)
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
                    ShowFailTextPlayer(Color.red);
                    AddCombatText();
                    combatDialogue[0].text = "Player failed heal";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (states4[i].name4 == StateType4.PARALISIS)
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
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
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

        endedMove = true;
    }

    void BasicSpell() //It's the despair one
    {
        HideActions();

        if (states4[0].name4 != StateType4.PARALISIS || states4[1].name4 != StateType4.PARALISIS || states4[2].name4 != StateType4.PARALISIS)
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

                if (Random.value > 0.3)
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

        //Aplicación de Paralisis
        for (int i = 0; i < 3; i++)
        {
            if (states4[i].name4 == StateType4.PARALISIS)
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
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
                    if (playerScript.moves > 0 && playerScript.energy > 2)
                    {
                        ShowActions();
                    }
                }
            }
        }
    }

    IEnumerator ThrowProjectile(GameObject particleHolder, ParticleSystem particleSystem, int damage, AudioClip auxClip)
    {
        float speed = 20.0f;
        Vector3 initialPos = new Vector3(-6.024f, 0.178f, -6.852f);
        Vector3 finalPos = new Vector3(1.27f, 1.89f, -5.8f);
        GameObject despairClone = Instantiate(particleHolder, initialPos, Quaternion.identity);
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
        audioSource.clip = receiveDamageAudio;
        audioSource.Play();
        bossAnimator.Play("Damage");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        ShowPopupText(damage, Color.red);
    }

    IEnumerator BasicSpellWaiter(int d)
    {
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.6f); //Tiempo de espera de la animación
        StartCoroutine(ThrowProjectile(despairParticleHolder, despairParticleSystem, d, despairAudio));
        yield return new WaitForSecondsRealtime(3);
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

        if (states4[0].name4 != StateType4.PARALISIS || states4[1].name4 != StateType4.PARALISIS || states4[2].name4 != StateType4.PARALISIS)
        {
            if (usedGuard == false)
            {
                playerScript.energy -= 4;
                playerScript.moves--;

                float armored = playerScript.stats.endurance * playerScript.enduranceMultiplier;
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
                    float armored = playerScript.stats.endurance * playerScript.enduranceMultiplier;
                    StartCoroutine(GuardWaiter(armored));
                    AddCombatText();
                    combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
                else
                {
                    Debug.Log("Player failed protecting");
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
            if (states4[i].name4 == StateType4.PARALISIS)
            {
                if (Random.value > 0.4)
                {
                    if (usedGuard == false)
                    {
                        playerScript.energy -= 4;
                        playerScript.moves--;

                        float armored = playerScript.stats.endurance * playerScript.enduranceMultiplier;
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
                            float armored = playerScript.stats.endurance * playerScript.enduranceMultiplier;
                            StartCoroutine(GuardWaiter(armored));
                            AddCombatText();
                            combatDialogue[0].text = "Player covered himself with " + armored.ToString() + " armor";
                            combatDialogue[0].color = new Color(1, 1, 1, 1);
                        }
                        else
                        {
                            Debug.Log("Player failed protecting");
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
                    AddCombatText();
                    combatDialogue[0].text = "Player failed to prtect due to paralisis.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(StateEffectFeedback(paralizeEffect));
                    ShowActions();
                }
            }

            else if (states4[i].name4 == StateType4.GRIEF)
            {
                if (Random.value > 0.7)
                {
                    states4[i].name4 = StateType4.NULL;
                    states4[i].turnsLeft4 = 0;
                    AddCombatText();
                    combatDialogue[0].text = "GRIEF Effect disapear.";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
            }
        }
    }

    IEnumerator GuardWaiter(float d)
    {
        audioSource.clip = guardAudio;
        audioSource.Play();
        endedMove = false;
        playerAnimator.Play("Guard");
        armorEffect.SetActive(true);
        yield return new WaitForSecondsRealtime(2f); //Tiempo de espera de la animación        
        armorEffect.SetActive(false);
        playerScript.blockChance += 5;
        if (playerScript.moves > 0 && playerScript.energy > 2)
            ShowActions();
        RefreshUI();
        for (float i = d; i > 0; i--)
        {
            playerScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }

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
        yield return new WaitForSecondsRealtime(3); //Tiempo de espera de la animación
        StartCoroutine(ThrowProjectile(animaBlastParticleHolder, animaBlastParticleSystem, d, sorrow2Audio));
        ShowPopupText(d, Color.red);
        yield return new WaitForSecondsRealtime(3);
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

    //SORROWS - Estos producen un efecto en el boss
    public void TerrorSpell()
    {
        HideActions();
        Debug.Log("Used Terror");

        if (terrorCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 5;

            int damage = playerScript.stats.power * playerScript.powerMultiplier;
            StartCoroutine(TerrorSpellWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player used Terror and dealt" + damage.ToString();
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            terrorCooldown = true;
        }
    }

    IEnumerator TerrorSpellWaiter(int damage)
    {
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ThrowProjectile(terrorParticleHolder, terrorParticleSystem, damage, sorrow2Audio));
        yield return new WaitForSecondsRealtime(3);
        ShowPopupText(damage, Color.red);

        if (Random.value > 0.4)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossstates4[0].name4 == StateType4.NULL)
            {
                bossstates4[0].name4 = StateType4.PARALISIS;
                bossstates4[0].turnsLeft4 = 3;
            }
            else if (bossstates4[0].name4 == StateType4.NUMB || bossstates4[0].name4 == StateType4.GRIEF)
            {
                if (bossstates4[1].name4 == StateType4.NULL)
                {
                    bossstates4[1].name4 = StateType4.PARALISIS;
                    bossstates4[1].turnsLeft4 = 3;
                }
                else if (bossstates4[1].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.GRIEF)
                {
                    if (bossstates4[2].name4 == StateType4.NULL)
                    {
                        bossstates4[2].name4 = StateType4.PARALISIS;
                        bossstates4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (bossstates4[0].name4 == StateType4.PARALISIS)
            {
                bossstates4[0].turnsLeft4 += 2;
                if (bossstates4[0].turnsLeft4 > 5)
                {
                    bossstates4[0].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[1].name4 == StateType4.PARALISIS)
            {
                bossstates4[1].turnsLeft4 += 2;
                if (bossstates4[1].turnsLeft4 > 5)
                {
                    bossstates4[1].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[2].name4 == StateType4.PARALISIS)
            {
                bossstates4[2].turnsLeft4 += 2;
                if (bossstates4[2].turnsLeft4 > 5)
                {
                    bossstates4[2].turnsLeft4 = 5;
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

        if (rageCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 5;

            int damage = (playerScript.stats.power * playerScript.powerMultiplier) * 2;
            StartCoroutine(RageSpellWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player used Rage and dealt" + damage.ToString();
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            rageCooldown = true;
        }
    }

    IEnumerator RageSpellWaiter(int damage)
    {
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ThrowProjectile(rageParticleHolder, rageParticleSystem, damage, sorrow1Audio));
        yield return new WaitForSecondsRealtime(3);
        ShowPopupText(damage, Color.red);

        if (Random.value > 0.4)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossstates4[0].name4 == StateType4.NULL)
            {
                bossstates4[0].name4 = StateType4.NUMB;
                bossstates4[0].turnsLeft4 = 3;
            }
            else if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.GRIEF)
            {
                if (bossstates4[1].name4 == StateType4.NULL)
                {
                    bossstates4[1].name4 = StateType4.NUMB;
                    bossstates4[1].turnsLeft4 = 3;
                }
                else if (bossstates4[1].name4 == StateType4.GRIEF || bossstates4[1].name4 == StateType4.PARALISIS)
                {
                    if (bossstates4[2].name4 == StateType4.NULL)
                    {
                        bossstates4[2].name4 = StateType4.NUMB;
                        bossstates4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (bossstates4[0].name4 == StateType4.NUMB)
            {
                bossstates4[0].turnsLeft4 += 2;
                if (bossstates4[0].turnsLeft4 > 5)
                {
                    bossstates4[0].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[1].name4 == StateType4.NUMB)
            {
                bossstates4[1].turnsLeft4 += 2;
                if (bossstates4[1].turnsLeft4 > 5)
                {
                    bossstates4[1].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[2].name4 == StateType4.NUMB)
            {
                bossstates4[2].turnsLeft4 += 2;
                if (bossstates4[1].turnsLeft4 > 5)
                {
                    bossstates4[1].turnsLeft4 = 5;
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

        if (griefCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 5;

            int damage = (playerScript.stats.power * playerScript.powerMultiplier) / 2;
            StartCoroutine(GriefSpellWaiter(damage));
            AddCombatText();
            combatDialogue[0].text = "Player used Grief and dealt" + damage.ToString();
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            griefCooldown = true;
        }
    }

    IEnumerator GriefSpellWaiter(int damage)
    {
        endedMove = false;
        playerAnimator.Play("Despair");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ThrowProjectile(griefParticleHolder, griefParticleSystem, damage, sorrow2Audio));
        yield return new WaitForSecondsRealtime(3);
        ShowPopupText(damage, Color.red);

        if (Random.value > 0.20)
        {
            AddCombatText();
            combatDialogue[0].text = "The enemy is now griefed.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            if (bossstates4[0].name4 == StateType4.NULL)
            {
                AddCombatText();
                combatDialogue[0].text = "grief pos0";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                bossstates4[0].name4 = StateType4.GRIEF;
                bossstates4[0].turnsLeft4 = 3;
            }
            else if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.NUMB)
            {
                if (bossstates4[1].name4 == StateType4.NULL)
                {
                    AddCombatText();
                    combatDialogue[0].text = "grief pos1";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    bossstates4[1].name4 = StateType4.GRIEF;
                    bossstates4[1].turnsLeft4 = 3;
                }
                else if (bossstates4[1].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.PARALISIS)
                {
                    if (bossstates4[2].name4 == StateType4.NULL)
                    {
                        AddCombatText();
                        combatDialogue[0].text = "grief pos2";
                        combatDialogue[0].color = new Color(1, 1, 1, 1);
                        bossstates4[2].name4 = StateType4.GRIEF;
                        bossstates4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (bossstates4[0].name4 == StateType4.GRIEF)
            {
                AddCombatText();
                combatDialogue[0].text = "grief pos0 + 2";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                bossstates4[0].turnsLeft4 += 2;
                if (bossstates4[0].turnsLeft4 > 5)
                {
                    bossstates4[0].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[1].name4 == StateType4.GRIEF)
            {
                AddCombatText();
                combatDialogue[0].text = "grief pos1 + 2";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                bossstates4[1].turnsLeft4 += 2;
                if (bossstates4[1].turnsLeft4 > 5)
                {
                    bossstates4[1].turnsLeft4 = 5;
                }
            }
            else if (bossstates4[2].name4 == StateType4.GRIEF)
            {
                AddCombatText();
                combatDialogue[0].text = "grief pos2 + 2";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                bossstates4[2].turnsLeft4 += 2;
                if (bossstates4[1].turnsLeft4 > 5)
                {
                    bossstates4[1].turnsLeft4 = 5;
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

    //DRIVES courage, focus, will
    public void CourageDrive()
    {
        HideActions();

        if (courageCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 4;

            if (states4[0].name4 == StateType4.PARALISIS)
            {
                states4[0].name4 = StateType4.NULL;
                states4[0].turnsLeft4 = 0;
            }
            else if (states4[1].name4 == StateType4.PARALISIS)
            {
                states4[1].name4 = StateType4.NULL;
                states4[1].turnsLeft4 = 0;
            }
            else if (states4[2].name4 == StateType4.PARALISIS)
            {
                states4[2].name4 = StateType4.NULL;
                states4[2].turnsLeft4 = 0;
            }
            StartCoroutine(CourageDriveWaiter());
            courageCooldown = true;
        }
    }

    IEnumerator CourageDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        courageParticle.SetActive(true);
        audioSource.clip = courageAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(0.5f);
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

        if (focusCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 4;

            if (states4[0].name4 == StateType4.NUMB)
            {
                states4[0].name4 = StateType4.NULL;
                states4[0].turnsLeft4 = 0;
            }
            else if (states4[1].name4 == StateType4.NUMB)
            {
                states4[1].name4 = StateType4.NULL;
                states4[1].turnsLeft4 = 0;
            }
            else if (states4[2].name4 == StateType4.NUMB)
            {
                states4[2].name4 = StateType4.NULL;
                states4[2].turnsLeft4 = 0;
            }
            StartCoroutine(FocusDriveWaiter());
            focusCooldown = true;
        }
    }

    IEnumerator FocusDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        focusParticle.SetActive(true);
        audioSource.clip = focusAudio;
        audioSource.Play();
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

        if (willCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 4;

            if (states4[0].name4 == StateType4.GRIEF)
            {
                states4[0].name4 = StateType4.NULL;
                states4[0].turnsLeft4 = 0;
            }
            else if (states4[1].name4 == StateType4.GRIEF)
            {
                states4[1].name4 = StateType4.NULL;
                states4[1].turnsLeft4 = 0;
            }
            else if (states4[2].name4 == StateType4.GRIEF)
            {
                states4[2].name4 = StateType4.NULL;
                states4[2].turnsLeft4 = 0;
            }
            StartCoroutine(WillDriveWaiter());
            willCooldown = true;
        }
    }

    IEnumerator WillDriveWaiter()
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        willParticle.SetActive(true);
        audioSource.clip = willAudio;
        audioSource.Play();
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

    public void GraceDrive()
    {
        HideActions();
        if (graceCooldown == false)
        {
            playerScript.moves--;
            playerScript.energy -= 4;

            float healing = 3;
            StartCoroutine(GraceDriveWaiter(healing));

            AddCombatText();
            combatDialogue[0].text = "Player healed himself for " + healing.ToString() + " HP";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            graceCooldown = true;
        }
    }

    IEnumerator GraceDriveWaiter(float healing)
    {
        endedMove = false;
        playerAnimator.Play("Heal");
        //graceParticle.SetActive(true);
        audioSource.clip = graceAudio;
        audioSource.Play();

        //Posibility of healing a state
        if (Random.value > 0.05)
        {
            int randomPercentage = Random.Range(0, 100);
            if (randomPercentage <= 33)
            {
                AddCombatText();
                combatDialogue[0].text = "Player healed Grief state";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if (states4[0].name4 == StateType4.GRIEF)
                {
                    states4[0].name4 = StateType4.NULL;
                    states4[0].turnsLeft4 = 0;
                }
                else if (states4[1].name4 == StateType4.GRIEF)
                {
                    states4[1].name4 = StateType4.NULL;
                    states4[1].turnsLeft4 = 0;
                }
                else if (states4[2].name4 == StateType4.GRIEF)
                {
                    states4[2].name4 = StateType4.NULL;
                    states4[2].turnsLeft4 = 0;
                }
            }
            else if (randomPercentage >= 33 && randomPercentage <= 66)
            {
                AddCombatText();
                combatDialogue[0].text = "Player healed Numb state";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if (states4[0].name4 == StateType4.NUMB)
                {
                    states4[0].name4 = StateType4.NULL;
                    states4[0].turnsLeft4 = 0;
                }
                else if (states4[1].name4 == StateType4.NUMB)
                {
                    states4[1].name4 = StateType4.NULL;
                    states4[1].turnsLeft4 = 0;
                }
                else if (states4[2].name4 == StateType4.NUMB)
                {
                    states4[2].name4 = StateType4.NULL;
                    states4[2].turnsLeft4 = 0;
                }
            }
            else
            {
                AddCombatText();
                combatDialogue[0].text = "Player healed Paralisis state";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                if (states4[0].name4 == StateType4.PARALISIS)
                {
                    states4[0].name4 = StateType4.NULL;
                    states4[0].turnsLeft4 = 0;
                }
                else if (states4[1].name4 == StateType4.PARALISIS)
                {
                    states4[1].name4 = StateType4.NULL;
                    states4[1].turnsLeft4 = 0;
                }
                else if (states4[2].name4 == StateType4.PARALISIS)
                {
                    states4[2].name4 = StateType4.NULL;
                    states4[2].turnsLeft4 = 0;
                }
            }
        }

        yield return new WaitForSecondsRealtime(0.5f);
        ShowPopupTextPlayer(healing, Color.green);
        yield return new WaitForSecondsRealtime(1.5f);
        //graceParticle.SetActive(false);
        if (playerScript.moves > 0 && playerScript.energy > 2)
        {
            ShowActions();
        }
        RefreshUI();
        for (float i = healing; i > 0; i--)
        {
            playerScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(2);

        endedMove = true;
    }


    private bool MoveToPosition(Vector3 enemy)
    {
        float animSpeed = 10.0f;
        return enemy != (player.transform.position = Vector3.MoveTowards(player.transform.position, enemy, animSpeed * Time.deltaTime));
    }


    // BOSS ACTIONS
    void Attack()
    {
        HideActions();

        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS || bossstates4[2].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.4)
            {
                AddCombatText();
                combatDialogue[0].text = "The boss is paralized due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
                {
                    AddCombatText();
                    combatDialogue[0].text = "Attack Blocked";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(PlayerBlocked());
                }
                else
                {
                    float damage = (int)Random.Range(((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 30) / 100) - 3), (int)((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                    StartCoroutine(BasicAtackWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
            }
        }
        else
        {
            if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
            {
                AddCombatText();
                combatDialogue[0].text = "Attack Blocked";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                StartCoroutine(PlayerBlocked());
            }
            else
            {
                float damage = (int)Random.Range(((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 30) / 100) - 3), (int)((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 30) / 100) + 3));
                StartCoroutine(BasicAtackWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
    }

    IEnumerator BasicAtackWaiter(float d)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Attack");
        audioSource.clip = attackplusAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(0.8f);
        playerAnimator.Play("HitReaction");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        audioSource.clip = HitStrikeAudio;
        audioSource.Play();
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

        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "The boss failed the attack+ due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
                {
                    AddCombatText();
                    combatDialogue[0].text = "Attack Blocked";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                    StartCoroutine(PlayerBlocked());
                }
                else
                {
                    float damage = (int)Random.Range(((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 80) / 100) - 3), (int)((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 80) / 100) + 3));
                    StartCoroutine(AttackPlusWaiter(damage));
                    AddCombatText();
                    combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
                    combatDialogue[0].color = new Color(1, 1, 1, 1);
                }
            }
        }
        else
        {
            if (playerScript.blockChance >= Random.Range(0, 99))//Blocked attack
            {
                AddCombatText();
                combatDialogue[0].text = "Attack Blocked";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                StartCoroutine(PlayerBlocked());
            }
            else
            {
                float damage = (int)Random.Range(((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 80) / 100) - 3), (int)((((bossScript.stats.strenght * bossScript.strenghtMultiplier * 2) * 80) / 100) + 3));
                StartCoroutine(AttackPlusWaiter(damage));
                AddCombatText();
                combatDialogue[0].text = "Boss dealt " + damage.ToString() + " damage to you";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
    }

    IEnumerator AttackPlusWaiter(float d)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.SetTrigger("Attack+");
        audioSource.clip = attackplusAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(0.8f);
        playerAnimator.Play("HitReaction");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        audioSource.clip = HitStrikeAudio;
        audioSource.Play();
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);
        //frontalPlayerCamera.enabled = !frontalPlayerCamera.enabled;

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

        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "The boss failed the attack due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                float dmg = (int)(((bossScript.stats.power * bossScript.powerMultiplier) * 20) / 100);
                StartCoroutine(EffectAttackWaiter(dmg));
                AddCombatText();
                combatDialogue[0].text = "Boss dealt " + dmg.ToString() + " damage.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            float dmg = (((bossScript.stats.power * bossScript.powerMultiplier) * 20) / 100);
            StartCoroutine(EffectAttackWaiter(dmg));
            AddCombatText();
            combatDialogue[0].text = "Boss dealt " + dmg.ToString() + " damage.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
        //HideActions();
    }

    IEnumerator EffectAttackWaiter(float d)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("EffectAttack");
        audioSource.clip = effectAttackAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(3);
        playerAnimator.Play("HitReaction");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        audioSource.clip = HitStrikeAudio;
        audioSource.Play();
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(2);

        int randomPercentage = Random.Range(0, 100);

        if (randomPercentage <= 33)
        {
            AddCombatText();
            combatDialogue[0].text = "Player is griefed.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(StateEffectFeedback(griefEffect));
            if (states4[0].name4 == StateType4.NULL)
            {
                states4[0].name4 = StateType4.GRIEF;
                states4[0].turnsLeft4 = 3;
            }
            else if (states4[0].name4 == StateType4.NUMB || states4[0].name4 == StateType4.PARALISIS)
            {
                if (states4[1].name4 == StateType4.NULL)
                {
                    states4[1].name4 = StateType4.GRIEF;
                    states4[1].turnsLeft4 = 3;
                }
                else if (states4[1].name4 == StateType4.NUMB || states4[1].name4 == StateType4.PARALISIS)
                {
                    if (states4[2].name4 == StateType4.NULL)
                    {
                        states4[2].name4 = StateType4.GRIEF;
                        states4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (states4[0].name4 == StateType4.GRIEF)
            {
                states4[0].turnsLeft4 += 2;
                if (states4[0].turnsLeft4 > 5)
                {
                    states4[0].turnsLeft4 = 5;
                }
            }
            else if (states4[1].name4 == StateType4.GRIEF)
            {
                states4[1].turnsLeft4 += 2;
                if (states4[1].turnsLeft4 > 5)
                {
                    states4[1].turnsLeft4 = 5;
                }
            }
            else if (states4[2].name4 == StateType4.GRIEF)
            {
                states4[2].turnsLeft4 += 2;
                if (states4[2].turnsLeft4 > 5)
                {
                    states4[2].turnsLeft4 = 5;
                }
            }
        }
        else if (randomPercentage > 33 && randomPercentage <= 66)
        {
            AddCombatText();
            combatDialogue[0].text = "Player is numb.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(StateEffectFeedback(numbEffect));
            if (states4[0].name4 == StateType4.NULL)
            {
                states4[0].name4 = StateType4.NUMB;
                states4[0].turnsLeft4 = 3;
            }
            else if (states4[0].name4 == StateType4.GRIEF || states4[0].name4 == StateType4.PARALISIS)
            {
                if (states4[1].name4 == StateType4.NULL)
                {
                    states4[1].name4 = StateType4.NUMB;
                    states4[1].turnsLeft4 = 3;
                }
                else if (states4[1].name4 == StateType4.GRIEF || states4[1].name4 == StateType4.PARALISIS)
                {
                    if (states4[2].name4 == StateType4.NULL)
                    {
                        states4[2].name4 = StateType4.NUMB;
                        states4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (states4[0].name4 == StateType4.NUMB)
            {
                states4[0].turnsLeft4 += 2;
                if (states4[0].turnsLeft4 > 5)
                {
                    states4[0].turnsLeft4 = 5;
                }
            }
            else if (states4[1].name4 == StateType4.NUMB)
            {
                states4[1].turnsLeft4 += 2;
                if (states4[1].turnsLeft4 > 5)
                {
                    states4[1].turnsLeft4 = 5;
                }
            }
            else if (states4[2].name4 == StateType4.NUMB)
            {
                states4[2].turnsLeft4 += 2;
                if (states4[2].turnsLeft4 > 5)
                {
                    states4[2].turnsLeft4 = 5;
                }
            }
        }
        else
        {
            AddCombatText();
            combatDialogue[0].text = "Player is paralized.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            StartCoroutine(StateEffectFeedback(paralizeEffect));
            if (states4[0].name4 == StateType4.NULL)
            {
                states4[0].name4 = StateType4.PARALISIS;
                states4[0].turnsLeft4 = 3;
            }
            else if (states4[0].name4 == StateType4.NUMB || states4[0].name4 == StateType4.GRIEF)
            {
                if (states4[1].name4 == StateType4.NULL)
                {
                    states4[1].name4 = StateType4.PARALISIS;
                    states4[1].turnsLeft4 = 3;
                }
                else if (states4[1].name4 == StateType4.NUMB || states4[1].name4 == StateType4.GRIEF)
                {
                    if (states4[2].name4 == StateType4.NULL)
                    {
                        states4[2].name4 = StateType4.PARALISIS;
                        states4[2].turnsLeft4 = 3;
                    }
                }
            }
            else if (states4[0].name4 == StateType4.PARALISIS)
            {
                states4[0].turnsLeft4 += 2;
                if (states4[0].turnsLeft4 > 5)
                {
                    states4[0].turnsLeft4 = 5;
                }
            }
            else if (states4[1].name4 == StateType4.PARALISIS)
            {
                states4[1].turnsLeft4 += 2;
                if (states4[1].turnsLeft4 > 5)
                {
                    states4[1].turnsLeft4 = 5;
                }
            }
            else if (states4[2].name4 == StateType4.PARALISIS)
            {
                states4[2].turnsLeft4 += 2;
                if (states4[2].turnsLeft4 > 5)
                {
                    states4[2].turnsLeft4 = 5;
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
        StartCoroutine(ChargeWaiter());
        AddCombatText();
        combatDialogue[0].text = "Boss charged his Special Attack.";
        combatDialogue[0].color = new Color(1, 1, 1, 1);
    }

    IEnumerator ChargeWaiter()
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Charge");
        audioSource.clip = chargeAudio;
        audioSource.Play();
        GameObject particle = Instantiate(chargeParticlesHolder);
        chargeParticles.Play();
        yield return new WaitForSecondsRealtime(3);
        chargeParticles.Stop();
        Destroy(particle);
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
        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS || bossstates4[2].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed special attack due to paralisis";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                HideActions();
                float dmg = (int)bossScript.stats.power * (int)bossScript.powerMultiplier;
                if (bossScript.stats.charge == true)
                {
                    StartCoroutine(SpecialAttackWaiter(dmg));
                }
            }
        }
        else
        {
            HideActions();
            float dmg = (int)bossScript.stats.power * (int)bossScript.powerMultiplier;
            if (bossScript.stats.charge == true)
            {
                StartCoroutine(SpecialAttackWaiter(dmg));
            }
        }
    }

    IEnumerator SpecialAttackWaiter(float d)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Special");
        audioSource.clip = specialAttackAudio;
        audioSource.Play();
        GameObject particle = Instantiate(specialParticlesHolder);
        specialParticles.Play();
        yield return new WaitForSecondsRealtime(1);
        playerAnimator.Play("HitReaction");
        StartCoroutine(CameraShake(vCamNoise, shakeAmplitudeLight, shakeFrequencyLight));
        audioSource.clip = HitStrikeAudio;
        audioSource.Play();
        ShowPopupTextPlayer(d, Color.red);
        yield return new WaitForSecondsRealtime(4);
        specialParticles.Stop();
        Destroy(particle);

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
        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed guard due to paralisis";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                float armor = (int)bossScript.stats.endurance * (int)bossScript.enduranceMultiplier;
                StartCoroutine(GuardBossWaiter(armor));
                AddCombatText();
                combatDialogue[0].text = "Boss has " + armor.ToString() + " armor now.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            float armor = (int)bossScript.stats.endurance * (int)bossScript.enduranceMultiplier;
            StartCoroutine(GuardBossWaiter(armor));
            AddCombatText();
            combatDialogue[0].text = "Boss has " + armor.ToString() + " armor now.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator GuardBossWaiter(float h)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Guard");
        audioSource.clip = guardAudio;
        audioSource.Play();
        GameObject particles = Instantiate(guardParticlesHolder);
        guardParticles.Play();
        for (float i = h; i > 0; i--)
        {
            bossScript.armor++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSecondsRealtime(2);
        guardParticles.Stop();
        Destroy(particles);

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void Heal()
    {
        HideActions();
        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[0].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed to heal due to paralisis";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else if (bossstates4[0].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.NUMB || bossstates4[2].name4 == StateType4.NUMB)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed heal because is numb";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                float heal = (int)bossScript.stats.vigor * (int)bossScript.vigorMultiplier;
                StartCoroutine(HealWaiter(heal));
                AddCombatText();
                combatDialogue[0].text = "Boss healed for " + heal.ToString() + " HP";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
            }
        }
        else if (bossstates4[0].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.NUMB || bossstates4[2].name4 == StateType4.NUMB)
        {
            AddCombatText();
            combatDialogue[0].text = "Boss failed heal because is numb";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            ShowActions();
        }
        else
        {
            float heal = (int)bossScript.stats.vigor * (int)bossScript.vigorMultiplier;
            StartCoroutine(HealWaiter(heal));
            AddCombatText();
            combatDialogue[0].text = "Boss healed for " + heal.ToString() + " HP";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator HealWaiter(float h)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Heal");
        audioSource.clip = bossHealAudio;
        audioSource.Play();
        healParticlesBoss.SetActive(true);
        ShowPopupText(h, Color.green);
        for (float i = h; i > 0; i--)
        {
            bossScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        healParticlesBoss.SetActive(false);
        yield return new WaitForSecondsRealtime(1);

        bossEndedMove = true;
        ShowActions();
        RefreshUI();
    }

    void HealPlus()
    {
        HideActions();
        if (bossstates4[0].name4 == StateType4.PARALISIS || bossstates4[1].name4 == StateType4.PARALISIS || bossstates4[2].name4 == StateType4.PARALISIS)
        {
            if (Random.value > 0.25)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed heal+ due to paralisis.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else if (bossstates4[0].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.NUMB || bossstates4[2].name4 == StateType4.NUMB)
            {
                AddCombatText();
                combatDialogue[0].text = "Boss failed heal+ because is numb.";
                combatDialogue[0].color = new Color(1, 1, 1, 1);
                ShowActions();
            }
            else
            {
                float heal = (int)(bossScript.stats.vigor * bossScript.vigorMultiplier) * 2;
                StartCoroutine(HealPlusWaiter(heal));
            }
        }
        else if (bossstates4[0].name4 == StateType4.NUMB || bossstates4[1].name4 == StateType4.NUMB || bossstates4[2].name4 == StateType4.NUMB)
        {
            AddCombatText();
            combatDialogue[0].text = "Boss failed heal+ because is numb.";
            combatDialogue[0].color = new Color(1, 1, 1, 1);
            ShowActions();
        }
        else
        {
            float heal = (int)(bossScript.stats.vigor * bossScript.vigorMultiplier) * 2;
            StartCoroutine(HealPlusWaiter(heal));
        }
    }

    IEnumerator HealPlusWaiter(float h)
    {
        yield return new WaitForSecondsRealtime(1.8f);
        bossEndedMove = false;
        bossAnimator.Play("Heal+");
        audioSource.clip = bossHealAudio;
        audioSource.Play();
        healParticlesBoss.SetActive(true);
        ShowPopupText(h, Color.green);
        for (float i = h; i > 0; i--)
        {
            bossScript.health++;
            RefreshUI();
            yield return 0;
            yield return new WaitForSeconds(0);
        }
        healParticlesBoss.SetActive(false);
        yield return new WaitForSecondsRealtime(1);

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

    //CAMERA SHAKE
    IEnumerator CameraShake(CinemachineBasicMultiChannelPerlin noiseChannel, float shakeAmplitude, float shakeFrequency)
    {
        noiseChannel.m_AmplitudeGain = shakeAmplitude; //set shaking values
        noiseChannel.m_FrequencyGain = shakeFrequency;
        yield return new WaitForSecondsRealtime(0.4f);
        noiseChannel.m_AmplitudeGain = 0;
        noiseChannel.m_FrequencyGain = 0;
    }

    //CORUTINAS PARA CONTROLAR LAS CINEMATICAS
    IEnumerator AuraWaiterCinem()
    {
        yield return new WaitForSecondsRealtime(2);
        teleportAura.SetActive(true);
        yield return new WaitForSecondsRealtime(8.3f);
        teleportAura.SetActive(false);
    }

    IEnumerator OrbWaiterCinem()
    {
        yield return new WaitForSecondsRealtime(2);
        teleportOrb.SetActive(true);
        audioSource.clip = teleportAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(8.5f);
        teleportOrb.SetActive(false);
    }

    IEnumerator PlayerWaiterCinem()
    {
        yield return new WaitForSecondsRealtime(7);
        player.SetActive(true);
    }

    IEnumerator BossWaiterCinem()
    {
        yield return new WaitForSecondsRealtime(10f);
        CM_vcam1.SetActive(false);
        CM_vcam2.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        bossAnimator.SetTrigger("Cinematic");
        audioSource.clip = introAudio;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(7f);
        CM_vcam1.SetActive(true);
        CM_vcam2.SetActive(false);
        ShowActions();
    }

    IEnumerator StateEffectFeedback(ParticleSystem particle)
    {
        Instantiate(particle);
        particle.Play();
        yield return new WaitForSecondsRealtime(1);
        particle.Stop();
    }

    IEnumerator WinScene()
    {
        bossAnimator.Play("Die");
        yield return new WaitForSecondsRealtime(5);
        winCanvas.SetActive(true);
        playerScript.coins += 500;
        orbs.quantity += 20;
        PlayerPrefs.SetInt("COINS", playerScript.coins);
        PlayerPrefs.SetInt("ORBS", orbs.quantity);
        orbsText.text = "20 orbs";
        moneyText.text = "500 coins";
        continueButton.onClick.AddListener(JumpScene);
    }

    void JumpScene()
    {
        SceneManager.LoadScene("Narrator", LoadSceneMode.Single); //Change scene if continue button is pressed
    }
}
