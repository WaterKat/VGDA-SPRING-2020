using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        int health = 100;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public DefaultInputActions InputActionMap;

        private void Awake()
        {
            InputActionMap = new DefaultInputActions();
        }
        private void OnEnable()
        {
            InputActionMap.Enable();
        }
        private void OnDisable()
        {
            InputActionMap.Disable();
        }

        #region "Grounded"
        public bool Grounded = false;
        public float GroundDistance = 0.15f;
        float SphereRadius = 0.6f;

        public bool CheckIfGrounded()
        {
            bool Grounded = false;
            Ray downwards = new Ray(transform.position, Vector3.down * (1 - SphereRadius + GroundDistance));
            RaycastHit hit;
            if (Physics.SphereCast(downwards, SphereRadius, out hit, downwards.direction.magnitude))
            {
                Grounded = true;
            }
            return Grounded;
        }
        public bool CheckIfGrounded(out Vector3 _groundVelocity)
        {
            bool Grounded = false;
            Ray downwards = new Ray(transform.position, Vector3.down * (1-SphereRadius + GroundDistance));
            RaycastHit hit;
            if (Physics.SphereCast(downwards,SphereRadius, out hit, downwards.direction.magnitude))
            {
                Grounded = true;
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    _groundVelocity = rb.velocity;
                }
            }
            _groundVelocity = Vector3.zero;
            return Grounded;
        }
        #endregion

        private void Update()
        {
            Grounded = CheckIfGrounded();
        }
    }
}