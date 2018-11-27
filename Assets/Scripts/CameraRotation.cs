using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    // Target related variables
    public Transform target;              // Camera targeting gameobject.
    private Vector3 offset;               // Distance between target gameobject and the actual camera.

    //New positions for the camera
    public Vector3 mainCameraNewPosition;

    // Speed related to the camera movement
    public float speed = 0.2f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(CameraChanging());
    }

    IEnumerator CameraChanging()
    {
        transform.LookAt(target);
        Vector3 updatedPosition = mainCameraNewPosition + offset;
        Vector3 positionChange = Vector3.Lerp(transform.position, updatedPosition, Time.deltaTime * speed);
        transform.position = positionChange;
        yield return new WaitForSeconds(0);
    }
}
