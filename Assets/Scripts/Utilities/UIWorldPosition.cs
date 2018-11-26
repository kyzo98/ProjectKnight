using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldPosition : MonoBehaviour {
    public Transform target;                                                    // Game Object to which the UI element will be attached 
    public Camera mainCamera;                                                   // Camera where the UI will be displayed

    private Vector3 realPosition;                                               // Variable that saves the real position in the world where the UI element will be displayed

    void Start() {
        realPosition = target.position + new Vector3(0, 1.1f, 0);               // Calculates the real position
    }

    void Update() {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(realPosition);   // Calculates the equivalent of screen position to the real position of the UI element
        transform.position = screenPosition;                                    // Updates the UI element in the screen position
    }
}
