using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View {
    public class PlayerMovementView : MonoBehaviour {
        [SerializeField] private float movementspeed = 5f;
        [SerializeField] private InputActionAsset inputSystem;
        
        private InputAction moveAction;
        private Vector2 moveInput;
        private Rigidbody2D rb;

        private void Awake()
        {
            // Get the Rigidbody2D component
            rb = GetComponent<Rigidbody2D>();
            
            // Set up the move action from the input system
            moveAction = inputSystem.FindActionMap("Player").FindAction("Move");
        }

        private void OnEnable()
        {
            moveAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        void Update()
        {
            // Read the joystick input
            moveInput = moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            // Apply movement using physics
            Vector2 movement = moveInput * movementspeed;
            rb.linearVelocity = movement;
        }
    }
}