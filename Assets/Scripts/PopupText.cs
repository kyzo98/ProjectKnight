using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour {

    public float DestroyTime = 1.5f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
