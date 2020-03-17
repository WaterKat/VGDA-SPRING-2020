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

            GunHolderA.LookAt(TargetPlayer.transform.position);
            Vector3 gunRotationA = GunHolderA.localRotation.eulerAngles;
            gunRotationA.z = 0;
            gunRotationA.x = Mathf.Min(0,gunRotationA.x);
            GunHolderA.localRotation = Quaternion.Euler(gunRotationA);

            GunHolderB.LookAt(TargetPlayer.transform.position);
            Vector3 gunRotationB = GunHolderB.localRotation.eulerAngles;
            gunRotationB.z = 0;
            gunRotationB.x = Mathf.Min(0, gunRotationB.x);
            GunHolderB.localRotation = Quaternion.Euler(gunRotationB);

            Vector3 desiredRotation = Quaternion.LookRotation((TargetPlayer.transform.position - transform.position).normalized).eulerAngles;
            transform.rotation = Quaternion.Euler(0, desiredRotation.y, 0);

            Vector3 currentV = CurrentRigidbody.velocity;
            Vector3 DesiredVelocity = ((TargetPlayer.transform.position - transform.position).normalized) * MaxSpeed;
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