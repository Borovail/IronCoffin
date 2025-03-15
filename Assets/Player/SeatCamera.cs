using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeatCamera : MonoBehaviour
{
    public float mouseXSensitivity = 90;
    public float mouseYSensitivity = 90;
    private float xRotation;
    private float yRotation = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }

    public void CameraToBasic()
    {
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        yRotation = 90f;
        xRotation = 0f;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 40f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, 30f, 150f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
