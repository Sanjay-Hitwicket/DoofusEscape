using System;
using UnityEngine;

namespace View {
    public class PlayerMovementView : MonoBehaviour {
        public float speed = 5f;
        public float jumpAmount = 2f;

        void Update()
        {
            // Basic movement
            if (Input.GetKey(KeyCode.W))
                transform.position += Vector3.forward * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.S))
                transform.position += Vector3.back * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.A))
                transform.position += Vector3.left * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.D))
                transform.position += Vector3.right * speed * Time.deltaTime;

            // Instant jump upwards
            if (Input.GetKeyDown(KeyCode.Space))
                transform.position += Vector3.up * jumpAmount;
        }
    }
}