using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldPosition : MonoBehaviour {
    public Transform target;                                                    // Game Object that is attached our graphic
    public Camera mainCamera;                                                   // Camera that our UI will be displayed

    private Vector3 realPosition;                                               // Variable that save the real position in the world where the graphic will be displayed

    void Start() {
        realPosition = target.position + new Vector3(0, 0, 0);                  // Calculates the real position
    }

    void Update() {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(realPosition);   // Calculates the screen position where the real position is
        transform.position = screenPosition;                                    // Draw the graphic in the screen position
    }
}
