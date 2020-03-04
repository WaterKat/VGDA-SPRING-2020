using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class Jump : MonoBehaviour
    {
        Player currentPlayer;
        Rigidbody currentRigidbody;

        InputAction jump_Input;

        [SerializeField]
        private float gravity;
        public float Gravity
        {
            get
            {
                return gravity;
            }
        }
        public float groundedGravity = 0.5f;
        public bool applyGravity = true;

        public float jumpHeight = 10;
        public float jumpTime = 0.5f;
        private float jumpVelocity;

        private void Awake()
        {
            currentPlayer = GetComponent<Player>();
            currentRigidbody = GetComponent<Rigidbody>();

            jump_Input = currentPlayer.InputActionMap.Gameplay.Jump;
            jump_Input.started += ctx => StartJump();
        }

        void Start()
        {
            gravity = (2.0f * jumpHeight) / Mathf.Pow(jumpTime, 2);
            jumpVelocity = gravity * jumpTime;
        }

        private Vector3 currentPlayerVelocity = Vector3.zero;

        private void Update()
        {
            if (applyGravity)
            {
                /*
                currentPlayerVelocity = currentRigidbody.velocity;
                currentPlayerVelocity.y +=  -1f * gravity * Time.deltaTime;
                currentRigidbody.velocity = currentPlayerVelocity;
                */
                if (currentPlayer.Grounded)
                {
                    //currentRigidbody.velocity += Vector3.down* groundedGravity * Time.deltaTime;
                }
                else
                {
                    currentRigidbody.velocity += Vector3.down * gravity * Time.deltaTime;
                }
            }
        }

        private void StartJump()
        {
            if (currentPlayer.Grounded)
            {
                currentPlayerVelocity = currentRigidbody.velocity;
                currentPlayerVelocity.y = jumpVelocity;
                currentRigidbody.velocity = currentPlayerVelocity;
            }
        }
    }
}