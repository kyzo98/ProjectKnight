using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneSeller : MonoBehaviour {
    public string NPCName;                                                                  //Real name of NPC
    [TextArea(3, 10)]
    public string[] NPCSentences;                                                           //Dialogue sentences of NPC
    public AudioClip[] NPCSentencesAudio;                                                   //Audio sentences of NPC

    public Material defaultMaterial;                                                        //Material predeterminado
    public Material activeMaterial;                                                         //Material cuando esta activo
    public RawImage pressEImage;                                                            //Imagen press E
    public GameObject dialogue;                                                             //GameObject contenedor del dialogo
    public GameObject storeWrap;                                                            //GameObject contenedor de la tienda
    public GameObject dialogueOptionsWrap;                                                  //GameObject contenedor de las opciones de dialogo
    public GameObject dialogueWaiter;                                                       //GameObject contenedor del dialogueWaiter

    public int optionSelected;                                                              //Opción selecionada por el jugador
    public Button buyButton;                                                                //Boton de comprar
        public Sprite buyButtonDefault;                                                         //Sprite por defecto
        public Sprite buyButtonHover;                                                            //Sprite activo
    public Button exitButton;                                                               //Boton de exit
        public Sprite exitButtonDefault;                                                        //Sprite por defecto
        public Sprite exitButtonHover;                                                          //Sprite activo

    private Renderer render;                                                                //Render de materiales del NPC
    private PlayerController playerController;                                              //Script de control del Player
    private AudioSource audioSource;                                                        //AudioSource voz del NPC
    private bool isTriggered;                                                               //Trigger de activación por proximidad
    private bool endCorutines;                                                              //Descativador de corrutinas
    private float letterPause;                                                              //Pausa entre letras

    private enum DialogueState{ NULL, INIT, WELCOME, BUY, BYE, END};                        //Posibles estados de dialogo con este NPC
    private DialogueState dialogueState;                                                    //Variable que guarda el estado del dialogo en el que estamos
    private float dialogueTimeLeft;                                                         //Tiempo que duran las cadenas de dialogo
    private Text dialogueText;                                                              //Texto a mostrar

    void Start () {
        render = GetComponent<Renderer>();                                                  //Guardamos componente Renderer
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();      //Guardamos script PlayerController
        audioSource = GetComponent<AudioSource>();                                          //Guardamos componente AudioSource
        dialogueText = dialogue.GetComponent<Text>();                                       //Guardamos componente Text

        dialogueText.text = "";                                                             //Inicializamos el texto vacío
        dialogue.SetActive(false);                                                          //Inicializamos el cuadro de dialogo no visible
        pressEImage.enabled = false;                                                        //Inicializamos el pressEImage no visible
        isTriggered = false;                                                                //Inicializamos el trigger de proximidad desactivado
        endCorutines = false;                                                               //Inicializamos el cortador de corrutinas en falso
        dialogueState = DialogueState.NULL;                                                 //Estado de la conversación inicial en NULL(sin conversación)
        letterPause = .0f;                                                                  //Iniciamos el letterPause a 0s

        buyButton.onClick.AddListener(buyOption);                                           //Asignamos la funcion buyOption al buyButton
        exitButton.onClick.AddListener(exitOption);                                         //Asignamos la funcion exitOption al exitButton
    }
	
	void Update () {
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
                playerController.enabled = false; //Ya no puedes mover el personaje
                pressEImage.enabled = false; //Deja de aparecer el boton E
                dialogueText.text = ""; //Aparece el cuadro de diálogo vacío
                dialogue.SetActive(true); //Activamos el texto
                dialogueTimeLeft = NPCSentencesAudio[0].length; //Preparamos la duración del siguiente audio
                dialogueState = DialogueState.WELCOME;
                optionSelected = 0;
                break;
            case DialogueState.WELCOME:
                if (dialogueTimeLeft == NPCSentencesAudio[0].length)//Inicia la animacion
                {
                    audioSource.clip = NPCSentencesAudio[0];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[0])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    dialogueOptionsWrap.SetActive(true);
                    dialogueWaiter.SetActive(true);

                    //Función elección
                    if (Input.GetButtonDown("D") && optionSelected == 0)
                    {
                        exitOptionSelected();
                    }
                    if (Input.GetButtonDown("A") && optionSelected == 1)
                    {
                        buyOptionSelected();
                    }

                    if (Input.GetButtonDown("E"))
                    {
                        dialogueWaiter.SetActive(false);
                        if (optionSelected == 0)
                            buyOption();
                        else
                        {
                            exitOption();
                            buyButton.image.sprite = buyButtonHover;
                            exitButton.image.sprite = exitButtonDefault;
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
                        dialogueText.text = NPCSentences[0];
                        dialogueTimeLeft = 0;
                    }
                }
                break;
            case DialogueState.BUY:
                if (dialogueTimeLeft == NPCSentencesAudio[2].length)//Inicia la animacion
                {
                    audioSource.clip = NPCSentencesAudio[2];
                    audioSource.Play(); //Ejecutamos el audio
                    StartCoroutine(TypeText(NPCSentences[2])); //Escribimos el texto
                }
                dialogueTimeLeft -= Time.deltaTime; //Restando el tiempo
                if (dialogueTimeLeft <= 0) //Comprovando que haya acabado la frase y que el jugador quiere avanzar
                {
                    storeWrap.SetActive(true);
                    dialogueWaiter.SetActive(true);
                    if (Input.GetButtonDown("E"))
                    {
                        storeWrap.SetActive(false);
                        dialogueWaiter.SetActive(false);
                        dialogueText.text = ""; //Reseteamos el texto
                        dialogueTimeLeft = NPCSentencesAudio[1].length; //Preparamos la duración del siguiente audio
                        endCorutines = false;
                        dialogueState = DialogueState.BYE;
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
            case DialogueState.BYE:
                if (dialogueTimeLeft == NPCSentencesAudio[1].length)//Inicia la animacion
                {
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
                        dialogueText.text = NPCSentences[1];
                        dialogueTimeLeft = 0;
                    }
                }
                break;
            case DialogueState.END:
                buyButton.image.sprite = buyButtonHover;
                exitButton.image.sprite = exitButtonDefault;
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
        void buyOption()
        {
            dialogueText.text = ""; //Reseteamos el texto
            dialogueOptionsWrap.SetActive(false);
            dialogueTimeLeft = NPCSentencesAudio[2].length; //Preparamos la duración del siguiente audio
            endCorutines = false;
            dialogueState = DialogueState.BUY;
        }

        void exitOption()
        {
            dialogueText.text = ""; //Reseteamos el texto
            dialogueOptionsWrap.SetActive(false);
            dialogueTimeLeft = NPCSentencesAudio[1].length; //Preparamos la duración del siguiente audio
            endCorutines = false;
            dialogueState = DialogueState.BYE;
        }

        public void buyOptionSelected()
        {
            optionSelected = 0; 
            buyButton.image.sprite = buyButtonHover;
            exitButton.image.sprite = exitButtonDefault;
            Debug.Log(optionSelected);
        }

        public void exitOptionSelected()
        {
            optionSelected = 1;
            buyButton.image.sprite = buyButtonDefault;
            exitButton.image.sprite = exitButtonHover;
            Debug.Log(optionSelected);
        }
}
