using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDowner : MonoBehaviour
{
    [SerializeField] private GameObject seatCam;
    [SerializeField] private GameObject startRayPoint;

    public bool isAvialable;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip notAvialable;

    private CameraActivator toCamActive;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(startRayPoint.transform.position, startRayPoint.transform.forward);
            Debug.DrawRay(startRayPoint.transform.position, startRayPoint.transform.forward * 2, Color.blue);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.gameObject.CompareTag("Seatbelt"))
                {
                    toCamActive = hit.collider.gameObject.GetComponent<CameraActivator>();
                    if(toCamActive.isAvialableSeat == true)
                    {
                        toCamActive.camActive();
                    }
                    else
                    {
                        source.PlayOneShot(notAvialable);
                    }
                }
                //Debug.Log("+Ray");
            }
        }
    }
}
