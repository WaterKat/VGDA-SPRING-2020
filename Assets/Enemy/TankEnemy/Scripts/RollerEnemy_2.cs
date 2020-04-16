using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;
namespace WaterKat.Enemy_N
{
    [RequireComponent(typeof(Rigidbody))]
    public class RollerEnemy_2 : MonoBehaviour
    {
        Enemy currentEnemy;
        Rigidbody currentRigidbody;

        public float rollingMaxVelocity = 60;
        public float rollingAcceleration = 30;
        private float rollingDragMultiplier;

        private Player TargetPlayer;

        private void Awake()
        {
            currentEnemy = GetComponent<Enemy>();
        }
        private void Start()
        {
            currentRigidbody = GetComponent<Rigidbody>();
            rollingDragMultiplier = (-2 * rollingAcceleration) / Mathf.Pow(rollingMaxVelocity, 2);
        }

        private Vector3 lastEnemyVelocity = Vector3.zero;
        private Vector3 currentEnemyVelocity = Vector3.zero;
        private Vector3 currentEnemyAcceleration
        {
            get
            {
                return currentEnemyVelocity - lastEnemyVelocity;
            }
        }

        [SerializeField]
        private float targetDistance = 100;

        private Vector3 rollingAccelerationVector = Vector3.zero;
        private Vector3 rollingDragAccelerationVector = Vector3.zero;

        private Vector3 targetPlayerRelativePosition = Vector3.zero;
        private Vector3 targetPlayerDirection = Vector3.zero;
        private float targetPlayerDistance = 0f;

        private void FixedUpdate()
        {
            Vector3 localEulerRotationAngles = transform.localEulerAngles;  //Rigidbody Constraints wasn't working for some reason
            localEulerRotationAngles.z = 0;
            localEulerRotationAngles.x = 0;

            if ((TargetPlayer == null))
            {
                TargetPlayer = FindObjectOfType<Player>();
                return;
            }

            if (Vector3.Distance(transform.position, TargetPlayer.transform.position) > targetDistance)
            {
                return;
            }

            targetPlayerRelativePosition = TargetPlayer.transform.position - transform.position;
            targetPlayerDirection = targetPlayerRelativePosition.normalized;
            targetPlayerDistance = targetPlayerRelativePosition.magnitude;

            lastEnemyVelocity = currentEnemyVelocity;
            currentEnemyVelocity = currentRigidbody.velocity;

            rollingAccelerationVector = targetPlayerDirection * rollingAcceleration;

            rollingDragAccelerationVector = 0.5f * rollingDragMultiplier * currentEnemyVelocity.normalized * Mathf.Pow(currentEnemyVelocity.magnitude, 2);

            Vector3 vertical = rollingAccelerationVector + rollingDragAccelerationVector;
            vertical.y = 0;
            currentRigidbody.AddForce(vertical,ForceMode.Force);

            float range = .75f;
            if (Mathf.Abs(transform.right.y) > range)
            {
                Vector3 right = transform.right;
                right.y = Mathf.Clamp(right.y, -range, range);
                transform.right = right;
            }

            currentRigidbody.AddForce(Vector3.down * 10);
        }

    }
}