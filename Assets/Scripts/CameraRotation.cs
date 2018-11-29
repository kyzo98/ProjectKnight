using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    // Target related variables
    public Transform target;              // Camera targeting gameobject.
    private Vector3 offset;               // Distance between target gameobject and the actual camera.

    // Positions for the camera
    public Vector3 mainCameraNewPosition;
    Vector3 firstPosition;
    Vector3 updatedPosition;
    Vector3 positionChange;

    // Speed related to the camera movement
    private float speed = 0.2f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(CameraMovement());
        firstPosition = transform.position;
    }

    IEnumerator CameraMovement()
    {
        transform.LookAt(target);
        updatedPosition = mainCameraNewPosition + offset;
        positionChange = Vector3.Lerp(transform.position, updatedPosition, Time.deltaTime * speed);
        transform.position = positionChange;
        yield return new WaitForSeconds(0);
    }

}
