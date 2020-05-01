using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.TimeKeeping;
using WaterKat.Player_N;
using WaterKat.Enemy_N;

namespace WaterKat.Player_N
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CameraController))]
    public class Partner : MonoBehaviour
    {
        public Player currentPlayer;
        private Rigidbody currentRigidbody;
        public CameraController currentCameraController;

        public float flyingMaxVelocity = 30;
        public float flyingAcceleration = 80;
        private float flyingDragMultiplier;

        public float shootingRange = 10;
        //  public float wanderMinTim
        public Ticker shootTicker = new Ticker() { MaxTick = 1 };

        public Vector3 targetOffset = new Vector3(3, 3, 0);

        public Enemy currentTarget;

        [SerializeField]
        GameObject happyFace;
        [SerializeField]
        GameObject angryFace;

        private void Start()
        {
            currentRigidbody = GetComponent<Rigidbody>();
            flyingDragMultiplier = (-2 * flyingAcceleration) / Mathf.Pow(flyingMaxVelocity, 2);

        }

        private Vector3 TargetMovePosition = Vector3.zero;
        private Vector3 TargetLookPosition = Vector3.zero;

        private void Update()
        {
            angryFace.SetActive(currentTarget!=null);
            happyFace.SetActive(currentTarget==null);
        }

        private void FixedUpdate()
        {
            Vector3 droneVelocity = currentRigidbody.velocity;




            TargetMovePosition = currentPlayer.transform.position + (currentCameraController.CameraQuaternion * targetOffset);
            if (currentTarget != null)
            {
                TargetLookPosition = currentTarget.transform.position;
                if (shootTicker.TryTick())
                {
                    Shoot(currentTarget.transform.position);
                }
            }
            else if (Vector3.Distance(TargetMovePosition, transform.position) < 2f)
            {
                TargetLookPosition = transform.position + (currentCameraController.cameraContainer.forward * 5);
            }
            else
            {
                TargetLookPosition = transform.position + currentRigidbody.velocity;
            }
        

            Vector3 targetDirection = Vector3.ClampMagnitude((TargetMovePosition - transform.position).normalized, Vector3.Distance(TargetMovePosition, transform.position));

            Vector3 flyingAccelerationVector = targetDirection * flyingAcceleration * Time.fixedDeltaTime;

            Vector3 DragAccelerationVector = 0.5f * flyingDragMultiplier * droneVelocity.normalized * Mathf.Pow(droneVelocity.magnitude, 2) * Time.fixedDeltaTime;
            if (Vector3.Distance(TargetMovePosition, transform.position)<2f)
            {
                DragAccelerationVector *= 5f;
            }
            currentRigidbody.velocity += flyingAccelerationVector+DragAccelerationVector;

            /*
            MoveToPosition(TargetMovePosition);
            */
            LookToPosition(TargetLookPosition);
        }

        private void MoveToPosition(Vector3 targetPosition)
        {
            currentRigidbody.velocity = Vector3.ClampMagnitude((targetPosition - transform.position).normalized*flyingMaxVelocity,Vector3.Distance(targetPosition,transform.position)/Time.fixedDeltaTime); 
        }
        private void LookToPosition(Vector3 targetPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), 5f);
        }

        [SerializeField]
        private Transform[] bulletSpawns;
        [SerializeField]
        public GameObject bullet;
        [SerializeField]
        public float bulletSpeed = 100f;

        private void Shoot(Vector3 target)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = bulletSpawns[Random.Range(0,1)].position;
            newBullet.transform.forward = target-transform.position;
            newBullet.GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * bulletSpeed;
            newBullet.SetActive(true);
            WaterKat.Audio.AudioManager.PlaySound("AekoShoot");
        }
        

        private void OnTriggerStay(Collider other)
        {
            if (currentTarget != null) { return; }
            Enemy maybeEnemy = other.gameObject.GetComponent<Enemy>();
            if (maybeEnemy != null)
            {
                currentTarget = maybeEnemy;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            Enemy maybeEnemy = other.gameObject.GetComponent<Enemy>();
            if (maybeEnemy == currentTarget)
            {
                currentTarget = null;
            }
            shootTicker.ResetTick();
        }

    }
}