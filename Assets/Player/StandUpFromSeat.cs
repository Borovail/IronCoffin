using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpFromSeat : MonoBehaviour
{
    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject seatCam;

    private Quaternion playerRot;

    private void Awake()
    {
        seatCam = this.gameObject;
        playerBody = GameObject.FindWithTag("Player");
        playerRot = playerBody.transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerBody.SetActive(true);
            playerBody.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            seatCam.SetActive(false);
        }
    }
}
