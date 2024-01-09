using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cameraSensitivity = 3f;
    float rotationX;
    float rotationY;
    Transform cameraTransform;

    void Start()
    {
        cameraTransform = transform.GetChild(0);
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity;
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }
    }
}
