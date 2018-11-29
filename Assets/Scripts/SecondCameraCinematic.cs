using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraCinematic : MonoBehaviour {

    // 
    public Transform target;
    private Vector3 offset;

    public Vector3 newPosition;
    Vector3 secondCameraUpdatedPosition;
    Vector3 secondCameraPositionChange;

    float speed = 0.2f;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(SecondCameraMovement());
	}

    IEnumerator SecondCameraMovement()
    {
        transform.LookAt(target);
        secondCameraUpdatedPosition = newPosition + offset;
        secondCameraPositionChange = Vector3.Lerp(transform.position, secondCameraUpdatedPosition, Time.deltaTime * speed);
        transform.position = secondCameraPositionChange;
        yield return new WaitForSeconds(0);
    }
}
