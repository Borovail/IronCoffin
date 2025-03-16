using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace interactive
{
    public class ControlPanel : MonoBehaviour
    {
        public float length = 0; 
        public float rotation = 0;
        
        [SerializeField] private MeshRenderer leftLight;
        [SerializeField] private MeshRenderer rightLight;
        
        private float rotationSpeed = 10f;
        private float moveSpeed = 10f;

        public bool isRotating = false;
        public bool movingForward = false;
        
        public bool isAbleToShoot = false;

        private int magic_number = 10; 

        private void Start()
        {
            update_length_and_rotation(1, 1);
        }
        
        private void Update()
        {
            if (rotation <= 0 && isRotating)
            {
                StopRotate();
            }
        
            if (length <= 0 && movingForward)
            {
                StopMoveForward();
            }
            
            if (isRotating)
            {
                rotation -= rotationSpeed * Time.deltaTime;
                print(rotation);
            }
        
            if (movingForward)
            {
                length -= moveSpeed * Time.deltaTime;
                print(length);
            }
        
            if (length <= 0 && rotation <= 0)
            {
                isAbleToShoot = true;
            }
        }

        public void StartRotate() => isRotating = true;
        public void StopRotate() => isRotating = false;

        public void StartMoveForward() => movingForward = true;
        public void StopMoveForward() => movingForward = false;

        public void AbleToShoot()
        {
            isAbleToShoot = true;
        }
        
        public void UnableToShoot()
        {
            isAbleToShoot = false;
        }

        public void update_length_and_rotation(int multiplier_for_rotation, int multiplier_for_move)
        {
            rotation = multiplier_for_rotation * magic_number;
            length = multiplier_for_move * magic_number;
        }

        public void LoadBullet()
        {
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, -8f), 2f);
        }

        public void ShootBullet()
        {
            transform.DOMoveZ(-5.95f, 0.2f).SetEase(Ease.Linear);
            update_length_and_rotation(2, 2);
        }
    }
}