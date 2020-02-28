using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WaterKat.Player_N
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

        public int FlyMode = 0; // 0:First Person Shooter 1:Exploration Mode

        public float MaxVelocity = 10f;
        public float Acceleration = 1f;
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

        private float DragMultiplier;

        public float DesiredVelocityMultiplier = 2f;
        private float BoostVelocityMultiplier = 2f;
        public float BoostCostMultiplier = 1.5f;
        public bool Boosting = false;

        public float DeadzoneBrake = 0.1f;
        public float DeadzoneBrakeAccelerationMultiplier = 16f;

        public float BodyRotationSpeed = 10f;
        public Quaternion LastViableRotation = Quaternion.identity;

        public float FlightTime = 10;
        public float FlightMax = 10;
        public float FlightRecoveryMod = 2;
        public float FlightFallingDrag = 0;

        public UI_UpdateAlphaMask FlyUI;

        private void Awake()
        {
            CurrentPlayer = GetComponent<Player>();
            Move_X_Input = CurrentPlayer.InputActionMap.Gameplay.Move_X;
            Move_Y_Input = CurrentPlayer.InputActionMap.Gameplay.Move_Y;
            Move_Z_Input = CurrentPlayer.InputActionMap.Gameplay.Move_Z;
            CurrentPlayer.InputActionMap.Gameplay.Boost.started += ctx => ToggleBoost();
        }
        void Start()
        {
            CurrentRigidbody = GetComponent<Rigidbody>();
            CurrentCameraController = GetComponent<CameraController>();
            DragMultiplier = (-2 * Acceleration) / Mathf.Pow(MaxVelocity, 2);
            BoostVelocityMultiplier = Mathf.Pow(DesiredVelocityMultiplier, 2);
        }

        void ToggleBoost()
        {
            Boosting = !Boosting;
        }

        private void Update()
        {
            FlyUI.deltaDesiredMask = (FlightTime / FlightMax);

            if (CurrentCameraController.CameraTransitioning) //Changes State based on camera conditions
            {
                FlyMode = 0;
            }
            else
            {
                FlyMode = 1;
            }

            Vector3 CurrentVelocity = CurrentRigidbody.velocity;

            Quaternion CameraRotationModifier;              //Uses right camera rotation in order to calculate movement
            if (FlyMode == 0)
            {
                //CameraRotationModifier = Camera.main.transform.rotation;
                CameraRotationModifier = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

            }
            else
            {
                CameraRotationModifier = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            }

            Vector3 DesiredMovement = new Vector3(Move_X_Input.ReadValue<float>(), Move_Y_Input.ReadValue<float>(), Move_Z_Input.ReadValue<float>()).normalized;   //reads input
            DesiredMovement = CameraRotationModifier * DesiredMovement;

            Vector3 XZDesiredMovement = new Vector3(Move_X_Input.ReadValue<float>(), 0, Move_Z_Input.ReadValue<float>());       //reads input -Y axis;
            if (XZDesiredMovement.magnitude > 0)
            {
                LastViableRotation = Quaternion.RotateTowards(CurrentPlayer.PlayerBody.transform.rotation, Quaternion.LookRotation(CameraRotationModifier * XZDesiredMovement,CameraRotationModifier*Vector3.up), BodyRotationSpeed);
            }
            CurrentPlayer.PlayerBody.transform.rotation = LastViableRotation;

            Vector3 DeadzoneBrakeVector = Vector3.zero;
            Vector3 RelativeCameraVelocity = Quaternion.Inverse(CameraRotationModifier) * CurrentVelocity;

            if (Mathf.Abs(Move_X_Input.ReadValue<float>()) <= DeadzoneBrake)
            {
                DeadzoneBrakeVector.x = -1 * Mathf.Clamp(RelativeCameraVelocity.x / Acceleration, -1, 1) * Acceleration / DeadzoneBrakeAccelerationMultiplier;
            }
            if (Mathf.Abs(Move_Y_Input.ReadValue<float>()) <= DeadzoneBrake)
            {
                DeadzoneBrakeVector.y = -1 * Mathf.Clamp(RelativeCameraVelocity.y / Acceleration, -1, 1) * Acceleration / DeadzoneBrakeAccelerationMultiplier;
            }
            if (Mathf.Abs(Move_Z_Input.ReadValue<float>()) <= DeadzoneBrake)
            {
                DeadzoneBrakeVector.z = -1 * Mathf.Clamp(RelativeCameraVelocity.z / Acceleration, -1, 1) * Acceleration / DeadzoneBrakeAccelerationMultiplier;
            }

            DeadzoneBrakeVector = CameraRotationModifier * DeadzoneBrakeVector;


            Vector3 DragVector = CurrentVelocity.normalized * Time.deltaTime * (DragMultiplier * Mathf.Pow(CurrentRigidbody.velocity.magnitude, 2) / 2);
            Vector3 MovementVector =  MovementVector = DesiredMovement * Time.deltaTime * ModifiedAcceleration;


            if ((CurrentPlayer.Grounded)&&(!Boosting))
            {
                FlightTime = Mathf.Min(FlightTime+(FlightRecoveryMod * Time.deltaTime),FlightMax);
                MovementVector.y = 0;
                DragVector.y = 0;
                DeadzoneBrakeVector.y = 0;
            }
            else
            {
                if (FlightTime > 0)
                {
                    FlightTime += -1 * (ModifiedAcceleration/Acceleration) * Time.deltaTime;
                }
                else
                {
                    MovementVector.y = (Physics.gravity.y * Time.deltaTime) + Mathf.Min(MovementVector.y, 0);
                    DeadzoneBrakeVector.y = 0;
                    DragVector.y = Mathf.Min(DragVector.y,FlightFallingDrag);
                }
            }

            CurrentRigidbody.velocity += DragVector+MovementVector+DeadzoneBrakeVector;

            //            Debug.Log("Velocity: " + CurrentRigidbody.velocity.magnitude);

            if ((Boosting)&&((DesiredMovement.magnitude < .75f)||(FlightTime<=0)))
            {
                Boosting = false;
            }
        }
    }
}