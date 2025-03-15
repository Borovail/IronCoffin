using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeatCamera : MonoBehaviour
{
    public float mouseXSensitivity = 90;
    public float mouseYSensitivity = 90;

    private float xRotation;
    private float yRotation = 0f;

    [SerializeField] private float _clampXMin;
	[SerializeField] private float _clampXMax;
	[SerializeField] private float _clampYMin;
	[SerializeField] private float _clampYMax;



	void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void CameraToBasic()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        yRotation = 0f;
        xRotation = 0f;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, _clampXMin, _clampXMax);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, _clampYMin, _clampYMax);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
