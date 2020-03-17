using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat.Player_N;
namespace WaterKat.Enemy_N
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(Rigidbody))]

    public class Mine : MonoBehaviour
    {
        Enemy CurrentEnemy;
        Rigidbody CurrentRigidbody;

        Player TargetPlayer;

        public float MineSpeed = 20;
        public int DamageDealt = 20;

        private void Awake()
        {
            CurrentEnemy = GetComponent<Enemy>();
        }
        void Start()
        {
            CurrentRigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if ((TargetPlayer == null))
            {
                TargetPlayer = FindObjectOfType<Player>();
                CurrentRigidbody.angularVelocity = Vector3.forward*100;
                CurrentRigidbody.velocity = Vector3.zero;
                return;
            }
            

            CurrentRigidbody.velocity = (TargetPlayer.transform.position - transform.position).normalized * MineSpeed;
            CurrentRigidbody.angularVelocity = Vector3.forward;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player playerScript = collision.collider.transform.GetComponent<Player>();
            if (playerScript != null)
            {
                Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }
}