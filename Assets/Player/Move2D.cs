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
        InputAction MovementInputAction;

        Rigidbody2D CurrentRigidbody2D;

        public float Speed = 10;
        public float DragMultiplier = 1;

        private void Awake()
        {
            CurrentPlayer = GetComponent<PlayerEntity2D>();
            MovementInputAction = CurrentPlayer.PlayerInputActions.Gameplay.Move;
        }

        void Start()
        {
            CurrentRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 DesiredMovement = MovementInputAction.ReadValue<Vector2>();

            CurrentRigidbody2D.velocity += DesiredMovement * Speed * Time.deltaTime;
            CurrentRigidbody2D.velocity += (-1 * CurrentRigidbody2D.velocity.normalized) * (Mathf.Pow(CurrentRigidbody2D.velocity.magnitude,2)/2) * DragMultiplier;
        }
    }
}