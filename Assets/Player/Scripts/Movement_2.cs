using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CameraController))]

    public class Movement_2 : MonoBehaviour
    {
        Player CurrentPlayer;
        InputAction Move_X_Input;
        InputAction Move_Z_Input;

        Rigidbody CurrentRigidbody;
        CameraController CurrentCameraController;

        private float DragMultiplier;
        private bool Boosting = false;
        private float BoostVelocityMultiplier = 2;


        public float MaxVelocity = 10f;
        public float Acceleration = 10f;
        /*
        public float ModifiedAcceleration
        {
            get
            {
                if (Boosting)
                {
                    return Acceleration * BoostVelocityMultiplier;
                }
                else
                {
                    return Acceleration;
                }
            }
        }
        */
        

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();
            Move_X_Input = CurrentPlayer.InputActionMap.Gameplay.Move_X;
            Move_Z_Input = CurrentPlayer.InputActionMap.Gameplay.Move_Z;
        }

        void Start()
        {
            CurrentRigidbody = GetComponent<Rigidbody>();
            CurrentCameraController = GetComponent<CameraController>();
            DragMultiplier = (-2 * Acceleration) / Mathf.Pow(MaxVelocity, 2);
        }

        private Vector3 desiredInput = Vector3.zero;
        private Vector3 movementAcceleration = Vector3.zero;
        private Quaternion cameraRotation = Quaternion.identity;

        void Update()
        {
            desiredInput.x = Move_X_Input.ReadValue<float>();
            desiredInput.z = Move_Z_Input.ReadValue<float>();
            cameraRotation = Quaternion.Euler(0, CurrentCameraController.CameraQuaternion.eulerAngles.y, 0);

            movementAcceleration = cameraRotation * desiredInput;
        }
    }
}