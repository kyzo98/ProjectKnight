using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {
    public Camera mainCamera;                                           // Camera that follows the player in the lobby
    public bool xMovementInverted;                                      // Bool to on/off inverted movement in x axis
    public bool zMovementInverted;                                      // Bool to on/off inverted movement in z axis

    private float speed;                                                // Variable that controls the speed of movement of the character
    private Vector3 offset;                                             // Distance between the camera and the character
    private Transform mainCameraTransform;                              // Variable that saves the main camera transform
    private int X_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it
    private int Z_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it

    Animator animation;                                                 // It saves the animator.

    void Start () {
        animation = GetComponent<Animator>();                           // Gets the animator from the actual character.

        transform.position = new Vector3(0, 0.48f, 0);                      // Positions in which the character will be spawned
        transform.eulerAngles = new Vector3(0, 90, 0);                   // Orientation of character spawn
          
        mainCameraTransform = mainCamera.GetComponent<Transform>();     // Gets the main camera transform information
        mainCameraTransform.position    = new Vector3(-5, 3, 0);        // Position where the camera is set at start
        mainCameraTransform.eulerAngles = new Vector3(30, 90, 0);       // Orientation of camera spawn

        offset = mainCamera.transform.position - transform.position;    // Measures the distance between the character and the camera

        if (xMovementInverted) X_MOVEMENT = -1;                         // Sets the numbers to inverted or not movement in x axis
        else X_MOVEMENT = 1;

        if (zMovementInverted) Z_MOVEMENT =  -1;                         // Sets the numbers to inverted or not movement in x axis
        else Z_MOVEMENT = 1;

        speed = 5.0f;                                                   // Sets the speed number
    }
	
	void Update () {
        float xMove = Input.GetAxis("Horizontal")   * speed * X_MOVEMENT; // Gets the input of the vertical axis and applies speed 
        float zMove = Input.GetAxis("Vertical") * speed * Z_MOVEMENT; // Gets the input of the horizontal axis and applies speed 

        xMove *= Time.deltaTime;                                        // Makes the movement based on time and not in frames per second
        zMove *= Time.deltaTime;                                        // Makes the movement based on time and not in frames per second

        if(xMove > 0 || xMove < 0 || zMove > 0 || zMove < 0)            // Sets a an animation parameter based on how it is moving
        {
            animation.SetFloat("Speed", 1);                        
        }
        else if(xMove == 0 || zMove == 0)
        {
            animation.SetFloat("Speed", 0);
        }

        //if (xMove < 0)                                                   // Changes the rotation of the character based on the direction it is moving
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        //else if (xMove > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}
        //else if (zMove < 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 270, 0);
        //}
        //else if (zMove > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 90, 0);
        //}

        transform.Translate(xMove, 0, zMove);                           // Transforms the player position
        mainCamera.transform.position = transform.position + offset;    // Transforms the camera position
	}
}
