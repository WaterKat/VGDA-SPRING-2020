using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace WaterKat.Player
{
    public class Jump : MonoBehaviour
    {

        public enum PlayerJumpState
        {
            Standing = 0,
            Jumping = 1,
            Rising = 2,
            SlowFalling = 3,
            FreeFalling = 4,
        }

        [SerializeField]
        public PlayerJumpState JumpState = PlayerJumpState.Standing;

        Rigidbody PlayerRB;
        Player CurrentPlayer;

        int AvailableJumps = 2;
        public int MaxAvailableJumps = 2;

        public float GroundJumpHeight;
        public float AirJumpHeight;
        public float GroundJumpTime;

        public float FallingGravityRatio = -1000f;

        float FreefallVelocity;

        float RisingGravity = -1f;
        float FallingGravity = -1f;
        float GroundJumpVelocity = 1000f;
        float AirJumpVelocity = 1000f;

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();

            CurrentPlayer.InputActionMap.Gameplay.Jump.started += ctx => StartJump();
            CurrentPlayer.InputActionMap.Gameplay.Jump.canceled += ctx => EndJump();
        }

        void Start()
        {
            CalculateDesiredJump();
            PlayerRB = transform.GetComponentInChildren<Rigidbody>();

        }

        void CalculateDesiredJump()
        {
            RisingGravity = (2f * GroundJumpHeight) / Mathf.Pow(GroundJumpTime, 2);
            FallingGravity = RisingGravity * FallingGravityRatio;

            GroundJumpVelocity = GroundJumpTime * RisingGravity;
            AirJumpVelocity = RisingGravity * Mathf.Sqrt(2 * AirJumpHeight / RisingGravity);

            FreefallVelocity = -GroundJumpVelocity;
            /*
            Debug.Log("JumpVelocity" + GroundJumpVelocity);
            Debug.Log("AirJumpVelocity" + AirJumpVelocity);
            */
        }

        void StartJump()
        {
            Vector3 NewVelocity = PlayerRB.velocity;
            if (AvailableJumps > 0)
            {
                AvailableJumps -= 1;
                JumpState = PlayerJumpState.Jumping;

                if (CurrentPlayer.Grounded)
                {
                    NewVelocity.y = GroundJumpVelocity;
                }
                else
                {
                    NewVelocity.y = AirJumpVelocity;
                }
            }
            PlayerRB.velocity = NewVelocity;
        }

        void EndJump()
        {
            JumpState = PlayerJumpState.FreeFalling;
        }

        void Update()
        {
            Vector3 CurrentVelocity = PlayerRB.velocity;
            Vector3 NewVelocity = CurrentVelocity;

            Vector3 GroundVelocity;
            bool Grounded = CurrentPlayer.CheckIfGrounded(out GroundVelocity);

            if (Grounded)
            {
                AvailableJumps = MaxAvailableJumps;
            }

            switch (JumpState)
            {
                case PlayerJumpState.Standing:
                    if (Grounded)
                    {
                        NewVelocity.y += -1f * Time.deltaTime;
                    }
                    else
                    {
                        JumpState = PlayerJumpState.SlowFalling;
                        goto case PlayerJumpState.SlowFalling;
                    }
                    break;

                case PlayerJumpState.Jumping:

                    if (CurrentVelocity.y < 0)
                    {
                        JumpState = PlayerJumpState.SlowFalling;
                        goto case PlayerJumpState.SlowFalling;
                    }
                    goto case PlayerJumpState.SlowFalling;
                    //break;

                case PlayerJumpState.SlowFalling:

                    if (CurrentVelocity.y < FreefallVelocity)
                    {
                        JumpState = PlayerJumpState.FreeFalling;
                        goto case PlayerJumpState.FreeFalling;
                    }
                    if ((Grounded) && (CurrentVelocity.y - GroundVelocity.y) < 0)
                    {
                        JumpState = PlayerJumpState.Standing;
                        goto case PlayerJumpState.Standing;
                    }
                    NewVelocity.y += -RisingGravity * Time.deltaTime;
                    break;

                case PlayerJumpState.FreeFalling:
                    if ((Grounded) && (CurrentVelocity.y - GroundVelocity.y) < 0)
                    {
                        JumpState = PlayerJumpState.Standing;
                        goto case PlayerJumpState.Standing;
                    }
                    NewVelocity.y += -FallingGravity * Time.deltaTime;
                    break;
            }

            PlayerRB.velocity = NewVelocity;
        }
    }
}
