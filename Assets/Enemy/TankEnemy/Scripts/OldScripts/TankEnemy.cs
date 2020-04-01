using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;

namespace WaterKat.Enemy_N
{
    [RequireComponent(typeof(Enemy))]
    //[RequireComponent(typeof(Rigidbody))]
    public class TankEnemy : MonoBehaviour
    {
        Enemy CurrentEnemy;
        Rigidbody CurrentRigidbody;

        public Transform GunHolderA;
        public Transform GunHolderB;

        public float MaxSpeed = 20;

        bool gunReady = true;
        public float attackDelay = 1;
        [SerializeField]
        private Transform bulletSpawnLocA;
        [SerializeField]
        private Transform bulletSpawnLocB;
        [SerializeField]
        private GameObject bullet;
        public float gunspeed = 200;

        Player TargetPlayer;

        private void Awake()
        {
            CurrentEnemy = GetComponent<Enemy>();
        }
        void Start()
        {
            CurrentRigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if ((TargetPlayer == null))
            {
                TargetPlayer = FindObjectOfType<Player>();
                return;
            }

            if (gunReady)
            {
                Shoot();
            }

            Vector3 gunRotationA = Quaternion.LookRotation((TargetPlayer.transform.position-transform.position).normalized,transform.up).eulerAngles;
           // gunRotationA.z = 0;
            gunRotationA.x = Mathf.Max(0,gunRotationA.x);
            GunHolderA.rotation = Quaternion.Inverse(transform.rotation) * Quaternion.Euler(gunRotationA);

            /*
            GunHolderB.LookAt(TargetPlayer.transform.position, transform.rotation * Vector3.right);
            Vector3 gunRotationB = GunHolderB.localRotation.eulerAngles;

            //gunRotationB.x = Mathf.Max(0, gunRotationB.x);

            GunHolderB.localRotation = Quaternion.Euler(gunRotationB);

    */

            Vector3 desiredRotation = Quaternion.LookRotation((TargetPlayer.transform.position - transform.position).normalized).eulerAngles;
            if (Vector3.Distance(transform.position, TargetPlayer.transform.position) > 40)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, desiredRotation.y, 0), 90f * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, -desiredRotation.y, 0), 90f * Time.deltaTime);
            }


            Vector3 currentV = CurrentRigidbody.velocity;
            //            Vector3 DesiredVelocity = ((TargetPlayer.transform.position - transform.position).normalized) * MaxSpeed;
            Vector3 DesiredVelocity = (transform.forward) * MaxSpeed;
            DesiredVelocity.y = currentV.y;
            CurrentRigidbody.velocity = DesiredVelocity;
        }

        void Shoot()
        {
            GameObject tempBulletA = Instantiate(bullet, bulletSpawnLocA.transform.position, bulletSpawnLocA.transform.rotation) as GameObject;
            Rigidbody tempBulletRbA = tempBulletA.GetComponent<Rigidbody>();
            tempBulletRbA.velocity = (TargetPlayer.transform.position - transform.position).normalized * gunspeed;
            
            GameObject tempBulletB = Instantiate(bullet, bulletSpawnLocB.transform.position, bulletSpawnLocB.transform.rotation) as GameObject;
            Rigidbody tempBulletRbB = tempBulletB.GetComponent<Rigidbody>();
            tempBulletRbB.velocity = (TargetPlayer.transform.position - transform.position).normalized * gunspeed;


            StartCoroutine(ReloadCannon());
        }

        IEnumerator ReloadCannon()
        {
            gunReady = false;
            yield return new WaitForSeconds(attackDelay + Random.Range(-0.5f, 0.5f));
            gunReady = true;
        }
    }
}