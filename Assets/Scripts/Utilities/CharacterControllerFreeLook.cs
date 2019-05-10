using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerFreeLook : MonoBehaviour {

    public float inputX;
    public float inputZ;
    public Vector3 moveDirection;
    public float rotationSpeed = 0.1f;
    public float speed;

    private CharacterController characterController;
    private Animator anim;
    private Camera mainCamera;

    public GameObject pauseMenu;
    public GameObject characterMenu;
    public GameObject inventoryMenu;
    public GameObject menusMenu;

    // Use this for initialization
    void Start () {
        characterController = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if (Input.GetKeyDown("c"))
        {
            if (!menusMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
                menusMenu.SetActive(true);
                characterMenu.SetActive(true);
            }
            else
            {
                if (characterMenu.activeSelf)
                {
                    inventoryMenu.SetActive(false);
                    menusMenu.SetActive(false);
                    characterMenu.SetActive(false);
                }
                else
                {
                    inventoryMenu.SetActive(false);
                    characterMenu.SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown("i"))
        {
            if (!menusMenu.activeSelf)
            {
                characterMenu.SetActive(false);
                menusMenu.SetActive(true);
                inventoryMenu.SetActive(true);
            }
            else
            {
                if (inventoryMenu.activeSelf)
                {
                    characterMenu.SetActive(false);
                    menusMenu.SetActive(false);
                    inventoryMenu.SetActive(false);
                }
                else
                {
                    characterMenu.SetActive(false);
                    inventoryMenu.SetActive(true);
                }
            }
        }

        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        moveDirection = mainCamera.transform.forward * inputZ + mainCamera.transform.right * inputX;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
	}
}
