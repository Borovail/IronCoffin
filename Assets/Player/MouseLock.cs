using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLock : MonoBehaviour
{
    public float mouseXSensitivity = 100;
    public float mouseYSensitivity = 100;

    public Transform playerBody;



    [SerializeField] private float _maxDistanceBeforePicture;
    [SerializeField] private float _power;

    private float xRotation = 0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PictureShaker());
        }
    }

	protected IEnumerator PictureShaker()
	{
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, _maxDistanceBeforePicture);

        if(hit.collider != null && hit.collider.gameObject.tag == "Picture")
        {
            hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(_power, _power, _power, ForceMode.VelocityChange);
            yield return new WaitForSeconds(1.2f);
            hit.collider.gameObject.GetComponent<HingeJoint>().breakForce = 0f;
        }
	}
}
