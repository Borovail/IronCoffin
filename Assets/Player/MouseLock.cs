using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLock : MonoBehaviour
{
    public float mouseXSensitivity = 100;
    public float mouseYSensitivity = 100;

    public Transform playerBody;

    public bool _isHaveEstinguisher = false;

    [SerializeField] private float _maxDistanceBeforePicture;
    [SerializeField] private float _power;

    private float xRotation = 0f;

    private Extinguisher _ext;


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

	private IEnumerator PictureShaker()
	{

		RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, _maxDistanceBeforePicture);

		if (_isHaveEstinguisher == true)
		{
			_ext.LetGoF();
			_ext.enabled = false;
			_isHaveEstinguisher = false;
		}

		if (hit.collider != null && hit.collider.gameObject.CompareTag("Picture"))
        {
            hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(_power, 0f, _power, ForceMode.VelocityChange);
            yield return new WaitForSeconds(1.2f);
            hit.collider.gameObject.GetComponent<HingeJoint>().breakForce = 0f;
        }
        else if (hit.collider.gameObject.CompareTag("Spotter"))
        {
            hit.collider.gameObject.GetComponent<Turret>().ActivateAim();
		}
		else if (hit.collider.gameObject.CompareTag("Extinguisher"))
		{
            if(_isHaveEstinguisher == false)
            {
				_ext = hit.collider.gameObject.GetComponent<Extinguisher>();
                _ext.enabled = true;

				yield return new WaitForEndOfFrame();
				yield return new WaitForEndOfFrame();

				_ext.HoldEstinguisher();
                _isHaveEstinguisher = true;
			}
		}

	}
}
