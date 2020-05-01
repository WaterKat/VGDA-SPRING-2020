using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WaterKat.Audio;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Jump))]
    public class Jetpack : MonoBehaviour
    {
        Player currentPlayer;
        Rigidbody currentRigidbody;
        Jump currentJump;

        InputAction jetpack_Input;

        public float fuel = 100;
        public float maxFuel = 100;

        public float fuelRecovery = 5;
        public float groundedFuelRecovery = 30;

        public float fuelCost = 20;

        [SerializeField]
        private bool jetpacking = false;

        public float jetpackMaxVelocity = 10;
        public float jetpackAcceleration = 10;
        private float jetpackDragMultiplier;

        public float jetpackTurnaroundMultiplier = 5;

        public CameraShake cameraShake;

        private void Awake()
        {
            currentPlayer = GetComponent<Player>();

            jetpack_Input = currentPlayer.InputActionMap.Gameplay.Jetpack;
            jetpack_Input.started += ctx => jetpacking = true;
            jetpack_Input.canceled += ctx => jetpacking = false;
        }

        void Start()
        {
            currentRigidbody = GetComponent<Rigidbody>();
            currentJump = GetComponent<Jump>();

            jetpackDragMultiplier = (-2 * jetpackAcceleration) / Mathf.Pow(jetpackMaxVelocity, 2);
        }

        private Vector3 currentPlayerVelocity = Vector3.zero;

        private float modifiedJetpackAcceleration = 0.0f;
        private float modifiedJetpackDragAcceleration = 0.0f;

        void Update()
        {
            currentPlayerVelocity = currentRigidbody.velocity;

            modifiedJetpackDragAcceleration = 0.5f * jetpackDragMultiplier * Mathf.Sign(currentPlayerVelocity.y) * Mathf.Pow(currentPlayerVelocity.y, 2);
            modifiedJetpackDragAcceleration = Mathf.Min(0, modifiedJetpackDragAcceleration);

            //Debug.Log("Jetpack Drag: " + modifiedJetpackDragAcceleration);

            if (!jetpacking)
            {
                AudioManager.StopSound("JetpackSustain");
                cameraShake.ResetShake();

                if (currentPlayer.Grounded)
                {
                    fuel += groundedFuelRecovery * Time.deltaTime;
                }
                else
                {
                    fuel += fuelRecovery * Time.deltaTime;
                }
                fuel = Mathf.Clamp(fuel, 0, maxFuel);
            }


            if (jetpacking && CanUseFuel())
            {
                if(Input.GetKey(KeyCode.Space) && !AudioManager.SoundPlaying("JetpackSustain"))
                {
                    AudioManager.PlaySound("JetpackSustain");
                }
                // Camera shake
                StartCoroutine(cameraShake.BoostStartShake(0.1f, 0.06f));
                cameraShake.BoostCameraShake(0.02f);

                modifiedJetpackAcceleration = jetpackAcceleration + Mathf.Abs(currentJump.Gravity);
                if ((currentPlayerVelocity.y < 0) && CanUseFuel());
                {
                    modifiedJetpackAcceleration *= jetpackTurnaroundMultiplier;
                }
                currentPlayerVelocity.y += (modifiedJetpackAcceleration + modifiedJetpackDragAcceleration) * Time.deltaTime;
            }
            else
            {
                AudioManager.StopSound("JetpackSustain");
                cameraShake.ResetShake();
                currentPlayerVelocity.y += (modifiedJetpackDragAcceleration) * Time.deltaTime;
            }

            currentRigidbody.velocity = currentPlayerVelocity;
        }

        private bool CanUseFuel()
        {
            float modFuelCost = -1f * fuelCost * Time.deltaTime;
            if (fuel + modFuelCost >= 0)
            {
                fuel += modFuelCost;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}