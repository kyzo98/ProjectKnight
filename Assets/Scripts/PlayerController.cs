﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {
    private Camera mainCamera;                                           // Camera that follows the player in the lobby
    public CinemachineFreeLook cinemachineFree;                          // saves the cm free look component
    private float xAxisMaxSpeed;
    private float yAxisMaxSpeed;
    public bool xMovementInverted;                                      // Bool to on/off inverted movement in x axis
    public bool zMovementInverted;                                      // Bool to on/off inverted movement in z axis

    private float speed;                                                // Variable that controls the speed of movement of the character
    private Vector3 offset;                                             // Distance between the camera and the character
    private Transform mainCameraTransform;                              // Variable that saves the main camera transform
    private int X_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it
    private int Z_MOVEMENT;                                             // Variable that saves the number that multiplies the control to invert it
    private Rigidbody rigidbody;                                        // variable that saves the rigidbody component of the character.

    public Animator anim;                                                      // It saves the animator.

    public GameObject pauseMenu;
    public GameObject characterMenu;
    public GameObject inventoryMenu;
    public GameObject menusMenu;

    void Start () {
        anim = this.GetComponent<Animator>();                                // Gets the animator from the actual character.
        rigidbody = this.GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        xAxisMaxSpeed = 250;
        yAxisMaxSpeed = 2;

        transform.position = new Vector3(-18.43f, 0.09f, 1.63f);                      // Positions in which the character will be spawned
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
                cinemachineFree.m_XAxis.m_MaxSpeed = 0;
                cinemachineFree.m_YAxis.m_MaxSpeed = 0;
            }
            else
            {
                if (characterMenu.activeSelf)
                {
                    inventoryMenu.SetActive(false);
                    menusMenu.SetActive(false);
                    characterMenu.SetActive(false);
                    cinemachineFree.m_XAxis.m_MaxSpeed = xAxisMaxSpeed;
                    cinemachineFree.m_YAxis.m_MaxSpeed = yAxisMaxSpeed;
                }
                else
                {
                    inventoryMenu.SetActive(false);
                    characterMenu.SetActive(true);
                    cinemachineFree.m_XAxis.m_MaxSpeed = 0;
                    cinemachineFree.m_YAxis.m_MaxSpeed = 0;
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
                cinemachineFree.m_XAxis.m_MaxSpeed = 0;
                cinemachineFree.m_YAxis.m_MaxSpeed = 0;
            }
            else
            {
                if (inventoryMenu.activeSelf)
                {
                    characterMenu.SetActive(false);
                    menusMenu.SetActive(false);
                    inventoryMenu.SetActive(false);
                    cinemachineFree.m_XAxis.m_MaxSpeed = xAxisMaxSpeed;
                    cinemachineFree.m_YAxis.m_MaxSpeed = yAxisMaxSpeed;
                }
                else
                {
                    characterMenu.SetActive(false);
                    inventoryMenu.SetActive(true);
                    cinemachineFree.m_XAxis.m_MaxSpeed = 0;
                    cinemachineFree.m_YAxis.m_MaxSpeed = 0;
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

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 orientation = forward * zMove + right * xMove;                                               // Saves the orientation of the player based in where the camera is looking
        transform.rotation = Quaternion.LookRotation(orientation);                                        // Changes the orientation of the player based in his movement direction
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(orientation), 0.15f);  // Makes the orientation change more smooth.

        rigidbody.velocity = orientation * speed;                                                           //Moves the player. Changed it because the transform.translate doesn't care about all the collisions

        //We don't need to update camera position now, is a free look cinemachine now and using it's follow and look at components is all we need to update it.
	}
}
