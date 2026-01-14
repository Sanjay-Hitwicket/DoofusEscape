using UnityEngine;
using UnityEngine.InputSystem;

namespace View {
    public class PlayerMovementView : MonoBehaviour {
        [SerializeField] private float movementspeed = 5f;
        [SerializeField] private InputActionAsset inputSystem;
        [SerializeField] private Rigidbody rb;
        
        private InputAction moveAction;
        private Vector2 moveAmt;

        private void Awake() {
            moveAction = inputSystem.FindActionMap("Player").FindAction("Move");
        }

        private void OnEnable() {
            inputSystem.FindActionMap("Player").Enable();
            //moveAction.Enable();
        }

        private void OnDisable() {
            inputSystem.FindActionMap("Player").Disable();
            //moveAction.Disable();
        }

        void Update() {
            moveAmt = moveAction.ReadValue<Vector2>();
            // DO JUMP ACTIONS HERE
        }

        private void FixedUpdate() {
            Movement();
        }

        private void Movement() {
            //Vector3  displacement = rb.transform.forward * moveAmt * movementspeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + transform.forward * moveAmt.y * movementspeed * Time.deltaTime);
        }
    }
}