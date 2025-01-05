using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace TurnBasedGame.Ability
{
    public class PlayerController : CharacterAbility
    {
        // Player movement settings
        public float speed = 5f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;

        // Mouse look settings
        public float mouseSensitivity = 100f;

        // References
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private CharacterController characterController;
        private Vector3 velocity;
        private float verticalLookRotation = 0f;
        private bool isMove;
        private float currentSpeed;

        private void Start()
        {
            // Lock the cursor to the game window
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            HandleMovement();
        }

        void HandleMovement()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            characterController.Move(move * speed * Time.deltaTime);

            isMove = move.magnitude > 0.01f;
            currentSpeed = move.magnitude;

            // Apply gravity
            if (!characterController.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Apply vertical velocity
            characterController.Move(velocity * Time.deltaTime);
        }

        public override void UpdateAnimator()
        {
            UpdateAnimatorBool("Running", isMove);
            UpdateAnimatorFloat("Speed", currentSpeed);
        }
    }
}