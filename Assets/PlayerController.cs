using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float moveSpeed;

        private float mouseX;
        private float mouseY;
        private float rotationX;
        private Vector3 moveInput;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;//cant have it locked all the time because of UI things
        }

        private void Update()
        {
            GetInputs();
            Look();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void LateUpdate()
        {
            playerCamera.transform.position = playerTransform.position + new Vector3(0, 0.5f, 0);
        }

        private void GetInputs()
        {
            moveInput = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
        }

        private void Look()
        {
            Vector3 rot = playerCamera.transform.localRotation.eulerAngles;
            float desiredX = rot.y + mouseX;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, desiredX, 0);
        }

        private void Move()
        {
            Vector3 moveDirection = playerCamera.transform.TransformDirection(moveInput) * moveSpeed;
            playerRb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        }
    }
}