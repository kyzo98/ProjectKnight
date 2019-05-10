using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Camera mainCamera;                                           // Camera that follows the player in the lobby
    public bool xMovementInverted;                                      // Bool to on/off inverted movement in x axis
    public bool zMovementInverted;                                      // Bool to on/off inverted movement in z axis

    private float speed;                                                // Variable that controls the speed of movement of the character
    private Vector3 offset;                                             // Distance between the camera and the character
    private Transform mainCameraTransform;                              // Variable that saves the main camera transform
    private int X_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it
    private int Z_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it
    private Rigidbody rigidbody;

    public Animator anim;                                                      // It saves the animator.

    public GameObject pauseMenu;
    public GameObject characterMenu;
    public GameObject inventoryMenu;
    public GameObject menusMenu;

    void Start () {
        anim = this.GetComponent<Animator>();                                // Gets the animator from the actual character.
        rigidbody = this.GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        transform.position = new Vector3(4.89f, 0.002f, 4.56f);                      // Positions in which the character will be spawned
        transform.eulerAngles = new Vector3(0, 0, 0);                   // Orientation of character spawn

        offset = mainCamera.transform.position - transform.position;    // Measures the distance between the character and the camera

        if (xMovementInverted) X_MOVEMENT = -1;                         // Sets the numbers to inverted or not movement in x axis
        else X_MOVEMENT = 1;

        if (zMovementInverted) Z_MOVEMENT =  -1;                         // Sets the numbers to inverted or not movement in x axis
        else Z_MOVEMENT = 1;

        speed = 2.0f;                                                   // Sets the speed number
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

        float xMove = Input.GetAxis("Horizontal") * X_MOVEMENT;                         // Gets the input of the vertical axis and applies speed 
        float zMove = Input.GetAxis("Vertical") * Z_MOVEMENT;                           // Gets the input of the horizontal axis and applies speed 

        if (xMove > 0 || xMove < 0 || zMove > 0 || zMove < 0)            // Sets a an animation parameter based on how the player is moving
        {
            anim.SetFloat("Speed", 1);
        }
        else if (xMove == 0 || zMove == 0)
        {
            anim.SetFloat("Speed", 0);
        }

        Vector3 orientation = mainCamera.transform.forward * zMove + mainCamera.transform.right * xMove;                                               // Saves the orientation of the player based in where the camera is looking
        transform.rotation = Quaternion.LookRotation(orientation);                                        // Changes the orientation of the player based in his movement direction
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(orientation), 0.15f);  // Makes the orientation change more smooth.

        rigidbody.velocity = orientation * speed;                                                           //Moves the player. Changed it because the transform.translate doesn't care about all the collisions

        //We don't need to update camera position now, is a free look cinemachine now and using it's follow and look at components is all we need to update it.
	}
}
