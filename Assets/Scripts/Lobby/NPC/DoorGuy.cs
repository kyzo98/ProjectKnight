using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorGuy : MonoBehaviour
{
    public string NPCName;                                                                  //Real name of NPC
    [TextArea(3, 10)]
    public string[] NPCSentences;                                                           //Dialogue sentences of NPC
    public AudioClip[] NPCSentencesAudio;                                                   //Audio sentences of NPC

    public Material defaultMaterial;                                                        //Material predeterminado
    public Material activeMaterial;                                                         //Material cuando esta activo
    public RawImage pressEImage;                                                            //Imagen press E
    public GameObject dialogue;                                                             //GameObject contenedor del dialogo                                                        
    public GameObject dialogueOptionsWrap;                                                  //GameObject contenedor de las opciones de dialogo
    public GameObject dialogueWaiter;                                                       //GameObject contenedor del dialogueWaiter
    public GameObject NPCModel;
    Animator animator;

    public int optionSelected;                                                              //Opción selecionada por el jugador
    public Button yesButton;                                                                //Boton de comprar
    public Sprite yesButtonDefault;                                                         //Sprite por defecto
    public Sprite yesButtonHover;                                                            //Sprite activo
    public Button noButton;                                                               //Boton de exit
    public Sprite noButtonDefault;                                                        //Sprite por defecto
    public Sprite noButtonHover;                                                          //Sprite activo

    private Renderer render;                                                                //Render de materiales del NPC
    private PlayerController playerController;                                              //Script de control del Player
    private AudioSource audioSource;                                                        //AudioSource voz del NPC
    private bool isTriggered;                                                               //Trigger de activación por proximidad
    private bool endCorutines;                                                              //Descativador de corrutinas
    private float letterPause;                                                              //Pausa entre letras

    private enum DialogueState { NULL, INIT, WELCOME1, WELCOME2, WELCOME3, GO_BATTLE,YES, NO, END };                        //Posibles estados de dialogo con este NPC
    private bool welcome; 
    private DialogueState dialogueState;                                                    //Variable que guarda el estado del dialogo en el que estamos
    private float dialogueTimeLeft;                                                         //Tiempo que duran las cadenas de dialogo
    private Text dialogueText;                                                              //Texto a mostrar

    private bool defeatedBoss1;
    private int fightSceneOrder;

    void Start()
    {
        fightSceneOrder = PlayerPrefs.GetInt("FIGHT_ORDER");

        render = GetComponent<Renderer>();                                                  //Guardamos componente Renderer
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();      //Guardamos script PlayerController
        audioSource = GetComponent<AudioSource>();                                          //Guardamos componente AudioSource
        dialogueText = dialogue.GetComponent<Text>();                                       //Guardamos componente Text
        animator = NPCModel.GetComponent<Animator>();

        dialogueText.text = "";                                                             //Inicializamos el texto vacío
        dialogue.SetActive(false);                                                          //Inicializamos el cuadro de dialogo no visible
        pressEImage.enabled = false;                                                        //Inicializamos el pressEImage no visible
        isTriggered = false;                                                                //Inicializamos el trigger de proximidad desactivado
        endCorutines = false;                                                               //Inicializamos el cortador de corrutinas en falso
        dialogueState = DialogueState.NULL;                                                 //Estado de la conversación inicial en NULL(sin conversación)
        letterPause = .0f;                                                                  //Iniciamos el letterPause a 0s
        welcome = false; 

        yesButton.onClick.AddListener(noOption);                                           //Asignamos la funcion buyOption al buyButton
        noButton.onClick.AddListener(yesOption);                                         //Asignamos la funcion exitOption al exitButton
    }

    void Update()
    {
        if (isTriggered)                                                                    //Si el jugador entra en el area
        {
            if (Input.GetButtonDown("E") && dialogueState == DialogueState.NULL)            //Si el jugador pulsa "E" y no ha empezado una conversación
            {
                dialogueState = DialogueState.INIT;                                         //Iniciamos conversación
            }
        }

        if (dialogueState != DialogueState.NULL)                                            //Si ha iniciado conversación
        {
            sellerTalk();                                                                   //Función conversación
        }
    }

    //Función iniciar diálogo vendedor
    void sellerTalk()
    {
        switch (dialogueState)
        {
            case DialogueState.NULL:
                Debug.Log("No estoy Conversando");
                break;
            case DialogueState.INIT:
                playerController.anim.SetFloat("Speed", 0);
                playerController.enabled = false; //Ya no puedes mover el personaje
                pressEImage.enabled = false; //Deja de aparecer el boton E
                dialogueText.text = ""; //Aparece el cuadro de diálogo vacío
                dialogue.SetActive(true); //Activamos el texto
                if (!welcome)
                {
                    dialogueTimeLeft = NPCSentencesAudio[0].length; //Preparamos la duración del siguiente audio
                    dialogueState = DialogueState.WELCOME1;
                }
                else
                {
                    dialogueTimeLeft = NPCSentencesAudio[3].length; //Preparamos la duración del siguiente audio
                    dialogueState = DialogueState.GO_BATTLE; 
                }
                optionSelected = 0;
                break;
            case DialogueState.WELCOME1:
                if (dialogueTimeLeft == NPCSentencesAudio[0].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[0];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[0])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    
                    dialogueWaiter.SetActive(true);

                   
                   

                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        dialogueText.text = ""; //Reseteamos el texto
                        dialogueTimeLeft = NPCSentencesAudio[1].length; //Preparamos la duración del siguiente audio
                        endCorutines = false;
                        dialogueState = DialogueState.WELCOME2; 
                    }
                }
                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[0];
                        dialogueTimeLeft = 0;
                    }
                }
                break;
            case DialogueState.WELCOME2:
                if (dialogueTimeLeft == NPCSentencesAudio[1].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[1];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[1])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {

                    dialogueWaiter.SetActive(true);




                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        dialogueText.text = ""; //Reseteamos el texto
                        dialogueTimeLeft = NPCSentencesAudio[2].length; //Preparamos la duración del siguiente audio
                        endCorutines = false;
                        dialogueState = DialogueState.WELCOME3;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[1];
                        dialogueTimeLeft = 0;
                    }
                }
                break;


            case DialogueState.WELCOME3:
                if (dialogueTimeLeft == NPCSentencesAudio[2].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[2];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[2])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {

                    dialogueWaiter.SetActive(true);




                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        welcome = true; 
                        //to do . Intro guy se mueve a la entrada del boss
                        dialogueState = DialogueState.END;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[2];
                        dialogueTimeLeft = 0;
                    }
                }
                break;
            case DialogueState.GO_BATTLE:
                if (dialogueTimeLeft == NPCSentencesAudio[3].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[3];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[3])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    dialogueOptionsWrap.SetActive(true);
                    dialogueWaiter.SetActive(true);

                    //Función elección
                    if (Input.GetButtonDown("D") && optionSelected == 0)
                    {
                        noOptionSelected();
                    }
                    if (Input.GetButtonDown("A") && optionSelected == 1)
                    {
                        yesOptionSelected();
                    }

                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        if (optionSelected == 0)
                            noOption();
                        else
                        {
                            yesOption();
                            yesButton.image.sprite = yesButtonHover;
                            noButton.image.sprite = noButtonDefault;
                        }
                    }

                }

                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[3];
                        dialogueTimeLeft = 0;
                    }
                }
                    break;
            case DialogueState.YES:
                if (dialogueTimeLeft == NPCSentencesAudio[4].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[4];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[4])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    dialogueWaiter.SetActive(true);
                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        dialogueText.text = ""; //Reseteamos el texto
                        endCorutines = false;
                        dialogueState = DialogueState.END;
                        switch (fightSceneOrder)
                        {
                            case 0:
                                SceneManager.LoadScene("Fight", LoadSceneMode.Single);
                                fightSceneOrder++;
                                PlayerPrefs.SetInt("FIGHT_ORDER", fightSceneOrder);
                                break;
                            case 1:
                                SceneManager.LoadScene("Fight4", LoadSceneMode.Single);
                                fightSceneOrder++;
                                PlayerPrefs.SetInt("FIGHT_ORDER", fightSceneOrder);
                                break;
                            case 3:
                                SceneManager.LoadScene("Fight3", LoadSceneMode.Single);
                                fightSceneOrder++;
                                PlayerPrefs.SetInt("FIGHT_ORDER", fightSceneOrder);
                                break;
                            case 4:
                                SceneManager.LoadScene("Fight2", LoadSceneMode.Single);
                                fightSceneOrder++;
                                PlayerPrefs.SetInt("FIGHT_ORDER", fightSceneOrder);
                                break;
                        }
                    }
                }
                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[4];
                        dialogueTimeLeft = 0;
                    }
                }
                break;

            case DialogueState.NO:
                if (dialogueTimeLeft == NPCSentencesAudio[5].length)//Inicia la animacion
                {
                    animator.Play("Talk");
                    audioSource.clip = NPCSentencesAudio[5];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[5])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    dialogueWaiter.SetActive(true);
                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        dialogueText.text = ""; //Reseteamos el texto
                        endCorutines = false;
                        dialogueState = DialogueState.END;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("E"))
                    {
                        audioSource.Stop();
                        endCorutines = true;
                        dialogueText.text = "";
                        dialogueText.text = NPCSentences[5];
                        dialogueTimeLeft = 0;
                    }
                }
                break;


            case DialogueState.END:
                render.material = activeMaterial; //Cambiamos de material
                pressEImage.enabled = true; //Mostramos la imágen en pantalla
                dialogue.SetActive(false); //Desactivamos el texto
                playerController.enabled = true; //Volvemos a darle al jugador el movimiento
                dialogueState = DialogueState.NULL;
                break;
            default:
                break;
        }
    }

    //Detección de collider del NPC con el del player
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")                                           //Comprueba colisicón con el Player
        {
            isTriggered = true;                                                             //Cambiamos el isTrigered a true
            render.material = activeMaterial;                                               //Cambiamos de material
            pressEImage.enabled = true;                                                     //Mostramos la imágen en pantalla
        }
    }

    //Detección de salida de collider del NPC con el del player
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Player")                                           //Comprueba colisicón con el Player
        {
            isTriggered = false;                                                            //Cambiamos el isTrigered a true
            render.material = defaultMaterial;                                              //Cambiamos de material
            pressEImage.enabled = false;                                                    //Dejamos de mostrar la imágen
        }
    }

    //Corutina de espera
    IEnumerator Waiter()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
    }

    //Animación texto
    IEnumerator TypeText(string message)
    {
        foreach (char letter in message.ToCharArray())
        {
            letterPause = .0f;
            dialogueText.text += letter;

            yield return 0;
            yield return new WaitForSeconds(letterPause);
            if (endCorutines)
            {
                yield break;
            }
        }
    }

    //Funciones de opciones de diálogo
    void yesOption()
    {
        dialogueText.text = ""; //Reseteamos el texto
        dialogueOptionsWrap.SetActive(false);
        dialogueTimeLeft = NPCSentencesAudio[4].length; //Preparamos la duración del siguiente audio
        endCorutines = false;
        dialogueState = DialogueState.YES;
    }

    void noOption()
    {
        dialogueText.text = ""; //Reseteamos el texto
        dialogueOptionsWrap.SetActive(false);
        dialogueTimeLeft = NPCSentencesAudio[5].length; //Preparamos la duración del siguiente audio
        endCorutines = false;
        dialogueState = DialogueState.NO;
    }

    public void yesOptionSelected()
    {
        optionSelected = 0;
        yesButton.image.sprite = yesButtonHover;
        noButton.image.sprite = noButtonDefault;
        Debug.Log(optionSelected);
    }

    public void noOptionSelected()
    {
        optionSelected = 1;
        yesButton.image.sprite = yesButtonDefault;
        noButton.image.sprite = noButtonHover;
        Debug.Log(optionSelected);
    }
}
