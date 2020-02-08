using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]

    public class Fly : MonoBehaviour
    {
        Player CurrentPlayer;
        InputAction Move_X_Input;
        InputAction Move_Y_Input;
        InputAction Move_Z_Input;

        Rigidbody CurrentRigidbody;

        public CameraController CurrentCameraController;

        public int FlyMode = 0;

        public float MaxVelocity = 10f;
        public float Acceleration = 1f;
        private float DragMultiplier;

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();
            Move_X_Input = CurrentPlayer.InputActionMap.Gameplay.Move_X;
            Move_Y_Input = CurrentPlayer.InputActionMap.Gameplay.Move_Y;
            Move_Z_Input = CurrentPlayer.InputActionMap.Gameplay.Move_Z;
        }
        void Start()
        {
            CurrentRigidbody = GetComponent<Rigidbody>();
            CurrentCameraController = GetComponent<CameraController>();
            DragMultiplier = (-2 * Acceleration) / Mathf.Pow(MaxVelocity, 2);
        }
        private void Update()
        {
            if (CurrentCameraController.CameraTransitioning)
            {
                FlyMode = 0;
            }
            else
            {
                FlyMode = 1;
            }
            Vector3 DesiredMovement = Vector3.zero;
            if (FlyMode == 0)
            {
                DesiredMovement = new Vector3(Move_X_Input.ReadValue<float>(), Move_Y_Input.ReadValue<float>(), Move_Z_Input.ReadValue<float>());
                DesiredMovement = Camera.main.transform.rotation * DesiredMovement;
            }
            else if (FlyMode == 1)
            {
                DesiredMovement = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * new Vector3(Move_X_Input.ReadValue<float>(), 0, Move_Z_Input.ReadValue<float>());
                DesiredMovement += new Vector3(0, Move_Y_Input.ReadValue<float>(), 0);
            }

            Vector3 CurrentVelocity = CurrentRigidbody.velocity;
            Vector3 DragVector = CurrentVelocity.normalized * Time.deltaTime * (DragMultiplier * Mathf.Pow(CurrentRigidbody.velocity.magnitude, 2) / 2);
            Vector3 MovementVector = DesiredMovement * Time.deltaTime * Acceleration;
            CurrentRigidbody.velocity += DragVector+MovementVector;

            Debug.Log("Velocity: " + CurrentRigidbody.velocity.magnitude);
        }
    }
}