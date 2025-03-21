﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement_Basic : MonoBehaviour
{
    [SerializeField] private GameObject _flashlight;

    private Transform transform;

    [SerializeField] private CharacterController controller;

    public float walkSpeed = 10f;
    public float runSpeed = 14f;
    public float gravity = 9.81f;
    //public float jumpHeight = 5f;

    public float speed;

    private Vector3 velocity;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool _isGrounded = false;

    private float x;
    private float z;

    private void Start()
    {
        speed = walkSpeed;
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && _isGrounded)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        _isGrounded = controller.isGrounded;

        if (_isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        z = Input.GetAxis("Vertical");
        x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{

        //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        //}

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.L))
        {
            _flashlight.SetActive(!_flashlight.activeSelf);
        }
    }

    
}
