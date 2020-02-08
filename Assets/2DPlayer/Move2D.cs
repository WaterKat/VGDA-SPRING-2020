using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

namespace WaterKat.Player2D
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerEntity2D))]

    public class Move2D : MonoBehaviour
    {
        PlayerEntity2D CurrentPlayer;
        InputAction Move_XInput;
        InputAction Move_YInput;

        Rigidbody2D CurrentRigidbody2D;

        public float MaxVelocity = 10f;
        public float Acceleration= 1f;
        public float DragMultiplier;

        private void Awake()
        {
            CurrentPlayer = GetComponent<PlayerEntity2D>();
            Move_XInput = CurrentPlayer.PlayerInputActions.Gameplay.Move_X;
            Move_YInput = CurrentPlayer.PlayerInputActions.Gameplay.Move_Y;

        }

        void Start()
        {
            CurrentRigidbody2D = GetComponent<Rigidbody2D>();

            DragMultiplier = (-2 * Acceleration) / Mathf.Pow(MaxVelocity,2);
        }

        void Update()
        {
            Vector2 DesiredMovement = new Vector2(Move_XInput.ReadValue<float>(), Move_YInput.ReadValue<float>());


            // CurrentRigidbody2D.velocity += DesiredMovement * MaxVelocity * Time.deltaTime;
            //CurrentRigidbody2D.velocity += Time.deltaTime*(-1 * CurrentRigidbody2D.velocity.normalized) * (Mathf.Pow(CurrentRigidbody2D.velocity.magnitude,2)/2) * DragMultiplier;

            Vector2 CurrentVelocity = CurrentRigidbody2D.velocity;
            Vector2 DragVector = CurrentVelocity.normalized * Time.deltaTime * (DragMultiplier * Mathf.Pow(CurrentRigidbody2D.velocity.magnitude, 2) / 2);
            Vector2 MovementVector = DesiredMovement * Time.deltaTime * Acceleration;
            CurrentRigidbody2D.velocity += DragVector + MovementVector;

            Debug.Log("Velocity: " + CurrentRigidbody2D.velocity.magnitude);
        }
    }
}