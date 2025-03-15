using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraActivator : MonoBehaviour
{
    [SerializeField] private GameObject seatCamera;
    [SerializeField] private GameObject playerBody;

    public bool isAvialableSeat = true;

    public void camActive()
    {
        seatCamera.SetActive(true);
        seatCamera.GetComponent<SeatCamera>().CameraToBasic();
        playerBody.SetActive(false);
    }

    void Start()
    {
        playerBody = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }
}
