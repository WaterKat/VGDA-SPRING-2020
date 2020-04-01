using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;
namespace WaterKat.Enemy_N
{
    [RequireComponent(typeof(Rigidbody))]
    public class RollerEnemy : MonoBehaviour
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
            //transform.localEulerAngles = localEulerRotationAngles;

            if ((TargetPlayer == null))
            {
                TargetPlayer = FindObjectOfType<Player>();
                return;
            }

            targetPlayerRelativePosition = TargetPlayer.transform.position - transform.position;
            targetPlayerDirection = targetPlayerRelativePosition.normalized;
            targetPlayerDistance = targetPlayerRelativePosition.magnitude;

            lastEnemyVelocity = currentEnemyVelocity;
            currentEnemyVelocity = currentRigidbody.velocity;

           // if (currentEnemy.Grounded)
           // {
             //   rollingAccelerationVector = targetPlayerDirection * rollingAcceleration;
           /*}
            else
            {
                rollingAccelerationVector = Vector3.zero;
            }
            */

           // rollingDragAccelerationVector = 0.5f * rollingDragMultiplier * currentEnemyVelocity.normalized * Mathf.Pow(currentEnemyVelocity.magnitude, 2);

            currentRigidbody.AddRelativeTorque(Vector3.right*100);


            float isRollerEnemyUpright = Vector3.Dot(transform.right, Vector3.up);

            Debug.Log("is roller upright: " + isRollerEnemyUpright);

            Vector3 transformRight = transform.right;
            transformRight.y = 0;
            Vector3 transformForward = Quaternion.Euler(0, -90, 0) * transformRight.normalized;

            float currentZRotation = (Quaternion.Inverse(transform.rotation) * currentRigidbody.angularVelocity).z;
            float desiredZRotation = isRollerEnemyUpright*200f;
            float newZRotationSpeed = desiredZRotation - currentZRotation;


            currentRigidbody.AddTorque(Quaternion.Euler(transformForward)*Vector3.forward*newZRotationSpeed);

            //currentRigidbody.AddTorque(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * Vector3.forward * -Mathf.Sign(isRollerEnemyUpright) * 100f);
           
            //Quaternion.Inverse(Quaternion.Euler(0,transform.rotation.eulerAngles.y,0))*
            //Debug.DrawLine(transform.position + transform.right * 5, transform.position + currentRigidbody.angularVelocity,Color.blue,0.1f);

            float distFromRight = Vector3.Distance(TargetPlayer.transform.position,transform.position + transform.right * 5f);
            float distFromLeft = Vector3.Distance(TargetPlayer.transform.position, transform.position + transform.right * -5f);

            


           currentRigidbody.AddTorque(Vector3.up*-Mathf.Sign(distFromRight-distFromLeft)*500f);

          //  if ()

            //currentRigidbody.AddForce(rollingAccelerationVector + rollingDragAccelerationVector);
        }

    }
}