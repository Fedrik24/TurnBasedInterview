using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedGame.Ability
{
    public class CameraFollow : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        [SerializeField] private Transform cameraTransform;
        private float xRotation = 0f;
        private float verticalLookRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleMouseLook();
        }

        void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            // Note : Disable Rotation of Y-Axis.
            // verticalLookRotation -= mouseY;
            // verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
            // cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        }
    }

}
