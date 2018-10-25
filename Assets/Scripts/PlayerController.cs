using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {
    private const int INVERT_CONTROL = -1;
    public Camera mainCamera;                                           // Camara that follows the player in the lobby

    private float speed;                                                // Variable that controls the speed of movement of the character
    private Vector3 offset;                                             // Distance between the camera and the character
    private Transform mainCameraTransform;                              // variable that saves the main camera transform
    
	
	void Start () {
        speed = 5.0f;                                           
        transform.position = new Vector3(0, 1, 0);                      //Positions in which the character will be spawned
        transform.eulerAngles = new Vector3(0, 0, 0);                   //Orientation of character spawn
          
        mainCameraTransform = mainCamera.GetComponent<Transform>();     // Gets the main camera transform information
        mainCameraTransform.position = new Vector3(-5, 3, 0);           // Position where the camera is set at start
        mainCameraTransform.eulerAngles = new Vector3(30, 90, 0);       // Orientation of camera spawn

        offset = mainCamera.transform.position - transform.position;    // Measures the distance between the character and the camera
    }
	
	
	void Update () {
        float xMove = Input.GetAxis("Vertical") * speed ;                     //Gets the input of the vertical axis and applies speed 
        float zMove = Input.GetAxis("Horizontal") * speed * INVERT_CONTROL;   //Gets the input of the horizontal axis and applies speed 

        xMove *= Time.deltaTime;                                              // Makes the movement based on time and not in frames per second
        zMove *= Time.deltaTime;                                              // Makes the movement based on time and not in frames per second
            
        transform.Translate(xMove, 0, zMove);                                 //Transforms the player position
        mainCamera.transform.position = transform.position + offset;          //Transforms the camera position
		
	}
}
