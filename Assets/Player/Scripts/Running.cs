using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CameraController))]

    public class Running : MonoBehaviour
    {
        Player currentPlayer;
        InputAction move_X_Input;
        InputAction move_Z_Input;

        Rigidbody currentRigidbody;
        CameraController currentCameraController;

        public float runningMaxVelocity = 10;
        public float runningAcceleration = 10;
        private float runningDragMultiplier;

        private void Awake()
        {
            currentPlayer = GetComponent<Player>();
            move_X_Input = currentPlayer.InputActionMap.Gameplay.Move_X;
            move_Z_Input = currentPlayer.InputActionMap.Gameplay.Move_Z;
        }

        void Start()
        {
            currentRigidbody = GetComponent<Rigidbody>();
            currentCameraController = GetComponent<CameraController>();
            runningDragMultiplier = (-2 * runningAcceleration) / Mathf.Pow(runningMaxVelocity, 2);
        }

        private Vector3 currentPlayerVelocity = Vector3.zero;

        private Vector3 playerInput = Vector3.zero;

        private Quaternion cameraRotation = Quaternion.identity;

        private Vector3 runningAccelerationVector = Vector3.zero;
        private Vector3 runningDragAccelerationVector = Vector3.zero;


        void Update()
        {
            currentPlayerVelocity = currentRigidbody.velocity;
            currentPlayerVelocity.y = 0;

            playerInput.x = move_X_Input.ReadValue<float>();
            playerInput.z = move_Z_Input.ReadValue<float>();
            playerInput.Normalize();

            cameraRotation = Quaternion.Euler(0, currentCameraController.CameraQuaternion.eulerAngles.y, 0);

            runningAccelerationVector = cameraRotation * playerInput * runningAcceleration * Time.deltaTime;
            runningDragAccelerationVector = 0.5f * runningDragMultiplier * currentPlayerVelocity.normalized * Mathf.Pow(currentPlayerVelocity.magnitude, 2) * Time.deltaTime;


            currentRigidbody.velocity += runningAccelerationVector + runningDragAccelerationVector;
        }
    }
}