using System;
using TurnBasedGame.Type;
using UnityEngine;


namespace TurnBasedGame.Ability
{
    /// <summary>
    /// just Character Movement
    /// </summary>
    public class PlayerController : CharacterAbility,IDisposable
    {
        [SerializeField] private CharacterController characterController;

        public float speed = 5f;
        public float jumpHeight = 2f;
        public float gravity = -9.81f;
        public float mouseSensitivity = 100f;

        private Vector3 velocity;
        private float verticalLookRotation = 0f;
        private bool isMove;
        private float currentSpeed;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void Update()
        {
            if(character.gameState == GameState.Battle)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
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

        public void Dispose()
        {
        }
    }
}