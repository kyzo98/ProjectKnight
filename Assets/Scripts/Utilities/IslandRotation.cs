using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandRotation : MonoBehaviour {

    //VARIABLES
    Vector3 rotationMask;
    public float rotationSpeed;
    public GameObject lookAtObject;

	// Use this for initialization
	void Start () {
        rotationMask = new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(lookAtObject.transform.position, rotationMask, Time.deltaTime * rotationSpeed);
	}
}
