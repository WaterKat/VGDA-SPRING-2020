using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CameraController))]
    public class Partner : MonoBehaviour
    {
        public enum PartnerMovementState
        {
            Follow = 0,
            Idle = 1,
            Wander = 2,
        }
        public enum PartnerRotationState
        {
            Follow = 0,
            Idle = 1,
            LookAtMovingObject = 2,
            LookAtRandomObjects = 3,
            ScanTarget = 4,
        }

        public PartnerMovementState partnerMovementState = PartnerMovementState.Idle;
        public PartnerRotationState partnerRotationState = PartnerRotationState.Follow;

        public Player currentPlayer;
        private Rigidbody currentRigidbody;

        public float flyingMaxVelocity = 30;
        public float flyingAcceleration = 80;
        private float flyingDragMultiplier;

        public float floatingRange = 10;
        //  public float wanderMinTim
        public Ticker wanderTicker = new Ticker() { MaxTick = 5 };

        private void Start()
        {
            currentRigidbody = GetComponent<Rigidbody>();
        }

        private Vector3 TargetMovePosition = Vector3.zero;
        private Vector3 TargetLookPosition = Vector3.zero;
        private void Update()
        {
            //If Too far, change state to follow 
            if (Vector3.Distance(currentPlayer.transform.position, transform.position) > floatingRange)
            {
                partnerMovementState = PartnerMovementState.Follow;
            }

            switch (partnerMovementState)
            {
                case PartnerMovementState.Follow:
                    TargetMovePosition = currentPlayer.transform.position;

                    if (Vector3.Distance(currentPlayer.transform.position, transform.position) < floatingRange)
                    {
                        TargetMovePosition = transform.position;
                        partnerMovementState = PartnerMovementState.Idle;
                    }
                    break;
                case PartnerMovementState.Idle:
                    if (wanderTicker.TryTick())
                    {
                        goto case PartnerMovementState.Wander;
                    }
                    break;
                case PartnerMovementState.Wander:
                    Vector3 randomVector3 = currentPlayer.transform.position+new Vector3(Random.Range(-floatingRange, floatingRange)/2, Random.Range(-floatingRange, floatingRange)/2, Random.Range(-floatingRange, floatingRange)/2);
                    Vector3 directionalVector3 = (randomVector3 - transform.position);
                    RaycastHit raycastHit;
                    if(!Physics.Raycast(transform.position, directionalVector3.normalized, out raycastHit, directionalVector3.magnitude))
                    {
                        TargetMovePosition = transform.position + randomVector3;
                    }
                    else
                    {
                        TargetMovePosition = ((raycastHit.point - transform.position) * .75f)+transform.position;
                    }
                    break;
                default:
                    break;
            }

            switch (partnerRotationState)
            {
                case PartnerRotationState.Follow:
                    TargetLookPosition = transform.position + currentRigidbody.velocity;
                    break;
                case PartnerRotationState.Idle:
                    break;
                case PartnerRotationState.LookAtMovingObject:
                    Rigidbody[] rigidbodies = GameObject.FindObjectsOfType<Rigidbody>();
                    Rigidbody fastestRigidbody;
                    foreach (Rigidbody rigidbody in rigidbodies)
                    {
                        if (rigidbody == currentRigidbody) { continue; }
                        if (rigidbody == currentPlayer.GetComponent<Rigidbody>()) { continue; }

                        
                    }
                    break;
                case PartnerRotationState.LookAtRandomObjects:


                    break;
                case PartnerRotationState.ScanTarget:
                    break;
                default:
                    break;
            }

            MoveToPosition(TargetMovePosition);
            LookToPosition(TargetLookPosition);
        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            currentRigidbody.velocity = Vector3.ClampMagnitude(targetPosition - transform.position, flyingMaxVelocity); 
        }
        private void LookToPosition(Vector3 targetPosition)
        {
            transform.LookAt(targetPosition);
        }
    }
}