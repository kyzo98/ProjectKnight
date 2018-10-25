using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldPosition : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;

    private Vector3 postionMove;
    private Vector3 realPostion;


    void Start()
    {
        realPosition = target.position + positionMove;
    }


    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(realPosition);
        transform.position = screenPosition;
    }
}
