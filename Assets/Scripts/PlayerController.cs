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

    public Animator anim;                                                      // It saves the animator.

    void Start () {
        anim = GetComponent<Animator>();                                // Gets the animator from the actual character.

        transform.position = new Vector3(0, 0.48f, 0);                      // Positions in which the character will be spawned
        transform.eulerAngles = new Vector3(0, 90, 0);                   // Orientation of character spawn
          
        mainCameraTransform = mainCamera.GetComponent<Transform>();     // Gets the main camera transform information
        mainCameraTransform.position    = new Vector3(-5, 3, 0);        // Position where the camera is set at start
        mainCameraTransform.eulerAngles = new Vector3(30, 90, 0);       // Orientation of camera spawn

        offset = mainCamera.transform.position - transform.position;    // Measures the distance between the character and the camera

        if (xMovementInverted) X_MOVEMENT = 1;                         // Sets the numbers to inverted or not movement in x axis
        else X_MOVEMENT = -1;

        if (zMovementInverted) Z_MOVEMENT =  -1;                         // Sets the numbers to inverted or not movement in x axis
        else Z_MOVEMENT = 1;

        speed = 2.0f;                                                   // Sets the speed number
    }
	
	void Update () {
        float xMove = Input.GetAxis("Horizontal") * X_MOVEMENT;                         // Gets the input of the vertical axis and applies speed 
        float zMove = Input.GetAxis("Vertical") * Z_MOVEMENT;                           // Gets the input of the horizontal axis and applies speed 

        if(xMove > 0 || xMove < 0 || zMove > 0 || zMove < 0)            // Sets a an animation parameter based on how the player is moving
        {
            anim.SetFloat("Speed", 1);                        
        }
        else if(xMove == 0 || zMove == 0)
        {
            anim.SetFloat("Speed", 0);
        }

        Vector3 orientation = new Vector3(zMove, 0, xMove);                                               // Saves the orientation of the player
        transform.rotation = Quaternion.LookRotation(orientation);                                        // Changes the orientation of the player based in his movement direction
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(orientation), 0.15f);  // Makes the orientation change more smooth.

        transform.Translate(orientation * speed * Time.deltaTime, Space.World);                           // Transforms the player position
        mainCamera.transform.position = transform.position + offset;                                      // Transforms the camera position
	}
}
